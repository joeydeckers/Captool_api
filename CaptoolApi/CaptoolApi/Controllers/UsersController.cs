﻿using System;
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

namespace CaptoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos _userRepos;

        public UsersController(IUserRepos userRepos)
        {
            _userRepos = userRepos;
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
        [HttpPost("[action]")]
        public async Task<ActionResult<User>> Login([FromBody] LoginViewModel viewModel)
        {
            var user = await _userRepos.Login(viewModel.Email, viewModel.Password);

            if (user == null) return NotFound();

            return user;
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

        // PUT: api/Users/id
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int? id, User user)
        //{
        //    await _userRepos.UpdateAsync(user);
        //    return NoContent();
        //}
    }
}