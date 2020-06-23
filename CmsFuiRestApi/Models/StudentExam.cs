using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models
{
    public class StudentExam
    {
        public int Id { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        public int ObtainedMarks { get; set; }
        
    }
}
