using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.UserInterfaces
{
    public interface IUserLogic
    {
        Task UpdateUser(User oldUser, User newUser);
    }
}
