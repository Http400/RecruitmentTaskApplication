using DAL.Entities;
using System.Collections.Generic;
using System.Security.Principal;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IUserService
    {
        string ValidateUser(string email, string password);
        User CreateUser(SignUpDTO signUpData);
        int GetUserId(IIdentity identity);
        List<UserDTO> GetUsers();
    }
}
