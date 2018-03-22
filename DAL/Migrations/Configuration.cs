namespace DAL.Migrations
{
    using Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.MyDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.MyDatabaseContext context)
        {
            context.UserSet.AddOrUpdate(n => n.Name, new User[]
            {
                new User()
                {
                    Name = "John",
                    Surname = "Doe",
                    Address = "New York",
                    PhoneNumber = "123124213"
                },
                new User()
                {
                    Name = "Jane",
                    Surname = "Smith",
                    Address = "Washington",
                    PhoneNumber = "346545433"
                }
            });
        }
    }
}
