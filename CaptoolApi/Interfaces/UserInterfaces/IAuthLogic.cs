using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.UserInterfaces
{
     public interface IAuthLogic
    {
        User GenerateJWT(string email, string password);

        User AuthenticateUser(LoginViewModel login);
    }
}
