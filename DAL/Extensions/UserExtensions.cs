using DAL.Entities;
using DAL.Repositories;
using System.Linq;

namespace DAL.Extensions
{
    public static class UserExtensions
    {
        public static User GetSingleByEmail(this IEntityBaseRepository<User> userRepository, string email)
        {
            return userRepository.GetAll().FirstOrDefault(u => u.Email == email);
        }
    }
}
