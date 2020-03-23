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
        User Delete(int id);
        DbSet<User> GetAll();
        User Get(int id);
        User Update(User userChanges);
    }
}
