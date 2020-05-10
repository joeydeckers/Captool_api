using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLayer.ViewModels;
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
using Logic;
using Logic.Logic;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos _userRepos;
        private IConfiguration _config;
        private IAuthLogic _Authlogic;

        public UsersController(IUserRepos userRepos, IConfiguration config, IAuthLogic authLogic)
        {
            _userRepos = userRepos;
            _config = config;
            _Authlogic = authLogic;
        }

        // GET: api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int? id)
        {
            var user = await _userRepos.GetAsync(id);

            if (user == null) return NotFound();

            return user;
        }

        // POST: api/Users/Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = _Authlogic.GenerateJWT(loginViewModel.Email, loginViewModel.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
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

        //private async LoginViewModel AuthenticateUser(LoginViewModel login)
        //{
        //    var user = await _userRepos.Login(login.Email, login.Password);

        //    if (user == null) return NotFound();

        //    return user;
        //    //LoginViewModel user = null;
        //    ////static info
        //    //if (login.Email == "test@test.test" && login.Password == "test")
        //    //{
        //    //    user = new LoginViewModel { Email = "test@test.test", Password = "test" };
        //    //}
        //    //return user;
        //}
    }
}