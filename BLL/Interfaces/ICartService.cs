using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface ICartService
    {

        ProductDTO GetProduct(int? id);
        IEnumerable<ProductDTO> GetProducts();
        void AddItem(ProductDTO product, int quantity);
        void RemoveLine(ProductDTO product);
        decimal ComputeTotalValue();
        IEnumerable<CartLineDTO> GetCarts();
        void Clear();
        void Dispose();
    }
}
