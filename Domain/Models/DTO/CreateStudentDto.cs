using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Domain.Models.DTO
{
    public class CreateStudentDto
    {
        [Required]
        [MinLength(1, ErrorMessage ="Name has to be a minimum of 1 Character")]
        public required string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Not a valid Email")]
        public required string Email { get; set; }  

        [Required]
        [MinLength(1, ErrorMessage ="Nationality has to be a minimum of 1 Character")]
        public required string Nationality { get; set; }

        public bool IsEnabled { get; set; }

        public Guid? ClassId { get; set; }
        public Guid? RankingId { get; set; }
    }
}