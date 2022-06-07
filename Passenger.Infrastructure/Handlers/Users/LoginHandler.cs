using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IHandler _handler;
        private readonly IMemoryCache _cache;
        public LoginHandler(IHandler handler, IUserService userService, IJwtHandler jwtHandler, IMemoryCache cache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
            _handler = handler;
        }

        public async Task HandleAsync(Login command)
            => await _handler
                .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
                .Next()
                .Run(async () => 
                {
                    await _userService.LoginAsync(command.Email, command.Password);
                    var user = await _userService.GetAsync(command.Email);
                    var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
                    _cache.SetJwt(command.TokenId, jwt);
                })
                .ExecuteAsync();
    }
}