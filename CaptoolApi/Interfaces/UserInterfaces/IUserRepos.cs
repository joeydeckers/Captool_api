using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using ModelLayer.ViewModels;

namespace Interfaces.UserInterfaces
{
    public interface IUserRepos
    {
        Task<User> GetAsync(int? id);
        User Login(LoginViewModel login);
        User GetByEmail(string email);
        Task<bool> IsEmailAvailable(string email);
        Task<User> Add(User user);
        Task Delete(int? id);
        Task UpdateAsync(User userChanges);
    }
}
