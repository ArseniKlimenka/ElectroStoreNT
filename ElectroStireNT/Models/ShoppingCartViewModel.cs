using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectroStireNT.Models
{
    public class ShoppingCartViewModel
    {
        public List<CartItemDTO> CartItems { get; set; }
        public string returnUrl { get; set; }
        public decimal CartTotal
        {
            get; set;
        }
    }
}