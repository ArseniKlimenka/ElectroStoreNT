using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Context;
using Common.Entitites;
using DLL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DLL.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private EFdbContext db;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ApplicationUserManager userManager;
        private ICartRepository _cartRepository;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public UnitOfWorkRepository(string connectionString)
        {
            db = new EFdbContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }
        public IProductRepository Products
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(db);
                return _productRepository;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(db);
                return _categoryRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public ICartRepository Carts
        {
            get
            {
                if (_cartRepository == null)
                    _cartRepository = new CartRepository(db);
                return _cartRepository;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
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
