using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos _userRepo;

        public UsersController(IUserRepos userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userRepo.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepo.Get(id);

            if (user == null) return NotFound();

            return user;
        }

        // GET: api/Users/email/password
        [HttpGet("{email}/{password}")]
        public ActionResult<User> Login(string email, string password)
        {
            var user = _userRepo.TryLogin(email, password);
                
            if (user == null) return NotFound();

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _userRepo.Add(user);
            // await _userRepo.SaveChangesAsync(); 

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int? id)
        {
            var user = _userRepo.Get(id);

            if (user == null) return NotFound();
            
            _userRepo.Delete(user);
            // await _userRepo.SaveChangesAsync();

            return user;
        }
    }
}