using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Settings;
using Passenger.Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace Passenger.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {   
        private readonly JwtSettings _settings;
        
        // public JwtHandler(JwtSettings settings)
        // {
        //     _settings = settings;
        // }
         private readonly IConfiguration _configuration;
         private readonly IOptions<JwtSettings> config;

         private readonly JwtSettings _jwtSettings;

          public JwtHandler(IOptions<JwtSettings> settings)
        {
            _jwtSettings = settings.Value;
        }

        //  public JwtHandler(IConfiguration Configuration)
        // {
        //      _configuration = Configuration;
             
        // }
        
        

        public JwtDto CreateToken(Guid userId, string role)
        {
           var now = DateTime.UtcNow;
           
           var keygood = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
          
            var minutes3 = Convert.ToDouble(_jwtSettings.ExpiryMinutes);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(), ClaimValueTypes.Integer64)

            };
            
            var expiry = now.AddMinutes(minutes3);
            var signingCredentials = new SigningCredentials(keygood,
            SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expiry,
                signingCredentials: signingCredentials

            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expiry = expiry.ToTimestamp()
            };

        }
    }
}