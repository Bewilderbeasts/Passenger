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

namespace Passenger.Api
{
    public class Startup
    {
       
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

         public IConfiguration Configuration { get; }
        


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            // Add framework services.
            services.AddMvc();
            services.AddOptions();
            //services.AddRazorPages();
            

            
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            

            var sp = services.BuildServiceProvider();
            var jwtSettings = sp.GetService<JwtSettings>();
            

            
            var signingKey = Configuration.GetSection("JwtSettings:Key").Value;
            var issuer = Configuration.GetSection("JwtSettings:Issuer").Value;



            // var appSettingsSection = Configuration.GetSection("JwtSettings");


            // services.Configure<JwtSettings>(appSettingsSection); //get key from appSettings 
            // var appSettings = appSettingsSection.Get<JwtSettings>(); 
            // var key = Encoding.UTF8.GetBytes(appSettings.Key); 

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

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            //builder.RegisterModule(new SettingsModule(Configuration));
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

        
        var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();
            if (generalSettings.SeedData)
            {
                
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
    
   
        app.UseDeveloperExceptionPage();

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

        
           //app.UseMvc();
        }
    }
}
