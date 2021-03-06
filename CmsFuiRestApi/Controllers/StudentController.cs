﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsFuiRestApi.Models.Data;
using CmsFuiRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CmsFuiRestApi.Controllers
{
    [Route("api/v1/student/")]
    public class StudentController:Controller
    {

        private readonly StudentService _studentService;

        public StudentController()
        {
            _studentService= new StudentService();
        }
        
        [HttpGet("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel auth)
        {
            var result = await  _studentService.Authenticate(auth);

            if (result == null)
                return BadRequest(new {error = "Unable to authenticate"});

            return Ok(result);
        }
    }
}
