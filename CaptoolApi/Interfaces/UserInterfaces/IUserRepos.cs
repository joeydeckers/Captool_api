using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;

namespace Interfaces.UserInterfaces
{
    public interface IUserRepos
    {
        Task<User> Add(User user);
        Task Delete(int id);
        Task<List<User>> GetAll();
        Task<User> GetAsync(int id);
        Task UpdateAsync(User userChanges);
        Task SaveAsync();
    }
}
