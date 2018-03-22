using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        MyDatabaseContext dbContext;

        public MyDatabaseContext Init()
        {
            return dbContext ?? (dbContext = new MyDatabaseContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
