using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsFuiRestApi.DatabaseContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CmsFuiRestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StudentDbContext dbContext = new StudentDbContext();
            dbContext.Database.EnsureCreated();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
