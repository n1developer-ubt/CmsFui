using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CmsFuiRestApi.Models.Data;

namespace CmsFuiRestApi.Models
{
    public class StudentSemester
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }

        [ForeignKey("StudentRegisteredCoursesId")]
        public List<SemesterCourse> StudentRegisteredCourses { get; set; }
    }
}
