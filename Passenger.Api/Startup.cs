using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Diagnostics;
using Passenger.Infrastructure.Extensions;
//using NLog.Extensions.Logging;
using NLog.Web;
using Passenger.Infrastructure.Mongo;

namespace Passenger.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer {get; private set;}
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

         public void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddNLog("nlog.config");
            logging.AddNLogWeb();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(x => x.JsonSerializerOptions.WriteIndented = true);
            services.AddOptions();
            
            //services.AddRazorPages();
            
    
            
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.Configure<GeneralSettings>(Configuration.GetSection("General")) ;

            var sp = services.BuildServiceProvider();
            var jwtSettings = sp.GetService<JwtSettings>();
            

            
            var signingKey = Configuration.GetSection("JwtSettings:Key").Value;
            var issuer = Configuration.GetSection("JwtSettings:Issuer").Value;

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            }
            );

            services.AddAuthentication(option =>
           {
               option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

           })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                    };
                });

          
            //ApplicationContainer = builder.Build();

            //return new AutofacServiceProvider(ApplicationContainer);
   
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new ContainerModule(Configuration));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {

        MongoConfigurator.Initialize();

          var generalSettings = Configuration.GetSettings<GeneralSettings>();
            if (generalSettings.SeedData)
            {
                // app.ApplicationServices.GetService<IDataInitializer>()!.SeedAsync().Wait();
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }    
    
   
        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("account/token", "{controller=Account}/{action=Get}");
            endpoints.MapControllerRoute("account/password", "{controller=Account}/{action=Put}");
            
            endpoints.MapControllers();
        });
        
        //app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

        
           //app.UseMvc();
           var opts = new ExceptionHandlerOptions{ ExceptionHandler = ctx => Task.CompletedTask };
           app.UseExceptionHandler(opts);
           //appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
