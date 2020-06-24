using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CmsFuiApiV1.Models.Data
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }
        public string Season { get; set; }
        public string Program { get; set; }
        public string RollNo { get; set; }

        
        [JsonIgnore]
        [IgnoreDataMember]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey("SemesterId")]
        public List<StudentSemester> Semesters { get; set; }

        [ForeignKey("CurrentSemesterId")]
        public StudentSemester CurrentSemester { get; set; }
    }
}
