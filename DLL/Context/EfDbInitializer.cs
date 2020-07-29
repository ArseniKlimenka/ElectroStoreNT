using Common.Entitites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class EfDbInitializer : DropCreateDatabaseIfModelChanges<EFdbContext>
    {
        protected override void Seed(EFdbContext context)
        {
            context.Products.Add(new Product { ProductId = 1, Name = "Qas", Description = "Hui", Category = "Драма", Price = 233 });
            context.Products.Add(new Product { ProductId = 2, Name = "Qas", Description = "Hui", Category = "Боевик", Price = 27 });
            context.Products.Add(new Product { ProductId = 3, Name = "Qas", Description = "Hui", Category = "Фэнтези", Price = 233 });
            context.Products.Add(new Product { ProductId = 4, Name = "Qas", Description = "Hui", Category = "Смех", Price = 73 });
            context.Products.Add(new Product { ProductId = 5, Name = "Qas", Description = "Hui", Category = "Драма", Price = 12 });
        }
    }
}