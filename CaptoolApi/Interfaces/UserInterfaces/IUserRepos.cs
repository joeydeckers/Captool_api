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
        Task<List<User>> GetAll();
        Task<User> GetAsync(int? id);
        Task<User> Login(string email, string password);
        Task<bool> IsEmailAvailable(string email);
        Task<User> Add(User user);
        Task Delete(int? id);
        Task UpdateAsync(User userChanges);
    }
}
