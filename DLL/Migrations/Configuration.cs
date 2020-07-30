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
            context.Products.AddOrUpdate(new Product { ProductId = 17, Name = "Qas", Description = "Hui", Category = "Драма", Price = 233 });
            context.Products.AddOrUpdate(new Product { ProductId = 18, Name = "Qas", Description = "Hui", Category = "Боевик", Price = 27 });
            context.Products.AddOrUpdate(new Product { ProductId = 19, Name = "Qas", Description = "Hui", Category = "Фэнтези", Price = 233 });
            context.Products.AddOrUpdate(new Product { ProductId = 20, Name = "Qas", Description = "Hui", Category = "Смех", Price = 73 });
            context.Products.AddOrUpdate(new Product { ProductId = 21, Name = "Qas", Description = "Hui", Category = "Драма", Price = 12 });
        }
    }
}
