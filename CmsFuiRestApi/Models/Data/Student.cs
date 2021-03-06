﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CmsFuiRestApi.Models
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
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey("SemesterId")]
        public List<StudentSemester> Semesters { get; set; }

        [ForeignKey("CurrentSemesterId")]
        public StudentSemester CurrentSemester { get; set; }
    }
}
