using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models
{
    public class SemesterCourse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("StudentExamsId")]
        public List<StudentExam> StudentExams { get; set; }
    }
}
