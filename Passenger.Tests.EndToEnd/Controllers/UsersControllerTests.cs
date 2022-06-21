using System.Net.Http;
using Passenger.Api;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Passenger.Infrastructure.DTO;
using System.Net;
using System.Text;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Tests.EndToEnd.Controllers 
{
    public class UsersControllerTests : ControllerTestsBase
    {
    
        // [Fact]
        // public async Task given_valid_email_user_should_exist()
        // {
        //     var email ="user1@test.com";
        //     var user = await GetUserAsync(email);

        //     Assert.Equal(user.Email, email);  
            
        // }


        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email ="user100@email.com";
            var response = await Client.GetAsync($"users/{email}");


            Assert.Equal(HttpStatusCode.NotFound,
                            response.StatusCode);   
        }    

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
           
            var command = new CreateUser 
            {
                Email = "test@email.com",
                Username = "test",
                Password = "secret"
            };
            var payload = GetPayLoad(command);
            var response = await Client.PostAsync("users", payload);

            Assert.Equal(HttpStatusCode.Created,
                            response.StatusCode); 

            var user = await GetUserAsync(command.Email);

            Assert.Equal(user.Email,command.Email);   
            Assert.Equal(response.Headers.Location.ToString(), $"users/{command.Email}")  ;
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
             var response = await Client.GetAsync($"users/{email}");
             var responseString = await response.Content.ReadAsStringAsync();
             
             return JsonConvert.DeserializeObject<UserDto>(responseString);

        }


       
    }
}