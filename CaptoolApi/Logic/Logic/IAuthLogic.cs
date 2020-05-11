using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public interface IAuthLogic
    {
        string GenerateJWT(User user);
        Task<User> AuthenticateUser(LoginViewModel login);
    }
}
