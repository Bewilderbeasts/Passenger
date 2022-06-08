using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(IUserService userService, IDriverService driverService, 
                    IDriverRouteService driverRouteService,
                    ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if(users.Any())
            {
                return; 
            }
            
            _logger.LogTrace("Initializing data...");
            var tasks = new List<Task>();
            for(var i = 1; i <=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"userz{i}";

                //_logger.LogTrace($"Created a new user: '{username}'.");
                await _userService.RegisterAsync(userId, $"user{i}@test.com",
                    username, "secret", "user");
                _logger.LogTrace($"Adding user: '{username}'.");
                
                await _driverService.CreateAsync(userId);
                await _driverService.SetVehicle(userId, "BMW", "i8");
                _logger.LogTrace($"Created a new driver for: '{username}'.");

                await _driverRouteService.AddAsync(userId, "Default route", 1,1,2,2);
                await _driverRouteService.AddAsync(userId, "Work route", 4,6,7,8);

                _logger.LogTrace($"Created a new route for: '{username}'.");      
            }

             for (var i = 1; i <=3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                _logger.LogTrace($"Created a new admin: '{username}'.");
                await _userService.RegisterAsync(userId, $"admin{i}@test.com",
                    username, "secret", "admin");
            }
            await Task.WhenAll(tasks);
            _logger.LogTrace("Data initialized.");
        }
    }
}