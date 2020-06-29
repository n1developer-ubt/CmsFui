using System.Collections.Generic;
using CmsFui.Models.Data;

namespace CmsFui.Models
{
    public class StudentSemester
    {
        public int Id { get; set; }

        public Semester Semester { get; set; }

        public List<SemesterCourse> Courses { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Gpa { get; set; }
    }
}
