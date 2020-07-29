using Common.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCart(Product product, string Id);
        Task<List<CartItem>> GetAll(string Id);
        void Remove(int id, string CartId);
        Task Empty(string CartId);
        Task CreateOrder(Order order, string CartId);
        Task MigrateCart(string userName, string CartId);
    }
}
