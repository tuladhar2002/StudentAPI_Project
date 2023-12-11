using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Domain.Models;
using StudentAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using StudentAPI.Domain.Models.DTO;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace StudentAPI.Repository
{
    public class PGSQLStudentsRepository : IStudentRepositories
    {
        private readonly StudentAPIDbContext dbContext;

        //Ctor
        public PGSQLStudentsRepository(StudentAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Create Student
        public async Task<Student?> CreateStudentAsync(Student student)
        {
            //check user provided class id and ranking id exists in dbs entities or not
            if(student.ClassId != null && student.RankingId != null)
            {
                var classResult = await dbContext.Classes.FirstOrDefaultAsync(x=> x.Id == student.ClassId);
                var rankingResult = await dbContext.Rankings.FirstOrDefaultAsync(x=> x.Id == student.RankingId);

                if(classResult == null || rankingResult == null)
                {
                    return null;
                }

            }
            await dbContext.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return student;


        }


        //Delete Existing Student
        public async Task<Student?> DeleteStudentAsync(Guid id)
        {
            var studentDomainModel = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentDomainModel == null) { return null; }

            dbContext.Students.Remove(studentDomainModel);
            await dbContext.SaveChangesAsync();

            return studentDomainModel;
        }

        //Get Student By Id
        public async Task<Student?> GetAllStudentByIdAsync(Guid id)
        {
            var student = await dbContext.Students.Include("Class").Include("Ranking").FirstOrDefaultAsync(x => x.Id == id && x.IsEnabled == true);
            if (student == null)
            {
                return null;
            }

            return student;
        }

        //Get All Students
        public async Task<List<Student>> GetAllStudentsAsync(string? filterOn=null, string? filterQuery=null, string? sortBy=null, bool isAscending=true)
        {
            var students = dbContext.Students.Include("Class").Include("Ranking").AsQueryable();

            //Query
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false) //check filterOn and filterQuery empty or not
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) //check if the entity exists
                {
                    students = students.Where(x=>x.Name.Contains(filterQuery)); //checks each dbs row 'Name' entity with filterQuery field and stores in students if equals
                }
                if(filterOn.Equals("Email", StringComparison.OrdinalIgnoreCase)) //check if the entity exists
                {
                    students = students.Where(x=>x.Email.Contains(filterQuery)); //checks each dbs row 'Email' entity with filterQuery field and stores in students if equals
                }
                if(filterOn.Equals("Nationality", StringComparison.OrdinalIgnoreCase)) //check if the entity exists
                {
                    students = students.Where(x=>x.Nationality.Contains(filterQuery)); //checks each dbs row 'Nationality' entity with filterQuery field and stores in students if equals
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy)==false) //check sortyBy field exists in User request
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) //check if sortBy field equals "Name"
                {
                    //ternary operator, orderby ascending if isAscending true or else order by descending
                    students = isAscending? students.OrderBy(x=>x.Name): students.OrderByDescending(x=>x.Name); 
                }
            }

            students = students.Where(x => x.IsEnabled == true);

            return await students.ToListAsync();
        }

        //Update existing Student
        public async Task<Student?> UpdateExistingStudentAsync(Guid id, Student student)
        {
            var studentDomainModel = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (studentDomainModel == null)
            {
                return null;
            }

            studentDomainModel.Name = student.Name;
            studentDomainModel.Email = student.Email;
            studentDomainModel.ClassId = student.ClassId;
            studentDomainModel.RankingId = student.RankingId;

            await dbContext.SaveChangesAsync();

            return studentDomainModel;

        }

        //enable/desable Student
        public async Task<string?> EnableStudent(Guid id, bool enableStudent)
        {
            var studentDomainModel = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (studentDomainModel == null) {
                return null;
            }
            if (enableStudent == true && studentDomainModel.IsEnabled == true){
                return "Already enabled";
            }
            if (enableStudent == false && studentDomainModel.IsEnabled == false){
                return "Already disabled";
            }
            if (enableStudent == true) {
                studentDomainModel.IsEnabled = true;
                await dbContext.SaveChangesAsync();
                return "The Student with ID: "+id+" is enabled";
            }
            else {
                studentDomainModel.IsEnabled = false;
                await dbContext.SaveChangesAsync();
                return "The Student with ID: "+id+" is disabled";
            }

        }
    }
}