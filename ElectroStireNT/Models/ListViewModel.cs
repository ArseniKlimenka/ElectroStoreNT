using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectroStireNT.Models
{
    public class ListViewModel
    {
        public IEnumerable<ProductViewModel> Prods {get;set;}
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}