using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Domain.Models;

namespace StudentAPI.Data
{
    public class StudentAPIDbContext : DbContext
    {
        public StudentAPIDbContext(DbContextOptions<StudentAPIDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //DbSets
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Ranking> Rankings { get; set; }

        //seed data into Rankinds and classes entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data into Ranking
            var rankings = new List<Ranking>()
            {
                new Ranking()
                {
                    Id=Guid.Parse("b9de3c6d-55e8-4857-9e8e-87a8c35f5ae0"),
                    Name="Brilliant"
                },
                new Ranking()
                {
                    Id=Guid.Parse("df1228e3-b8e6-4bd5-95a3-5b54e19b3a88"),
                    Name="Average"
                },
                new Ranking()
                {
                    Id=Guid.Parse("044677d4-ec73-4c97-a007-86530e9c0769"),
                    Name="Needs more work"
                }
            };
            modelBuilder.Entity<Ranking>().HasData(rankings);

            //Seed data into CLass
            var classes = new List<Class>()
            {
                new Class()
                {
                    Id=Guid.Parse("237b0357-dfb0-49e9-90a9-0a25933c439e"),
                    Name="Introduction to programming"
                },
                new Class()
                {
                    Id=Guid.Parse("5c47bab3-293b-4b64-8860-bdeb1516ed43"),
                    Name="Web Programming and Implementation"
                },
                new Class()
                {
                    Id=Guid.Parse("69e61e7e-6b80-4cde-b016-3a253e9a0c45"),
                    Name="Java Programming"
                },
                new Class()
                {
                    Id=Guid.Parse("0d06acb7-5ad4-458c-b26e-a3b390994335"),
                    Name="Object Oriented Programming"
                },
                new Class()
                {
                    Id=Guid.Parse("0d06acb7-5ad4-458c-b26e-a3b390994447"),
                    Name="Calculus"
                },
                 new Class()
                {
                    Id=Guid.Parse("0d06acb7-5ad4-458c-b26e-a3b390995678"),
                    Name="Data Structure and Algorythm"
                }
            };
            modelBuilder.Entity<Class>().HasData(classes);

        }

    }
}