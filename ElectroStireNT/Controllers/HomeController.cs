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
        
        public ActionResult Index(int? id,string category, int page = 1 )
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
            if (Request.IsAjaxRequest())
            {
                var cart = shoppingCartFactory.GetCart(HttpContext);
                var addedBook = productService.GetProduct(id.Value);
                orderService.AddToCart(addedBook, cart.ShoppingCartId);


                return PartialView("ProductSummary", model);
            }
            return View(model);
        }

        public async Task<ActionResult> AddToCart(int? id, int? page, string category, string author)
        {
            var cart = shoppingCartFactory.GetCart(HttpContext);
            var addedBook = productService.GetProduct(id.Value);
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            await orderService.AddToCart(addedBook, cart.ShoppingCartId);

            var books = productService.GetProducts();

            return PartialView("ProductSummary", books);
        }


        protected override void Dispose(bool disposing)
        {
           productService.Dispose();
            base.Dispose(disposing);
        }
    }
}