using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Passenger.Api.Controllers
{
        [Microsoft.AspNetCore.Mvc.Route("Acount")]   
        public class DefaultController
        {



            
        [Microsoft.AspNetCore.Mvc.Route("")]
        [Microsoft.AspNetCore.Mvc.Route("Index")]
            public ActionResult Index()
            {
                return new EmptyResult();
            }
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("Default/GetRecordsById/{id}")]
            public string GetRecordsById(int id)
            {
                string str = string.Format("The id passed as parameter is: {0}", id);
                //return ok(str);
                return str;
            }
        }
}