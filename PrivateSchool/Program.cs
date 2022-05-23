using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrivateSchool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool
{
    public class Program
    {
        public static void Main(string[] args)
        {   /*
            var privateSchoolDBContext = new PrivateSchoolDBContext();
            privateSchoolDBContext.Database.EnsureDeleted();
            privateSchoolDBContext.Database.EnsureCreated();
            */
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
