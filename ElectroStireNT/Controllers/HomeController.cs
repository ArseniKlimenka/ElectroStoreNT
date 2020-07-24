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
    public class HomeController : Controller
    {
        IProductService productService;
        public HomeController(IProductService serv)
        {
           productService = serv;
        }
        
        public ActionResult Index(string category, int page = 1)
        {
            int pageSize = 4;
            IEnumerable<ProductDTO> productDtos = productService.GetProducts() ;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            ListViewModel model = new ListViewModel
            {
                Prods = products.Where(p =>category == null || p.Category == category)
                    .OrderBy(product => product.ProductId).Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
        products.Count() :
       products.Where(p => p.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
           productService.Dispose();
            base.Dispose(disposing);
        }
    }
}