using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.DTO;
using Passenger.Core.Domain;
using System.Linq;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {

        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRouteManager _routeManager;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository,
                     IUserRepository userRepository, 
                     IRouteManager routeManager, IMapper mapper)
        {
           
            _driverRepository = driverRepository;
            _routeManager = routeManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid userId, string name, 
                    double startLatitude, double startLongitude,
                    double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
        
            var startAddress =  await _routeManager.GetAddressAsync(startLatitude, startLongitude);
            var endAddress = await _routeManager.GetAddressAsync(endLatitude, endLongitude);
            var startNode = Node.Create("Start address",startLatitude , startLongitude );
            var endNode = Node.Create("End address", endLatitude, endLongitude);
            var distance = _routeManager.CalculateLength(startLatitude, startLongitude,
                    endLatitude, endLongitude);
            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);

        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            // if (driver == null)
            // {
            //     throw new Exception($"Driver with user ID: '{userId}' was not found.");
            // }
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}