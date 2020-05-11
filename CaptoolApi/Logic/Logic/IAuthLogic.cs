using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Logic
{
    public interface IAuthLogic
    {
        string GenerateJWT(User user);
        User AuthenticateUser(LoginViewModel login);
    }
}
