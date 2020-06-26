using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsFuiApiV1.Models.Data
{
    public class Exam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double TotalMarks { get; set; }
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
