using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using Common.Entitites;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        IUnitOfWork Database { get; set; }

        public CartService(IUnitOfWork uow)
        {
            Database = uow;
        }

        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(CartLineDTO cartLineDTO, int quantity)
        {
          
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public decimal ComputeTotalValue()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<CartLineDTO> GetCarts()
        {
            throw new NotImplementedException();
        }

        public ProductDTO GetProduct(int? id)
        {
            var product = Database.Products.Get(id.Value);


            return new ProductDTO { Category = product.Category, ProductId = product.ProductId, Name = product.Name, Price = product.Price };
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(Database.Products.GetAll());
        }

        public void RemoveLine(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public void AddItem(ProductDTO product, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
