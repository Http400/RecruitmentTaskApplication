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
        UserDTO GetUser(int id);
        void EditUser(int id, UserDTO userData);
        void ChangePassword(int userId, string oldPassword, string newPassword);
    }
}
