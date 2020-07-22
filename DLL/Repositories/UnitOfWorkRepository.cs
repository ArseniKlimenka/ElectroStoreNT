using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Context;
using DLL.Entities;


namespace DLL.Repositories
{
    class UnitOfWorkRepository : IUnitOfWork
    {
        private EFdbContext db;
        private ProductRepository productRepository;
       

        public UnitOfWorkRepository(string connectionString)
        {
            db = new EFdbContext(connectionString);
        }
        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return productRepository;
            }
        }
      

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }    
}
