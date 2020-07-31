using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using ElectroStireNT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using PagedList;

namespace ElectroStireNT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoppingCartFactory shoppingCartFactory;
        IProductService productService;
        IOrderService orderService;

        public HomeController(IProductService serv, IOrderService oService, ShoppingCartFactory factory)
        {
            orderService = oService;
            shoppingCartFactory = factory;
            productService = serv;
        }
        
        public ActionResult Index(int? id, string category, int? page, string searchName)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            IEnumerable<ProductDTO> products;

            ViewBag.category = category;
            ViewBag.searchName = searchName;           

            if (searchName == null)
                products = productService.GetProducts();
            else
                products = productService.FindProducts(searchName);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var productsViewModel = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(products);

            if (Request.IsAjaxRequest())
            {
                var cart = shoppingCartFactory.GetCart(HttpContext);
                var addedProd = productService.GetProduct(id.Value);
                orderService.AddToCart(addedProd, cart.ShoppingCartId);


                return PartialView("Summary",productsViewModel.ToPagedList(pageNumber, pageSize));
            }
            return View(productsViewModel.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> AddToCart(int? id, int? page, string category)
        {

            var cart = shoppingCartFactory.GetCart(HttpContext);
            var addedProd = productService.GetProduct(id.Value);
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            await orderService.AddToCart(addedProd, cart.ShoppingCartId);

            var products = productService.GetProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var productsViewModel = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(products);

            return View("Index", productsViewModel.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Autocomplete(string term)
        {
            var products = productService.FindProducts(term).Select(b => new { value = b.Name }).Distinct();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
           productService.Dispose();
            base.Dispose(disposing);
        }
    }
}