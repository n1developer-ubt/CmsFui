using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsFuiApiV1.Models.Data
{
    public class Attendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Boolean Present { get; set; }
    }
}
