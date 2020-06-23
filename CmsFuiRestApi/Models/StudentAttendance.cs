using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models
{
    public class StudentAttendance
    {
        [Key]
        public int Id { get; set; }
        
        public Student Student { get; set; }
        
        public Attendance Attendance { get; set; }
    }
}
