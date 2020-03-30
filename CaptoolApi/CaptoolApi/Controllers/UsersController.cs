using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos _userRepos;

        public UsersController(IUserRepos userRepos)
        {
            _userRepos = userRepos;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userRepos.GetAll();
        }

        // GET: api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int? id)
        {
            var user = await _userRepos.GetAsync(id);

            if (user == null) return NotFound();

            return user;
        }

        public class LoginViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        // GET: api/Users/email/password
        //[HttpPost("[action]")]
        //public async Task<ActionResult<User>> Login([FromForm] string email, string password)
        //{
        //    var user = await _userRepos.Login(email, password);

        //    if (user == null) return NotFound();

        //    return user;
        //}

        //[HttpPost("[action]")]
        //public ActionResult Test()
        //{
        //    return Content("test");
        //}

        // GET: api/Users/email
        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<bool>> IsEmailAvailable(string email)
        {
            return await _userRepos.IsEmailAvailable(email);
        }

        // POST: api/Users
        [HttpPost("[action]")] // api/Users/PostUser
        public async Task<ActionResult<User>> PostUser([FromForm] User user)
        {
            return await _userRepos.Add(user);
        }

        // DELETE: api/Users/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int? id)
        {
            await _userRepos.Delete(id);
            return NoContent();
        }

        // PUT: api/Users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int? id, User user)
        {
            await _userRepos.UpdateAsync(user);
            return NoContent();
        }
    }
}