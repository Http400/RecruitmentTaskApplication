using DAL.Entities;

namespace DAL.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Email).IsRequired().HasMaxLength(200);
        }
    }
}
