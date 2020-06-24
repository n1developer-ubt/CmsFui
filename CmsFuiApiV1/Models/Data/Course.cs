using System.ComponentModel.DataAnnotations;

namespace CmsFuiApiV1.Models.Data
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
