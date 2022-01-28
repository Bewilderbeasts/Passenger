using System;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        
        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public DriverDto Get(Guid userId)
        {
            var driver = _driverRepository.GetAsync(userId);
            return new DriverDto
            {
                //ID
            };
        }

        public Task<DriverDto> GetAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}