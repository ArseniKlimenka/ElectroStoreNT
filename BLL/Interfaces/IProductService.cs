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
        IEnumerable<ProductDTO> GetProducts();
        void Dispose();
    }
}
