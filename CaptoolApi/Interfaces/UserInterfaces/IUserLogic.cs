using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.UserInterfaces
{
    public interface IUserLogic
    {
        User Authenticate(string email, string password);
    }
}
