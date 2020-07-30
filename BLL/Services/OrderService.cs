using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using Common.Entitites;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }


        //Start of ShoppingCart Logics
        public async Task AddToCart(ProductDTO cartItem, string Id)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            //var book = mapper.Map<BookDTO, Book>(Database.Books.Get(cartItem.Id));
            Product product = new Product()
            {
                ProductId = cartItem.ProductId,
                Name = cartItem.Name,
                Description = cartItem.Description,
                Price = cartItem.Price
            };
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>().ForMember(dt)
            //Database.Carts.AddToCart(book);
            await Database.Carts.AddToCart(product, Id);
            await Database.SaveAsync();

        }

        public async Task<List<CartItemDTO>> GetAllCartItems(string Id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CartItem, CartItemDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<CartItem>, List<CartItemDTO>>(await Database.Carts.GetAll(Id));
        }
      

        public void RemoveFromCart(int id, string CartId)
        {

            Database.Carts.Remove(id, CartId);
            Database.Save();

        }

        public async Task<decimal> GetTotal(string CartId)
        {
            var cartItems = await Database.Carts.GetAll(CartId);
            decimal total = cartItems.Select(c => c.Product.Price * c.Count).Sum();
            return total;
        }
        public async Task EmptyCart(string CartId)
        {
            var cartItems = await Database.Carts.GetAll(CartId);
            await Database.Carts.Empty(CartId);
            await Database.SaveAsync();
        }
        public async Task MigrateCart(string userName, string CartId)
        {
            await Database.Carts.MigrateCart(userName, CartId);
            await Database.SaveAsync();
        }


        // Orders logic
        public async Task CreateNewOrder(OrderDTO orderDTO, string CartId)
        {
            //if (IsAnyNullOrEmpty(orderDTO))
            //{
            //    throw new ValidationException("Все поля должны быть заполнены", "");
            //}
            var order = new Order
            {
                OrderDate = orderDTO.OrderDate,
                UserName = orderDTO.UserName,
                FirstName = orderDTO.FirstName,
                LastName = orderDTO.LastName,
                Address = orderDTO.Address,
                City = orderDTO.City,
                Phone = orderDTO.Phone,
                Email = orderDTO.Email
            };
            //try
            //{
            await Database.Carts.CreateOrder(order, CartId);
            await Database.SaveAsync();
            //}
            //catch (Exception)
            //{
            //    throw new ValidationException("Email указан некорректно", "Email");
            //}

        }
        private bool IsAnyNullOrEmpty(OrderDTO order)
        {
            foreach (PropertyInfo pi in order.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(order);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
