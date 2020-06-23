using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                {Server = "localhost", Port = 3306, Password = "", UserID = "root"}.ConnectionString);
        }
    }
}
