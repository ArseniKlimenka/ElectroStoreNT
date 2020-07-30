using Common.Entitites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class EfDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFdbContext> //DropCreateDatabaseIfModelChanges<EFdbContext>
    {
        protected override void Seed(EFdbContext context)
        {
            context.Products.Add(new Product { ProductId = 17, Name = "Qas", Description = "Hui", Category = "Драма", Price = 233 });
            context.Products.Add(new Product { ProductId = 18, Name = "Qas", Description = "Hui", Category = "Боевик", Price = 27 });
            context.Products.Add(new Product { ProductId = 19, Name = "Qas", Description = "Hui", Category = "Фэнтези", Price = 233 });
            context.Products.Add(new Product { ProductId = 20, Name = "Qas", Description = "Hui", Category = "Смех", Price = 73 });
            context.Products.Add(new Product { ProductId = 21, Name = "Qas", Description = "Hui", Category = "Драма", Price = 12 });

         context.CartItems.Add(new CartItem { CartId = "1", CartItemId = 1, Count = 1, Product = new Product { Name = "Qas" }, ProductId = 1 });
            context.SaveChanges();
        }
    }
}