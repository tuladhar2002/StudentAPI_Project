using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace StudentAPI.Domain.Models.DTO
{
    public class RegisterRequestDto
    {
        //accept Username & Password 
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; } //accept Roles as an array
    }
}