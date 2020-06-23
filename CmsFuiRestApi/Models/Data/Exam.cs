using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double TotalMarks { get; set; }
        public double ObtainedMarks { get; set; }
        public DateTime Date { get; set; }
    }

    public class ExamType
    {
        public int MidTerm { get; } = 1;
        public int Assignment { get; } = 2;
        public int Quiz { get; } = 3;
        public int FinalExam { get; } = 4;
    }

}
