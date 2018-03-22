using DAL.Configurations;
using DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL
{
    public partial class MyDatabaseContext : DbContext
    {
        public IDbSet<User> UserSet { get; set; }

        public MyDatabaseContext() : base("Name=MyDatabaseContext")
        {
            Database.SetInitializer<MyDatabaseContext>(null);
        }

        public virtual void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
