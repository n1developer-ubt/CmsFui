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

        [HttpGet("GetCourseExams")]
        public async Task<IActionResult> GetCourseExams([FromQuery] int studentId, [FromQuery] string courseCode)
        {
            var exams = await _studentService.GetExams(studentId, courseCode);

            if (exams == null)
                return BadRequest("Error");

            return Ok(exams);
        }

        [HttpGet("GetRegisteredCourses")]
        public async Task<IActionResult> GetRegisteredCourses([FromQuery] int studentId)
        {

            var res = await _studentService.GetRegisteredCourses(studentId);

            if (res == null)
                return BadRequest("Error");

            return Ok(res);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel auth)
        {
            var result = await  _studentService.Authenticate(auth);

            if (result == null)
                return BadRequest(new {error = "Unable to authenticate"});

            return Ok(result);
        }
    }
}
