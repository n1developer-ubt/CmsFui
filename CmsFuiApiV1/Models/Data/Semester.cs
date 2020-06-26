using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsFuiApiV1.Models.Data
{
    public class Semester
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] 
        public int Id { get; set; }
        public int Year { get; set; }
        public string Season { get; set; }
        public string Title { get; set; }
    }
}
