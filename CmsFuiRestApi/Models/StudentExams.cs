using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models
{
    public class StudentExams
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("ExamId")] 
        public Exam Exam { get; set; }

    }
}
