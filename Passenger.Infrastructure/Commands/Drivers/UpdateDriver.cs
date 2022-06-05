using System;
using static Passenger.Infrastructure.Commands.Drivers.CreateDriver;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class UpdateDriver : AuthenticatedCommandBase
    {
        public DriverVehicle Vehicle {get; set;}
    }
}
