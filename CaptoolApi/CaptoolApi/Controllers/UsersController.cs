using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptoolApi.ViewModels;
using Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos _userRepos;
        private IConfiguration _config;

        public UsersController(IUserRepos userRepos, IConfiguration config)
        {
            _userRepos = userRepos;
            _config = config;
        }

        // GET: api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int? id)
        {
            var user = await _userRepos.GetAsync(id);

            if (user == null) return NotFound();

            return user;
        }

        // GET: api/Users/Login
        [HttpGet("login")]
        public IActionResult Login(string email, string password)
        {
            LoginViewModel login = new LoginViewModel();
            login.Email = email;
            login.Password = password;
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }

        // POST: api/Users/PostUser
        [HttpPost("[action]")]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            return await _userRepos.Add(user);
        }

        // GET: api/Users/email
        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<bool>> IsEmailAvailable(string email)
        {
            return await _userRepos.IsEmailAvailable(email);
        }

        // DELETE: api/Users/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int? id)
        {
            await _userRepos.Delete(id);
            return NoContent();
        }

        private LoginViewModel AuthenticateUser(LoginViewModel login)
        {
            LoginViewModel user = null;
            //static info
            if (login.Email == "test@test.test" && login.Password == "test")
            {
                user = new LoginViewModel { Email = "test@test.test", Password = "test" };

            }
            return user;
        }

        private string GenerateJSONWebToken(LoginViewModel userinfo)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        // PUT: api/Users/id
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int? id, User user)
        //{
        //    await _userRepos.UpdateAsync(user);
        //    return NoContent();
        //}
    }
}