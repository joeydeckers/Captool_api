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
using System.Web.Helpers;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos _userRepos;
        private readonly IAuthLogic _authLogic;
        private readonly IUserLogic _userLogic;

        public UsersController(IUserRepos userRepos, IAuthLogic authLogic, IUserLogic userLogic)
        {
            _userRepos = userRepos;
            _authLogic = authLogic;
            _userLogic = userLogic;
        }

        // GET: api/Users/
        [Authorize]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetUser()
        {
            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);

            if (user == null) return NotFound();

            return user;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login ([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();

            var user = await _authLogic.GenerateJWT(login);

            if (user != null)
            {
                response = Ok(new { token = user.Token });
            }

            return response;
        }

        // POST: api/Users/PostUser
        [HttpPost("[action]")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            IActionResult response = Unauthorized();

            await _userRepos.Add(user);

            await _authLogic.GenerateJWT(new LoginViewModel() { Email = user.Email, Password = user.Password });

            if (user != null)
            {
                response = Ok(new { token = user.Token });
            }

            return response;
        }

        [Authorize]
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User userChanges)
        {

            var user = await _authLogic.GetUserFromToken(HttpContext.User.Identity as ClaimsIdentity);
            if (user == null) return Unauthorized();

            await _userLogic.UpdateUser(user, userChanges);

            LoginViewModel newData = new LoginViewModel()
            {
                Email = userChanges.Email,
                Password = userChanges.Password
            };

            return RedirectToAction("Login", newData);
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