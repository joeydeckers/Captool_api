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

        public User Add(User user)
        {
            //await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public User Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User Get(int? id)
        {
            throw new NotImplementedException();
        }

        public DbSet<User> GetAll()
        {
            return _context.ct_user;
        }

        public User TryLogin(string email, string password)
        {
            //return await _context.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            throw new NotImplementedException();
        }

        public User Update(User userChanges)
        {
            throw new NotImplementedException();
        }
    }
}
