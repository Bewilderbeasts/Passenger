using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {

        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, 
                     IMapper mapper)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitide, double endLongitude)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with user ID: '{userId}' was not found.");
            }
            var start = Node.Create("Start address", startLongitude, startLatitude);
            var end = Node.Create("End address", endLongitude, endLatitide);
            driver.AddRoute(name, start, end);
            await _driverRepository.UpdateAsync(driver);

        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with user ID: '{userId}' was not found.");
            }
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}