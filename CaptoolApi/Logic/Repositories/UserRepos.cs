using Data;
using Interfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public class UserRepos : IUserRepos
    {
        private readonly AppDbContext _context;

        public UserRepos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.ct_user.AddAsync(user);
            await SaveAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            User user = await _context.ct_user.FindAsync(id);
            if (user != null)
            {
                _context.ct_user.Remove(user);
                await SaveAsync();

            }
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.ct_user.ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.ct_user.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User userChanges)
        {
            var user = _context.ct_user.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
