using System.ComponentModel.DataAnnotations;
using CmsFuiApiV1.Models.Data;

namespace CmsFuiApiV1.Models
{
    public class StudentAttendance
    {
        [Key]
        public int Id { get; set; }
        
        public Student Student { get; set; }
        
        public Attendance Attendance { get; set; }
    }
}
