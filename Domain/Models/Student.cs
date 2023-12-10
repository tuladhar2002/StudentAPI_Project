using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }  
   
        public required string Nationality { get; set; }

        public Guid? ClassId { get; set; }
        public Guid? RankingId { get; set; }

        public bool IsEnabled { get; set; }

        //Nav Prop
        public Class Class { get; set; }
        public Ranking Ranking { get; set; }

    }
}