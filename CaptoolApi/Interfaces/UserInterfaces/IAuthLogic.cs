using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.UserInterfaces
{
     public interface IAuthLogic
    {
        Task<User> GenerateJWT(LoginViewModel login);
        Task<User> AuthenticateUser(LoginViewModel login);
        Task<User> GetUserFromToken(ClaimsIdentity identity);
    }
}
