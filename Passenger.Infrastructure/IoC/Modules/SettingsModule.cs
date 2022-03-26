using Autofac;
using Passenger.Infrastructure.Settings;
using Passenger.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
                builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                        .SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                        .SingleInstance();        
        }
    }
}