using System;
using System.Collections.Generic;
using CmsFui.Models.Data;

namespace CmsFui.Models
{
    public class SemesterCourse
    {
        
        public int Id { get; set; }

        public Teacher Teacher { get; set; }

        public Course Course { get; set; }
        public List<StudentExam> StudentExams { get; set; }

        public List<Attendance> Attendances { get; set; }

        public Boolean RegistrationAvailable { get; set; } = true;
        public Boolean Registered { get; set; } = false;

    }
}
