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

namespace Logic.Logic
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IConfiguration _config;
        private readonly IUserRepos _userRepos;

        public AuthLogic(IConfiguration config, IUserRepos userRepos)
        {
            _config = config;
            _userRepos = userRepos;
        }

        public string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
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
