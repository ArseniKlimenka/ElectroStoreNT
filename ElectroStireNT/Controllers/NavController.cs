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
        IProductService _productService;
        ICategoryService _categoryService;
        public NavController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public ActionResult Menu(string category = null)
        {
            var categories = _categoryService.GetCategories();
            MenuViewModel viewModel = new MenuViewModel
            {              
                Categories = categories.ToList()
            };
            ViewBag.SelectedCategegory = category;

            return PartialView(viewModel);

        }
    }
}