using CmsFui.Models.Data;
using Newtonsoft.Json;

namespace CmsFui.Models
{
    public class StudentExam
    {
        public int Id { get; set; }

        public Exam Exam { get; set; }

        public int ObtainedMarks { get; set; }

        [JsonIgnore]
        public double Percentage => (ObtainedMarks / Exam.TotalMarks) * 100;

    }
}
