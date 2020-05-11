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
using Logic.Logic;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos _userRepos;
        private readonly IAuthLogic _authLogic;

        public UsersController(IUserRepos userRepos, IAuthLogic authLogic)
        {
            _userRepos = userRepos;
            _authLogic = authLogic;
        }

        // GET: api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var email = claim[0].Value;
            
            var user = await _userRepos.GetByEmail(email);

            if (user == null) return NotFound();

            return user;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login ([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();

            var user = await _authLogic.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = _authLogic.GenerateJWT(user);
                response = Ok(new { token = tokenString });
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
    }
}