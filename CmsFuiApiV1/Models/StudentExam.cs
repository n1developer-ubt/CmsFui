using System.ComponentModel.DataAnnotations.Schema;
using CmsFuiApiV1.Models.Data;

namespace CmsFuiApiV1.Models
{
    public class StudentExam
    {
        public int Id { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        public int ObtainedMarks { get; set; }
        
    }
}
