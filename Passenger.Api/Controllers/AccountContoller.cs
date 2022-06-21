using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("account")] 
    public class AccountContoller : ApiControllerBase
    {

        private readonly IJwtHandler _jwtHandler;
        public AccountContoller(ICommandDispatcher commandDispatcher,
        IJwtHandler jwtHandler) 
            : base(commandDispatcher)
        {         
            _jwtHandler = jwtHandler;
        }
        
         [HttpPut]
         [Route("password")]
         public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
         {
            await DispatchAsync(command);            
         
            return NoContent();
         }


    }
}