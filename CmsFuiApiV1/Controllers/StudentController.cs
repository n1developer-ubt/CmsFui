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
            _studentService.AddFakeStudent();
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Helsso");
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
