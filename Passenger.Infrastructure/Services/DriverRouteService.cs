using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {

        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IRouteManager _routeManager;

        public DriverRouteService(IDriverRepository driverRepository, 
                     IMapper mapper, IRouteManager routeManager)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _routeManager = routeManager;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
        
            var startAddress =  await _routeManager.GetAdressAsync(startLongitude, startLatitude);
            var endAddress = await _routeManager.GetAdressAsync(endLongitude, endLatitude);
            var startNode = Node.Create("Start address", startLongitude, startLatitude);
            var endNode = Node.Create("End address", endLongitude, endLatitude);
            var distance = _routeManager.CalculateDistance(startLatitude, startLongitude,
                    endLatitude, endLongitude);
            driver.AddRoute(name, startNode, endNode, distance);
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