using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using Common.Entitites;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
           
            Product product = new Product()
            {
                ProductId = cartItem.ProductId,
                Name = cartItem.Name,
                Description = cartItem.Description,
                Price = cartItem.Price
            };
           
            await Database.Carts.AddToCart(product, Id);
            await Database.SaveAsync();

        }

        public async Task<List<CartItemDTO>> GetAllCartItems(string Id)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartItem, CartItemDTO>();
                cfg.CreateMap<Product, ProductDTO>();
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
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

            StringBuilder body = new StringBuilder()
               .AppendLine("Новый заказ обработан")
               .AppendLine("---")
               .AppendLine("Товары:");
            List<CartItem> cart = new List<CartItem>();           
            cart = await Database.Carts.GetAll(CartId);
           decimal subtotal = 0;
            foreach (var line in cart)
            {
                body.AppendFormat("{0} x {1} (итого: {2} руб.)",
                line.Count, line.Product.Name, line.Product.Price * line.Count)
                .AppendLine("");
                subtotal += line.Product.Price * line.Count;
            }

            body.AppendFormat("Общая стоимость: {0}", subtotal )
                .AppendLine("")
                .AppendLine("Доставка:")
                .AppendLine(order.FirstName)
                .AppendLine(order.LastName)
                .AppendLine(order.City)
                .AppendLine(order.Address)
                .AppendLine("---");

            MailMessage mailMessage = new MailMessage("Arseni.Klimenka@gmail.com", order.Email);
            mailMessage.Subject = "Новый заказ отправлен!";     // Тема
            mailMessage.Body = body.ToString();
            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com")) //используем сервера Google
            {

                client.Credentials = new NetworkCredential("Arseni.Klimenka@gmail.com", "PanShcadun2904"); //логин-пароль от аккаунта
                client.Port = 587; //порт 587 либо 465
                client.EnableSsl = true; //SSL обязательно
                client.Send(mailMessage);
            }


            await Database.Carts.CreateOrder(order, CartId);
            await Database.SaveAsync();

            

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
