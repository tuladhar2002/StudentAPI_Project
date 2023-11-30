using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Domain.Models.DTO;
using StudentAPI.Repository;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase    //create new users inside Identity Database
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Action Methods
        //Register New User 
        /// <summary>
        /// Register a student or admin
        /// </summary>
        /// <remarks>Awesomeness!</remarks>????????????????????????????????????????????????????????????????????????????????
        /// <response code="200">Student registered. Please Login</response>
        /// <response code="401">Something went wrong...</response>
        /// <response code="500">Something went wrong... we are looking into it!</response>
        //POST:/api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            //create new Identity User
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password); //use userManager.CreateAsync to take care of new user creation

            if(identityResult.Succeeded) //add role to user if succedded 
            {
                if(registerRequestDto.Roles !=null && registerRequestDto.Roles.Any()) //check registerRequestDto.Roles if empty
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was Registered! Please login..");
                    }
                }         
            }

            return BadRequest("Something went wrong...");
        }


        //Login Registered User
        //Action Methods
        //Register New User 
        /// <summary>
        /// Login a student or admin
        /// </summary>
        /// <remarks>Awesomeness!</remarks>????????????????????????????????????????????????????????????????????????????????
        /// <response code="200">Jwt....</response>
        /// <response code="401">Something went wrong...</response>
        /// <response code="500">Something went wrong... we are looking into it!</response>
        //POST:/api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username); //use userManager.FindByEmailAsync to take care of user login

            if(user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password); //Check if passwords match

                if(checkPasswordResult)
                {
                    //Implement created Token from ITokenRepository

                    //Get roles for the user
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList()); //pass in user and roles{as list} to get jwtToken
                        
                        var response = new LoginResponseDto //create a response to User with token after successful login attempt
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password Incorrect.");
        }

    }
}