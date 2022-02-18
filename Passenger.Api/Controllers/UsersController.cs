using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

namespace Passenger.Api.Controllers
{
    
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, 
        ICommandDispatcher commandDispatcher,
        GeneralSettings settings) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult>  GetAsync(string email)
           {
              var user =  await _userService.GetAsync(email);
              if (user == null)
              {
                  return NotFound();
              }
              return Json(user);
           } 

         [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
         {

            await CommandDispatcher.DispatchAsync(command); 
            // await _userService.RegisterAsync(command.Email, command.Username, command.Password);
         
            return Created($"users/{command.Email}", new object());
         }
    }
}
