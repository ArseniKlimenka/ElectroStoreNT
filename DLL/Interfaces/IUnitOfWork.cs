using Common.Entitites;
using DLL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
      IProductRepository Products { get; }
        ICartRepository Carts { get; }
        ApplicationUserManager UserManager { get; }
        ICategoryRepository Categories { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
        void Save();
    }
}
