using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentAPI.Data
{
    public class StudentAPIAuthDbContext: IdentityDbContext
    {
        public StudentAPIAuthDbContext(DbContextOptions<StudentAPIAuthDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //standerize Admin and Student role Ids
            var adminRoleId = "3e673030-80fa-4fa0-8f1d-1684bdcecc84";
            var studentRoleId = "d3638421-a6db-46a8-9c5c-288211ea63b0";

            //create Roles
            var roles = new List<IdentityRole> //use IdentityRole for role creation
            {
                new IdentityRole //Admin
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                 new IdentityRole //student
                {
                    Id = studentRoleId,
                    Name = "Student",
                    NormalizedName = "Student".ToUpper()
                }

            };

            //Seed data
            builder.Entity<IdentityRole>().HasData(roles);


        }
    }
}