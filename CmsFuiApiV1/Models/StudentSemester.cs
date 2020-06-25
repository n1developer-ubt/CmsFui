using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CmsFuiApiV1.Models.Data;

namespace CmsFuiApiV1.Models
{
    public class StudentSemester
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SemesterIdx")]
        public Semester Semester { get; set; }

        [ForeignKey("StudentRegisteredCoursesId")]
        public List<SemesterCourse> StudentRegisteredCourses { get; set; }
    }
}
