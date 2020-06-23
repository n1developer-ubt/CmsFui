using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsFuiRestApi.Models.Data
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
