using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Domain.Models.DTO
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }  
   
        public required string Nationality { get; set; }

        // public Guid? ClassId { get; set; }
        // public Guid? RankingId { get; set; }

        public ClassDto Class {get; set;}
        public RankingDto Ranking { get; set; }
    }
}