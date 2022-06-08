using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;


namespace Passenger.Api
{

    
    public class Program
    {
        public static void Main(string[] args)
        {
                    // ASP.NET Core 3.0+:
                // The UseServiceProviderFactory call attaches the
                // Autofac provider to the generic hosting mechanism.
                var host = Host.CreateDefaultBuilder(args)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureWebHostDefaults(webHostBuilder => {
                    webHostBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                    })
                    .Build();

                host.Run();

            
            // var host =  new WebHostBuilder()
            //     .UseKestrel()
            //     .UseContentRoot(Directory.GetCurrentDirectory())
            //     .UseIISIntegration()
            //     .UseStartup<Startup>()
            //     .Build();

            // ILogger logger = host.Services.GetService<ILogger<Program>>();

            // try
            // {
            //     logger.LogInformation("Starting web host");

            //     host.Run();
            // }
            // catch (Exception ex)
            // {
            //     logger.LogCritical(ex, "Starting web host failed.");
            // }

            // // var host = new WebHostBuilder()
            // //     .UseKestrel()
            // //     .UseContentRoot(Directory.GetCurrentDirectory())
            // //     .UseIISIntegration()
            // //     .UseStartup<Startup>()
            // //     .Build();

            // // host.Run();

            // // CreateWebHostBuilder(args).Build().Run();
        }
        

        //   public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        // Host.CreateDefaultBuilder(args)
        //     .ConfigureWebHostDefaults(webBuilder =>
        //     {
        //         webBuilder.UseStartup<Startup>();
        //     })
        //     .ConfigureLogging(logging =>
        //     {
        //         logging.ClearProviders();
                
        //         logging.SetMinimumLevel(LogLevel.Trace);
        //     })
        // .UseNLog();
        //   // NLog: Setup NLog for Dependency injection 
    }
}
