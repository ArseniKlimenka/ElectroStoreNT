using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class EFdbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public EFdbContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}

