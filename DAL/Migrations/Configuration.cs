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
                    Email = "john@doe.com",
                    Name = "John",
                    Surname = "Doe",
                    Address = "New York",
                    PhoneNumber = "123124213",
                    HashedPassword ="e2ii9sAyDvCh6D3A1Xyu9J43RDdRwJwxBYGQHJQs0H8=",
                    Salt = "JBVTvBeIiaW+98MhrfxXvg==",
                },
                new User()
                {
                    Email = "jane@smith.com",
                    Name = "Jane",
                    Surname = "Smith",
                    Address = "Washington",
                    PhoneNumber = "346545433",
                    HashedPassword ="e2ii9sAyDvCh6D3A1Xyu9J43RDdRwJwxBYGQHJQs0H8=",
                    Salt = "JBVTvBeIiaW+98MhrfxXvg==",
                }
            });
        }
    }
}
