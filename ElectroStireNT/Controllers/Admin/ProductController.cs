using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using ElectroStireNT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectroStireNT.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        // GET: Product
        private IProductService _productService;       
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _categoryService = categoryService;

            _productService = productService;
        }

        public ActionResult Main()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = _productService.GetProducts();

            return View(products);
        }

        public ActionResult GetAllProducts()
        {
            var products = _productService.GetProducts();
            return PartialView("ShowProducts", products);
        }

        public ActionResult Delete(int id)
        {
            var product = _productService.GetProduct(id);
            _productService.DeleteProduct(id);
            var products = _productService.GetProducts();
            return PartialView("Main", products);
        }

        public ActionResult Edit(int id)
        {
            var product = _productService.GetProduct(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, CreateProductViewModel>()).CreateMapper();
            var viewModel = mapper.Map<ProductDTO, CreateProductViewModel>(product);           
            viewModel.Categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName");
            return PartialView(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CreateProductViewModel, ProductDTO>()).CreateMapper();
                var productDTO = mapper.Map<CreateProductViewModel, ProductDTO>(viewModel);
                _productService.Update(productDTO);
                var products = _productService.GetProducts();

                return PartialView("ShowProducts", products);

            }
            else
            {
                ModelState.AddModelError("", "Данные заполнены неверно");
            }
           
            viewModel.Categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName");
            return PartialView("Edit", viewModel);

        }

        public ActionResult Create()
        {

            CreateProductViewModel viewModel = new CreateProductViewModel
            {
               
                Categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName")
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               ProductDTO productDTO = new ProductDTO
                {
                    ProductId = viewModel.Id,
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    CategoryId = viewModel.CategoryId,
                    Description = viewModel.Description
                    
                };

                _productService.CreateProduct(productDTO);
                return RedirectToAction("Main");
            }
            else
            {
                ModelState.AddModelError("", "Введенные данные неккоректны");
            }

            return View(viewModel);
        }









    }
}