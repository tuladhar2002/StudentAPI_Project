using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.CustomActionFilter;
using StudentAPI.Data;
using StudentAPI.Domain.Models;
using StudentAPI.Domain.Models.DTO;
using StudentAPI.Repository;

namespace StudentAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;

        public StudentController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieves all Students from the database
        /// ->can also query based on different fields (e.g. name, email)
        /// </summary>
        /// <remarks>Awesomeness!</remarks>????????????????????????????????????????????????????????????????????????????????ß
        /// <response code="200">Student retrieved</response>
        /// <response code="404">Students not found</response>
        /// <response code="500">Something went wrong... we are looking into it!</response>
        //GetAll{http://localhost:5288/api/student?filterOn=Name&filterQuery=Name}
        [HttpGet]
        [Authorize(Roles = "Admin, Student")]
        public async Task<IActionResult> GetAllStudents([FromQuery] string? filterOn,
        [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAscending)
        {
            var allStudents = await studentRepositories.GetAllStudentsAsync(filterOn, filterQuery, sortBy, isAscending);

            return Ok(mapper.Map<List<StudentDto>>(allStudents));
        }

        /// <summary>
        /// Retrieves a specific Students from the database by id 
        /// </summary>
        /// <remarks>Awesomeness!</remarks>????????????????????????????????????????????????????????????????????????????????
        /// <parameter id="...">Id of a student</parameter>
        /// <response code="200">Student retrieved</response>
        /// <response code="404">Student not found</response>
        /// <response code="500">Something went wrong... we are looking into it!</response>
        //GetById{http://localhost:5288/api/student/{ID}}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Student")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var studentDomainModel = await studentRepositories.GetAllStudentByIdAsync(id);

            if (studentDomainModel == null)
            {
                return NotFound("Failed. Provided StudentId does not exist.");
            }

            return Ok(mapper.Map<StudentDto>(studentDomainModel));
        }
        
        /// <summary>
        /// Create a student 
        /// </summary>
        /// <remarks>Awesomeness!</remarks>????????????????????????????????????????????????????????????????????????????????ß
        /// <response code="200">Student created</response>
        /// <response code="401">Failed to Create. Provided ClassId or RankingId does not exist.</response>
        /// <response code="404">Students not found</response>
        /// <response code="500">Something went wrong... we are looking into it!</response>
        //PostStudent{http://localhost:5288/api/student}
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto createStudentDto)
        {
            var studentDomainModel = mapper.Map<Student>(createStudentDto);

            var createdStudent = await studentRepositories.CreateStudentAsync(studentDomainModel);

            if (createdStudent == null) { return BadRequest(new { Error = "Failed to Create. Provided ClassId or RankingId does not exist." }); }

            return CreatedAtAction(nameof(GetStudentById), new { id = studentDomainModel.Id }, createStudentDto);

        }

        /// <summary>
        /// Update a already created student 
        /// </summary>
        /// <remarks>Awesomeness!</remarks>????????????????????????????????????????????????????????????????????????????????
        /// <parameter id="...">Id of the student</parameter>
        /// <response code="200">Student updated</response>
        /// <response code="401">Failed to Update. Provided StudentId does not exist</response>
        /// <response code="404">Student not found</response>
        /// <response code="500">Something went wrong... we are looking into it!</response>
        //PUTStudent{http://localhost:5288/api/student/{id}}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateExistingStudent([FromRoute] Guid id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            var studentDomainModel = mapper.Map<Student>(updateStudentDto);
            studentDomainModel = await studentRepositories.UpdateExistingStudentAsync(id, studentDomainModel);

            if (studentDomainModel == null) { return NotFound("Failed to Update. Provided StudentId does not exist"); }

            return Ok(mapper.Map<StudentDto>(studentDomainModel));
        }

        /// <summary>
        /// Delete a student 
        /// </summary>
        /// <remarks>Awesomeness!</remarks>????????????????????????????????????????????????????????????????????????????????
        /// <parameter id="...">Id of the student</parameter>
        /// <response code="200">Student deleted</response>
        /// <response code="404">Failed to Delete. Provided StudentId does not exist</response>
        /// <response code="500">Something went wrong... we are looking into it!</response>
        //DeleteStudent{http://localhost:5288/api/student/{id}}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var studentDomainModel = await studentRepositories.DeleteStudentAsync(id);

            if (studentDomainModel == null) { return NotFound("Failed to Delete. Provided StudentId does not exist"); }

            return Ok("Provided StudentId Succesfully Deleted");
        }

    }
}
