using DLL.Context;
using Common.Entitites;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    class ProductRepository : IProductRepository
    {
        private EFdbContext db;

        public ProductRepository(EFdbContext context)
        {
            db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.Include(b => b.Category).ToList();
        }

        public Product Get(int id)
        {
            return db.Products.Where(b => b.ProductId == id).Include(b => b.Category).FirstOrDefault();
        }

        public void Create(Product product)
        {
            db.Products.Add(product);
        }

        public void Update(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
        }

        public IEnumerable<Product> Find(string searchString)
        {
            return db.Products.Where(p=> p.Name.Contains(searchString)).Include(b => b.Category).ToList();
        }

        public void Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
                db.Products.Remove(product);
        }
    }

}
