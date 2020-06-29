using CmsFui.Models.Data;

namespace CmsFui.Models
{
    public class StudentExam
    {
        public int Id { get; set; }

        public Exam Exam { get; set; }

        public int ObtainedMarks { get; set; }
        
    }
}
