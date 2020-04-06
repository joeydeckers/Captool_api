using Data;
using Interfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<User> Login(string email, string password)
        {
            return await _context.ct_user.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<bool> IsEmailAvailable(string email)
        {
            var user = await _context.ct_user.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return true;

            return false;
        }

        public async Task<User> Add(User user)
        {
            await _context.ct_user.AddAsync(user);
            await SaveAsync();
            return user;
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
            var user = _context.ct_user.Attach(userChanges);
            user.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
