using System;
using Passenger.Infrastructure.Commands.Drivers.Models;
using static Passenger.Infrastructure.Commands.Drivers.CreateDriver;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class UpdateDriver : AuthenticatedCommandBase
    {
        public DriverVehicle Vehicle {get; set;}
    }
}
