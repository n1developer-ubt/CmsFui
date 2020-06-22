using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models
{
    public class Student
    {
        public string Year { get; set; }
        public string Department { get; set; }
        public string RollNo { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string CurrentSemester { get; set; }
        public string Email { get; set; }
    }
}
