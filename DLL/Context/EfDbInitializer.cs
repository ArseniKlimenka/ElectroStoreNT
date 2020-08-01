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
            context.Products.Add(new Product { ProductId = 17, Name = "Hello", Description = "Hello", CategoryId = 1, Price = 233 });
            context.Products.Add(new Product { ProductId = 18, Name = "Hello", Description = "Hello", CategoryId = 1, Price = 27 });
            context.Products.Add(new Product { ProductId = 19, Name = "Hello", Description = "Hello", CategoryId = 1, Price = 233 });
            context.Products.Add(new Product { ProductId = 20, Name = "Hello", Description = "Hello", CategoryId = 1, Price = 73 });
            context.Products.Add(new Product { ProductId = 21, Name = "Hello", Description = "Hello", CategoryId = 1, Price = 12 });

            context.Categories.Add(new Category { Id = 1, CategoryName = "Классика" });
            context.Categories.Add(new Category { Id = 2, CategoryName = "Фентези" });
            context.Categories.Add(new Category { Id = 3, CategoryName = "Ужасы" });
            context.Categories.Add(new Category { Id = 4, CategoryName = "Драмма" });
            context.Categories.Add(new Category { Id = 5, CategoryName = "Комедия" });
            base.Seed(context);
            
        }
    }
}