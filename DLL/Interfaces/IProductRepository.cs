using Common.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Find(string searchString);
    }
}
