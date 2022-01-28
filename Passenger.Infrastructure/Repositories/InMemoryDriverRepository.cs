using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {

        public static ISet<Driver> _drivers = new HashSet<Driver>();
        
        

        public async Task<Driver> GetAsync(Guid userId)
            =>  await Task.FromResult (_drivers.SingleOrDefault(x => x.UserId == userId));
            public async Task AddAsync(Driver driver)
        {
            await Task.CompletedTask;
            _drivers.Add(driver);
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
            => await Task.FromResult (_drivers);

        public async Task UpdateAsync(Driver driver)
        {
            await Task.CompletedTask;
        }
    }
}