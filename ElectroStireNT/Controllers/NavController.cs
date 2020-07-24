using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using ElectroStireNT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectroStireNT.Controllers
{
    public class NavController : Controller
    {
        IProductService productService;
        public NavController(IProductService serv)
        {
            productService = serv;
        }
        public ActionResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<ProductDTO> productDtos = productService.GetProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);

            IEnumerable<string> categories = products.Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}