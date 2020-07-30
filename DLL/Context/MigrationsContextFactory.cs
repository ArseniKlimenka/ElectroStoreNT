using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class MigrationsContextFactory : IDbContextFactory<EFdbContext>
    {
        public EFdbContext Create()
        {
            return new EFdbContext("EFdbContext");
        }
    }
}
