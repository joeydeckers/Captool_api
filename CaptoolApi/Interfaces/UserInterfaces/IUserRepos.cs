using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;

namespace Interfaces.UserInterfaces
{
    public interface IUserRepos
    {
        User Add(User user);
        User Delete(User user);
        DbSet<User> GetAll();
        User Get(int? id);
        User Update(User userChanges);
        User TryLogin(string email, string password);
    }
}
