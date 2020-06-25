using System;
using System.ComponentModel.DataAnnotations;

namespace CmsFuiApiV1.Models.Data
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public Boolean Present { get; set; }
    }
}
