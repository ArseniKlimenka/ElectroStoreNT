using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using Common.Entitites;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork Database { get; set; }

        public ProductService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
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
    }
}
