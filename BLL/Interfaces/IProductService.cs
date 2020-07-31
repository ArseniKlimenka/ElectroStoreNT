using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        ProductDTO GetProduct(int? id);
        IEnumerable<ProductDTO> FindProducts(string searchName);
        IEnumerable<ProductDTO> GetProducts(string category);
        IEnumerable<ProductDTO> GetProducts();
        void DeleteProduct(int id);
        void Update(ProductDTO productDTO);
        void CreateProduct(ProductDTO productDTO);
        void Dispose();
    }
}
