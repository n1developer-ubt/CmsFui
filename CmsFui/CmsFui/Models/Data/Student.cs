using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CmsFui.Models.Data
{
    public class Student
    {
        public int Id { get; set; }

        public int Year { get; set; }
        public string Season { get; set; }
        public string Program { get; set; }
        public string RollNo { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<StudentSemester> Semesters { get; set; }

        public StudentSemester CurrentSemester { get; set; }
    }
}
