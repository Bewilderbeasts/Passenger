using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer {get; private set;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IUserRepository, InMemoryUserRepository>();
            // services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddRazorPages();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            builder.RegisterModule(new SettingsModule(Configuration));
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        var loggerFactoryZ = LoggerFactory.Create(builder => builder.AddConsole());
        var loggerFactory2 = LoggerFactory.Create(builder => builder.AddDebug());
        appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());

        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
        });

           //app.UseMvc();
        }
    }
}
