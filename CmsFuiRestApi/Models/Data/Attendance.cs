using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public bool Present { get; set; }
    }
}
