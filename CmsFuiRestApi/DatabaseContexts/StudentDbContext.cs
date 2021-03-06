﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsFuiRestApi.Models;
using CmsFuiRestApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace CmsFuiRestApi.DatabaseContexts
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
