using System;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using System.Collections.Generic;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
    
        
        public DriverService(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);
            
            return _mapper.Map<Driver, DriverDto>(driver);
        }

        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id: {userId} was not found.");
            }
            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new Exception($"Driver with id: {userId} already exists.");
            }
            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task SetVechicleAsync(Guid userId, string brand, string name, int seats)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with id: {userId} was not found.");
            }
            driver.SetVechicle(brand, name, seats);
            await _driverRepository.UpdateAsync(driver);
        }
    }
       
        
}