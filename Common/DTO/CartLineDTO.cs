using Common.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CartLineDTO
    {
        Product Product { get; set; }
        int Quantity { get; set; }
    }
}
