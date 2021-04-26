using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            // var configuration = new ConfigurationBuilder()
            //.AddCommandLine(args)
            //.Build();


            //    var hostUrl = configuration["hosturl"];
            //    if (string.IsNullOrEmpty(hostUrl))
            //        hostUrl = "http://0.0.0.0:6000";


            //    var host = new WebHostBuilder()
            //        .UseKestrel()
            //        .UseUrls(hostUrl)   // <!-- this 
            //        .UseContentRoot(Directory.GetCurrentDirectory())
            //        .UseIISIntegration()
            //        .UseStartup<Startup>()
            //        .UseConfiguration(configuration)
            //        .Build();

            //    host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
