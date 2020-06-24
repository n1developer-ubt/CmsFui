using System.Collections.Generic;
using System.Threading.Tasks;
using CmsFuiApiV1.DatabaseContexts;
using CmsFuiApiV1.Models;
using CmsFuiApiV1.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace CmsFuiApiV1.Services
{
    public class StudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService()
        {
            _dbContext = new StudentDbContext();
        }

        public async void AddFakeStudent()
        {
            Student newStudent = new Student
            {
                Email = "usamat37@gmail.com",
                Name = "Usama Bin Tariq",
                RollNo = "053",
                Password = "abcdef",
                Year = 17,
                Program = "BCSE",
                Season = "FA",
            };

            var result = _dbContext.Students.Add(newStudent);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var result = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Student> Authenticate(AuthenticationModel auth)
        {
            var student =
                await _dbContext.Students.FirstOrDefaultAsync(x => auth.Username.ToLower().Equals((x.Season + x.Year + x.Program + x.RollNo).ToLower()));

            if (student == null)
                return null;

            return student.Password.Equals(auth.Password) ? student : null;
        }

        public async void UpdateStudent(Student stu)
        {
            _dbContext.Students.Update(stu);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SemesterCourse>> GetRegisteredCourses(int id)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(x=>x.Id == id);

            if (student == null)
                return null;

            return null;
        }

    }
}
