using CmsFuiApiV1.Models;
using CmsFuiApiV1.Models.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace CmsFuiApiV1.DatabaseContexts
{
    public class StudentDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(new MySqlConnectionStringBuilder()
                {Server = "localhost", Port = 3306, Password = "", UserID = "root", Database = "CMSFUI"}.ConnectionString);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SemesterCourse> SemesterCourses { get; set; }
    }
}
