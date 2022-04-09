using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class DriverController : ApiControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(ICommandDispatcher commandDispatcher, IDriverService driverService) 
            : base(commandDispatcher)
        {         
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
         {
            var drivers = await _driverService.BrowseAsync();            
         
            return Json(drivers);
         }



        [HttpPost]

        public async Task<IActionResult> Put([FromBody]CreateDriver command)
         {
            await CommandDispatcher.DispatchAsync(command);            
         
            return NoContent();
         }
    }
}