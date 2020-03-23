using Data;
using Interfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public DbSet<User> GetAll()
        {
            return _context.Users;
        }

        public User Update(User userChanges)
        {
            throw new NotImplementedException();
        }
    }
}
