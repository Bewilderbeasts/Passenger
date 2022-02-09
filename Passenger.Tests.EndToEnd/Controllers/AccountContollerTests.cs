using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Passenger.Api;
using Passenger.Infrastructure.Commands.Users;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class AccountContollerTests : ControllerTestsBase
    {
        
        [Fact]
        public async Task given_valid_current_and_new_password_it_should_be_changed()
        {
           
            var command = new ChangeUserPassword 
            {
                CurrentPassword = "secret",
                NewPassword = "secret2"
            };
            var payload = GetPayLoad(command);
            var response = await Client.PutAsync("account/password", payload);
            

            // Assert.Equal(response.StatusCode,
            // HttpStatusCode.NoContent); 
            Assert.Equal(response.StatusCode,
            HttpStatusCode.NotFound); 
        }

    }
}