using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(IUserService userService, IDriverService driverService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _driverService = driverService;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializing data...");
            var tasks = new List<Task>();
            for (var i = 1; i <=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                _logger.LogTrace($"Created a new user: '{username}'.");
                tasks.Add(_userService.RegisterAsync(userId, $"{username}@test.com",
                    username, "secret", "user"));
                tasks.Add(_driverService.CreateAsync(userId));
                tasks.Add(_driverService.SetVechicleAsync(userId, "BMW", "i8", 5));
                _logger.LogTrace($"Created a new driver for: '{username}'.");    
            }

             for (var i = 1; i <=3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                _logger.LogTrace($"Created a new admin: '{username}'.");
                tasks.Add(_userService.RegisterAsync(userId, $"{username}@test.com",
                    username, "secret", "admin"));
            }
            await Task.WhenAll(tasks);
            _logger.LogTrace("Data initialized.");
        }
    }
}