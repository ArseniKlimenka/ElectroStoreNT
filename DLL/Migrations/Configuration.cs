namespace DLL.Migrations
{
    using Common.Entitites;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DLL.Context.EFdbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DLL.Context.EFdbContext context)
        {
            context.Products.AddOrUpdate(new Product { ProductId = 17, Name = "Qas", Description = "Hui", CategoryId = 1, Price = 233 });
            context.Products.AddOrUpdate(new Product { ProductId = 18, Name = "Qas", Description = "Hui", CategoryId = 2, Price = 27 });
            context.Products.AddOrUpdate(new Product { ProductId = 19, Name = "Qas", Description = "Hui", CategoryId = 3, Price = 233 });
            context.Products.AddOrUpdate(new Product { ProductId = 20, Name = "Qas", Description = "Hui", CategoryId = 4, Price = 73 });
            context.Products.AddOrUpdate(new Product { ProductId = 21, Name = "Qas", Description = "Hui", CategoryId = 1, Price = 12 });

            //Start Categories Initialize
            context.Categories.Add(new Category { Id = 1, CategoryName = "Классика" });
            context.Categories.Add(new Category { Id = 2, CategoryName = "Фентези" });
            context.Categories.Add(new Category { Id = 3, CategoryName = "Ужасы" });
            context.Categories.Add(new Category { Id = 4, CategoryName = "Драмма" });
            context.Categories.Add(new Category { Id = 5, CategoryName = "Комедия" });
        }
    }
}
