using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
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

            Teacher teacherAsma = new Teacher()
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

            Exam exam = new Exam()
            {
                Name = "Assignment#1",
                Date = DateTime.Now,
                TotalMarks = 10
            };

            Exam exam2 = new Exam()
            {
                Name = "Assignment#1",
                Date = DateTime.Now,
                TotalMarks = 10
            };

            StudentExam stuExam = new StudentExam()
            {
                ObtainedMarks = 8,
                Exam = exam
            };

            StudentExam stuExam2 = new StudentExam()
            {
                ObtainedMarks = 8,
                Exam = exam2
            };

            SemesterCourse hciSemesterCourse = new SemesterCourse()
            {
                Teacher = teacherAsma,
                Course = hciCourse,
                StudentExams = new List<StudentExam>()
                {
                    stuExam,
                    stuExam,
                    stuExam,
                    stuExam
                },
                Attendances = GetRandomAttendances(200)
            };

            SemesterCourse svvSemesterCourse = new SemesterCourse()
            {
                Teacher = teacherAqeel,
                Course = svvCourse,
                StudentExams = new List<StudentExam>()
                {
                    stuExam2
                },
                Attendances = GetRandomAttendances(200)
            };


            var studentSemester = new StudentSemester()
            {
                Semester = sem6,
                Courses = new List<SemesterCourse>()
                {
                    hciSemesterCourse,
                    svvSemesterCourse,
                },
                Description = "Fall 2020",
                Title = "Semester #6",
                Gpa = 3.8
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
                Semesters = new List<StudentSemester>()
                {
                    studentSemester,
                    studentSemester,
                    studentSemester,
                    studentSemester,
                    studentSemester
                }
            };

            _dbContext.Students.Add(newStudent);

            _dbContext.SaveChanges();

            newStudent.CurrentSemester = studentSemester;

            _dbContext.SaveChanges();

        }

        public enum StudentUpdateResult
        {
            UpdateFailed,
            Updated
        }

        public async Task<StudentUpdateResult> UpdateStudent(Student newStudent)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == newStudent.Id);

            if (student == null)
                return StudentUpdateResult.UpdateFailed;

            student.Password = newStudent.Password;
            student.Email = newStudent.Email;
            student.Name = newStudent.Name;

            await _dbContext.SaveChangesAsync();

            return StudentUpdateResult.Updated;
        }

        public async Task<List<StudentSemester>> GetSemesterCoursesResult(int studentId)
        {
            var stu = await _dbContext.Students.Include(stu => stu.Semesters)
                .FirstOrDefaultAsync(stu => stu.Id == studentId);

            return stu?.Semesters.Select(s => new StudentSemester()
            {
                Description = s.Description,
                Gpa = s.Gpa,
                Title = s.Title
            }).ToList();
        }

        private List<Attendance> GetRandomAttendances(int count)
        {
            List<Attendance> at = new List<Attendance>();

            for (int x = 0; x < count; x++)
            {
                var att = new Faker<Attendance>()
                    .RuleFor(x => x.Date, f => f.Date.Between(new DateTime(2019, 6, 3), DateTime.Now))
                    .RuleFor(x => x.Present, f => 50 < new Random().Next(0, 100));
                at.Add(att);
            }

            return at;
        }

        public async Task<List<SemesterCourse>> GetRegisteredCourses(int id)
        {
            var stu = await _dbContext.Students
                .Include(student => student.CurrentSemester)
                    .ThenInclude(semester => semester.Courses)
                        .ThenInclude(semesterCourse => semesterCourse.Teacher)
                .Include(student => student.CurrentSemester)
                    .ThenInclude(semester => semester.Courses)
                        .ThenInclude(semesterCourse => semesterCourse.Course)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (stu == null)
                return null;

            return stu.CurrentSemester.Courses.Where(c => c.Registered).ToList();
        }

        public async Task RegisterCourses(int studentId, List<int> coursesToRegister)
        {
            var stu = await _dbContext.Students.Include(stu => stu.CurrentSemester).ThenInclude(sem => sem.Courses)
                .FirstOrDefaultAsync(stu => stu.Id == studentId);

            foreach (var courseId in coursesToRegister)
            {
                var course = stu.CurrentSemester.Courses.FirstOrDefault(c => c.Id == courseId);

                if (course == null)
                {
                    continue;
                }

                if (!course.RegistrationAvailable)
                    continue;

                course.Registered = true;
                course.RegistrationAvailable = false;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SemesterCourse>> GetUnRegisteredCourses(int studentId)
        {

            var cs = await _dbContext.Students
                .Include(stu => stu.CurrentSemester)
                .ThenInclude(sem => sem.Courses)
                .ThenInclude(c => c.Course)
                .Include(stu => stu.CurrentSemester)
                .ThenInclude(sem => sem.Courses)
                .ThenInclude(c => c.Teacher)
                .FirstOrDefaultAsync(stu => stu.Id == studentId);

            return cs?.CurrentSemester?.Courses?.Select(c => new SemesterCourse() { Id = c.Id, Course = c.Course, Teacher = c.Teacher, RegistrationAvailable = c.RegistrationAvailable }).Where(c => !c.Registered && c.RegistrationAvailable).ToList();
        }

        public async Task<List<Attendance>> GetAttendance(int studentId, string courseId)
        {
            var result = await _dbContext.Students
                .Include(student => student.CurrentSemester)
                .ThenInclude(semester => semester.Courses)
                .ThenInclude(reg => reg.Attendances)
                .Include(student => student.CurrentSemester)
                .ThenInclude(semester => semester.Courses)
                .ThenInclude(sem => sem.Course)
                .FirstOrDefaultAsync(student => student.Id == studentId);

            var results = result?.CurrentSemester?.Courses?.FirstOrDefault(x => x.Course.Code.Equals(courseId));

            return results?.Attendances;
        }

        public async Task<List<StudentExam>> GetExams(int studentId, string courseId)
        {
            var result = await _dbContext.Students
                .Include(student => student.CurrentSemester)
                .ThenInclude(semester => semester.Courses)
                .ThenInclude(reg => reg.StudentExams)
                .ThenInclude(stuExam => stuExam.Exam)
                .Include(student => student.CurrentSemester)
                .ThenInclude(semester => semester.Courses)
                .ThenInclude(sem => sem.Course)
                .FirstOrDefaultAsync(student => student.Id == studentId);

            var results = result?.CurrentSemester?.Courses?.FirstOrDefault(x => x.Course.Code.Equals(courseId));

            return results?.StudentExams;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var result = await _dbContext.Students.Select(x => new Student()
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.Id == id);

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

    }
}
