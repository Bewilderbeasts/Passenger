using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RouteManager : IRouteManager
    {

        private static readonly Random Random = new Random();

        public async Task<string> GetAddressAsync(double latitude, double longitude)
                => await Task.FromResult($"Sample address {Random.Next(100)}.");
        
        //public double CalculateDistance(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
                

        public double CalculateLength(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        => Random.Next(500,1000);
    }
}