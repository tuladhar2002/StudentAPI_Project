using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPI.Domain.Models;
using StudentAPI.Domain.Models.DTO;

namespace StudentAPI.Repository
{
    public interface IStudentRepositories
    {
        Task<List<Student>> GetAllStudentsAsync(string? filterOn=null, string? filterQuery=null, string? sortBy=null, bool isAscending=true); //getAllStudents
        Task<Student?> GetAllStudentByIdAsync(Guid id); //getStudentById
        Task<Student?> CreateStudentAsync(Student student); //CreateStudent
        Task<Student?> UpdateExistingStudentAsync(Guid id, Student student); //UpdateExistingStudent
        Task<Student?> DeleteStudentAsync(Guid id);
    }
}