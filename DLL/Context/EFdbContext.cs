using Common.Entitites;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class EFdbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public EFdbContext(string connectionString) : base(connectionString)
        {
        }
    }
}

