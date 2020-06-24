using System;
using System.ComponentModel.DataAnnotations;

namespace CmsFuiApiV1.Models.Data
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public bool Present { get; set; }
    }
}
