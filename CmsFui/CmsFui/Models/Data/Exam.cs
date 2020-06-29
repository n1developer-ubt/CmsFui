using System;

namespace CmsFui.Models.Data
{
    public class Exam
    {
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
