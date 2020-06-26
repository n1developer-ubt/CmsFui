using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CmsFuiApiV1.Models.Data;

namespace CmsFuiApiV1.Models
{
    public class SemesterCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("StudentExamsId")]
        public List<StudentExam> StudentExams { get; set; }

        [ForeignKey("SemesterCourseId")]
        public List<Attendance> Attendances { get; set; }

        public Boolean RegistrationAvailable { get; set; } = true;
        public Boolean Registered { get; set; } = false;

    }
}
