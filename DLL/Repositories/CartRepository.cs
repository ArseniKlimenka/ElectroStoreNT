using Common.Entitites;
using DLL.Context;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private EFdbContext db;
        public CartRepository(EFdbContext context)
        {
            this.db = context;
        }

        public async Task AddToCart(Product product, string Id)
        {
            var cartItem = await db.CartItems.SingleOrDefaultAsync(
                c => c.CartId == Id
                && c.ProductId == product.ProductId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                   ProductId = product.ProductId,
                    CartId = Id,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                db.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }

        }
        //Remove All CartItems From Db 
        public async Task Empty(string CartId)
        {
            var cartItems = await db.CartItems.Where(c => c.CartId == CartId).Include(b => b.Product).ToArrayAsync();
            db.CartItems.RemoveRange(cartItems);
        }

        public Task<List<CartItem>> GetAll(string Id)
        {
            return db.CartItems.Where(c => c.CartId == Id).Include(b => b.Product).ToListAsync();
        }

        public void Remove(int id, string CartId)
        {
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.CartId == CartId && cart.CartItemId == id);
            var itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
            }

        }
        public async Task CreateOrder(Order order, string CartId)
        {

            decimal orderTotal = 0;
            var cartItems = await GetAll(CartId);
            foreach (var item in cartItems)
            {
                var product = await db.Products.SingleAsync(b => b.ProductId == item.ProductId);
                var orderDetail = new OrderDetail
                {
                   ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = product.Price,
                    Quantity = item.Count
                };
                orderTotal += (item.Count * product.Price);
                db.OrderDetails.Add(orderDetail);

            }
            order.Total = orderTotal;
            db.Orders.Add(order);
            await Empty(CartId);

        }

        public async Task MigrateCart(string userName, string CartId)
        {
            var cartItems = await db.CartItems.Where(c => c.CartId == CartId).ToListAsync();
            foreach (var item in cartItems)
            {
                item.CartId = userName;
            }
        }



    }
}
