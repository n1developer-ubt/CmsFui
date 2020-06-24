using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsFuiApiV1.Models.Data
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
