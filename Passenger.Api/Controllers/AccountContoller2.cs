using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    
    [Microsoft.AspNetCore.Mvc.Route("Ackount")] 
    public class AccountContoller2 : ApiControllerBase
    {

        private readonly IJwtHandler _jwtHandler;
        public AccountContoller2(ICommandDispatcher commandDispatcher,
        IJwtHandler jwtHandler) 
            : base(commandDispatcher)
        {         
            _jwtHandler = jwtHandler;
        }
        
        // [Microsoft.AspNetCore.Mvc.Route("")]
        // [HttpGet]
        // [Microsoft.AspNetCore.Mvc.Route("token")]
        // public IActionResult Get()
        //  {
        //     var token = _jwtHandler.CreateToken("user1@gmail.com", "user");            
         
        //     return Json(token);
        //  }


        


    //    
    //     [Microsoft.AspNetCore.Mvc.Route("Index")]
    //         public ActionResult Index()
    //         {
    //             return new EmptyResult();
    //         }
    //     [HttpGet]
    //     [Microsoft.AspNetCore.Mvc.Route("Default/GetRecordsById/{id}")]
    //         public string GetRecordsById(int id)
    //         {
    //             string str = string.Format("The id passed as parameter is: {0}", id);
    //             //return ok(str);
    //             return str;
    //         }


    }
}