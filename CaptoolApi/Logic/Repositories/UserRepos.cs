using Data;
using Interfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ModelLayer.ViewModels;
using System.Web.Helpers;

namespace Logic.Repositories
{
    public class UserRepos : IUserRepos
    {
        private readonly AppDbContext _context;

        public UserRepos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(int? id)
        {
            return await _context.ct_user.FindAsync(id);
        }


        public async Task<bool> IsEmailAvailable(string email)
        {
            var user = await _context.ct_user.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return true;

            return false;
        }

        public async Task<string> Add(User user)
        {
            string nothashed = user.Password;


            var newuser = user;
            newuser.Password = user.Password;
            newuser.Password = Crypto.HashPassword(newuser.Password);
            await _context.ct_user.AddAsync(newuser);
            await SaveAsync();

            return nothashed;
        }

        public async Task Delete(int? id)
        {
            var user = await _context.ct_user.FindAsync(id);
            if (user != null)
            {
                _context.ct_user.Remove(user);
                await SaveAsync();
            }
        }

        public async Task UpdateAsync(User userChanges)
        {

            var user = _context.Set<User>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(userChanges.Id));

            User newuser = new User()
            {
                Id = user.Id,
                Name = userChanges.Name,
                Email = userChanges.Email,
                Password = userChanges.Password,
                Playlist = userChanges.Playlist
            };

            if (user != null)
            {
                _context.Entry(user).State = EntityState.Detached;
                var updated = _context.ct_user.Attach(newuser);
                _context.Entry(newuser).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.ct_user.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
