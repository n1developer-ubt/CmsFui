using System.Collections.Generic;
using System.Threading.Tasks;
using CmsFuiApiV1.DatabaseContexts;
using CmsFuiApiV1.Models.Data;
using CmsFuiApiV1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CmsFuiApiV1.Controllers
{
    [Route("/api/v1/student")]
    public class StudentController:Controller
    {

        private readonly StudentService _studentService;

        public StudentController(StudentDbContext dbContext)
        {
            _studentService = new StudentService(dbContext);
            
            //_studentService.AddFakeStudent();
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Helsso");
        }

        [HttpGet("GetSemesterCoursesResult")]
        public async Task<IActionResult> GetSemesterCoursesResult([FromQuery]int studentId)
        {
            var result = await  _studentService.GetSemesterCoursesResult(studentId);

            if (result == null)
                return BadRequest("GetSemesterCoursesResult Error");

            return Ok(result);

        }

        [HttpGet("GetUnRegisteredCourses")]
        public async Task<IActionResult> GetUnRegisteredCourses([FromQuery] int studentId)
        {
            var courses = await _studentService.GetUnRegisteredCourses(studentId);

            if (courses == null)
                return BadRequest("UnRegisteredCourses System Error");

            return Ok(courses);
        }

        [HttpPost("RegisterCourses")]
        public async Task<IActionResult> RegisterCourses([FromQuery]int studentId, [FromBody] List<int> coursesToRegister)
        {
            await _studentService.RegisterCourses(studentId,coursesToRegister);

            return Ok();
        }

        [HttpGet("GetAttendance")]

        public async Task<IActionResult> GetAttendance([FromQuery] int studentId, [FromQuery] string courseCode)
        {
            var exams = await _studentService.GetAttendance(studentId, courseCode);

            if (exams == null)
                return BadRequest("Error");

            return Ok(exams);
        }

        [HttpGet("GetCourseExams")]
        public async Task<IActionResult> GetCourseExams([FromQuery] int studentId, [FromQuery] string courseCode)
        {
            var exams = await _studentService.GetExams(studentId, courseCode);

            if (exams == null)
                return BadRequest("Error");

            return Ok(exams);
        }
        

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student stu)
        {
            return Ok((await _studentService.UpdateStudent(stu)).ToString());
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudent([FromQuery]int studentId)
        {
            var result = await _studentService.GetStudentById(studentId);

            if (result == null)
                return BadRequest("GetStudent Error");

            return Ok(result);
        }

        [HttpGet("GetRegisteredCourses")]
        public async Task<IActionResult> GetRegisteredCourses([FromQuery] int studentId)
        {

            var res = await _studentService.GetRegisteredCourses(studentId);

            if (res == null)
                return BadRequest("Error");

            return Ok(res);
        }

        [HttpGet("authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery] AuthenticationModel auth)
        {
            var result = await  _studentService.Authenticate(auth);

            if (result == null)
                return BadRequest(new {error = "Unable to authenticate"});

            return Ok(result);
        }
    }
}
