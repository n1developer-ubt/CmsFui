using System.ComponentModel.DataAnnotations;

namespace CmsFuiApiV1.Models.Data
{
    public class Semester
    {
        [Key] 
        public int Id { get; set; }
        public int Year { get; set; }
        public string Season { get; set; }
        public string Title { get; set; }
    }
}
