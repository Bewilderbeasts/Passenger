using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Api.Controllers
{
    public class DriverController : ApiControllerBase
    {

        public DriverController(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher)
        {         
        }


         [HttpPost]

        public async Task<IActionResult> Put([FromBody]CreateDriver command)
         {
            await CommandDispatcher.DispatchAsync(command);            
         
            return NoContent();
         }
    }
}