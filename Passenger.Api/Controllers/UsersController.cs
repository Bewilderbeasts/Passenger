using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> Get()
         {
            var users = await _userService.BrowseAsync(); 
            if (users == null)
            {
                return NotFound();
            }           
         
            return Json(users);
         }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
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

            await DispatchAsync(command); 
            
         
            return Created($"users/{command.Email}", null);
         }
    }
}
