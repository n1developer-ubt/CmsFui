using System.Collections.Generic;
using System.Linq;
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

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddFakeStudent()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            Teacher teacherAsma  = new Teacher()
            {
                Name = "Asma Naveed"
            };
            
            Teacher teacherAqeel = new Teacher()
            {
                Name = "Muhammad Aqeel"
            };

            Semester sem6 = new Semester()
            {
                Season = "F",
                Title = "Semester 6",
                Year = 2020
            };

            Course hciCourse = new Course()
            {
                Code = "001",
                Name = "Human Computer Interaction"
            };

            Course svvCourse = new Course()
            {
                Code = "002",
                Name = "Software V&V"
            };

            SemesterCourse hciSemesterCourse = new SemesterCourse()
            {
                Teacher = teacherAsma,
                Course = hciCourse
            };
            
            SemesterCourse svvSemesterCourse = new SemesterCourse()
            {
                Teacher = teacherAqeel,
                Course = svvCourse
            };
            

            var studentSemester = new StudentSemester()
            {
                Semester = sem6,
                StudentRegisteredCourses = new List<SemesterCourse>()
                {
                    hciSemesterCourse,
                    svvSemesterCourse
                }
            };

            Student newStudent = new Student
            {
                Email = "usamat37@gmail.com",
                Name = "Usama Bin Tariq",
                RollNo = "053",
                Password = "abcdef",
                Year = 17,
                Program = "BCSE",
                Season = "F",
                CurrentSemester = studentSemester
            };

            _dbContext.Students.Add(newStudent);

            _dbContext.SaveChanges();
        }

        public async Task<List<SemesterCourse>> GetRegisteredCourses(int id)
        {
            var stu = await _dbContext.Students
                .Include(student=>student.CurrentSemester)
                    .ThenInclude(semester=>semester.StudentRegisteredCourses)
                        .ThenInclude(semesterCourse=>semesterCourse.Teacher)
                .Include(student => student.CurrentSemester)
                    .ThenInclude(semester => semester.StudentRegisteredCourses)
                    .ThenInclude(semesterCourse => semesterCourse.Course)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (stu == null)
                return null;

            return stu.CurrentSemester.StudentRegisteredCourses;
        }


        public async Task<List<StudentExam>> GetExams(int studentId, string courseId)
        {
            var result = await _dbContext.Students
                .Include(student=>student.CurrentSemester)
                .ThenInclude(semester=>semester.StudentRegisteredCourses)
                .ThenInclude(reg=>reg.StudentExams)
                .Include(student => student.CurrentSemester)
                .ThenInclude(semester => semester.StudentRegisteredCourses)
                .ThenInclude(sem=>sem.Course)
                .FirstOrDefaultAsync(student => student.Id == studentId);

            var results = result?.CurrentSemester?.StudentRegisteredCourses?.FirstOrDefault(x => x.Course.Code.Equals(courseId));

            return results?.StudentExams;
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
    }
}
