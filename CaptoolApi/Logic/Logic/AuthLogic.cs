using Interfaces.UserInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using Interfaces;
using Helper;
using Microsoft.Extensions.Options;

namespace Logic.Logic
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IConfiguration _config;
        private readonly IUserRepos _userRepos;
        private readonly AppSettings _appSettings;

        public AuthLogic(IConfiguration config, IUserRepos userRepos, IOptions<AppSettings> appSettings)
        {
            _config = config;
            _userRepos = userRepos;
            _appSettings = appSettings.Value;
        }

        public User GenerateJWT(string email, string password)
        {
            var user = _userRepos.Login(email, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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

        public User AuthenticateUser(LoginViewModel login)
        {
            var user = _userRepos.GetByEmail(login.Email);

            if (Crypto.VerifyHashedPassword(user.Password, login.Password))
            {
                return user;
            }
            else user = null;

            return user;
        }
    }
}
