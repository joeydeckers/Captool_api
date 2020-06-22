using Interfaces.UserInterfaces;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Logic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepos _userRepos;

        public UserLogic(IUserRepos userRepos)
        {
            _userRepos = userRepos;
        }

        public async Task UpdateUser(User oldUser, User newUser)
        {
            oldUser.Name = newUser.Name;
            oldUser.Email = newUser.Email;
            oldUser.Playlist = newUser.Playlist;
            if (Crypto.VerifyHashedPassword(oldUser.Password, newUser.Password))
            {
                oldUser.Password = Crypto.HashPassword(newUser.Password);
            }


            await _userRepos.UpdateAsync(oldUser);
        }
    }
}
