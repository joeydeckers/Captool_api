using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Helpers;
using Interfaces.UserInterfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;

namespace Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly AppSettings _appsettings;

        public UserLogic(IOptions<AppSettings> appSettings)
        {
            _appsettings = appSettings.Value;
        }

        private List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Test", Email = "test@test.test.nl", Password = "test"}
        };

        public User Authenticate(string email, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == email && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
