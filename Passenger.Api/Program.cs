using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Passenger.Api
{

    
    public class Program
    {
        public static void Main(string[] args)
        {

            var host =  new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            ILogger logger = host.Services.GetService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Starting web host");

                host.Run();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Starting web host failed.");
            }

            // var host = new WebHostBuilder()
            //     .UseKestrel()
            //     .UseContentRoot(Directory.GetCurrentDirectory())
            //     .UseIISIntegration()
            //     .UseStartup<Startup>()
            //     .Build();

            // host.Run();

            // CreateWebHostBuilder(args).Build().Run();
        }

          public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                
                logging.SetMinimumLevel(LogLevel.Trace);
            })
        .UseNLog();  // NLog: Setup NLog for Dependency injection 
    }
}
