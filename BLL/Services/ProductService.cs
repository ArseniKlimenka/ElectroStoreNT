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
                      
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>().ForMember(dto => dto.Category,
                src => src.MapFrom(b => b.Category.CategoryName)).ForMember(dto => dto.CategoryId,
                src => src.MapFrom(b => b.Category.Id))).CreateMapper();
            return mapper.Map<Product, ProductDTO>(Database.Products.Get(id.Value));
        }

        public IEnumerable<ProductDTO> GetProducts(string category)
        {
            IEnumerable<Product> products;
            if (category != null)
            {
                products = Database.Products.GetAll().Where(b => b.Category.CategoryName == category );

            }
            else
            {
               products =Database.Products.GetAll();

            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>().ForMember(dto => dto.Category,
                src => src.MapFrom(b => b.Category.CategoryName))).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }
        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>().ForMember(dto => dto.Category,
                src => src.MapFrom(b => b.Category.CategoryName))).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(Database.Products.GetAll());
        }
        public IEnumerable<ProductDTO> FindProducts(string searchName)
        {
            var products = Database.Products.Find(searchName);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        public void DeleteProduct(int id)
        {
            Database.Products.Delete(id);
            Database.Save();
        }

        public void Update(ProductDTO productDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>()).CreateMapper();
            var product = mapper.Map<ProductDTO, Product>(productDTO);
            Database.Products.Update(product);
            Database.Save();
        }

        public void CreateProduct(ProductDTO productDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>()).CreateMapper();
            var product = mapper.Map<ProductDTO, Product>(productDTO);
            Database.Products.Create(product);
            Database.Save();
        }
    }
}
