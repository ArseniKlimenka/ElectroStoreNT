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
    public class CategoryController : Controller
    {
        // GET: Genre
        private IProductService _productService;
        private ICategoryService _categoryService;

        public CategoryController(IProductService productService, ICategoryService categoryService)
        {
            _categoryService = categoryService;

            _productService = productService;
        }


        public ActionResult GetAllCategories()
        {
           
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var categories = mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryService.GetCategories());
            return PartialView("ShowCategories", categories);
        }
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var categories =mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryService.GetCategories());
            return PartialView("ShowCategories", categories);
        }
        public ActionResult Edit(int id)
        {
            var categoryDTO = _categoryService.GetCategory(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var viewModel = mapper.Map<CategoryDTO, CategoryViewModel>(categoryDTO);
            return PartialView(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryViewModel, CategoryDTO>())
                    .CreateMapper();
                var category = mapper.Map<CategoryViewModel, CategoryDTO>(viewModel);
                _categoryService.CreateGenre(category);
                return RedirectToAction("GetAllCategories");
            }

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryViewModel, CategoryDTO>()).CreateMapper();
                var category = mapper.Map<CategoryViewModel, CategoryDTO>(viewModel);
                _categoryService.Update(category);
                var mapperGenres = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
                var categories =
                    mapperGenres.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryService.GetCategories());
                return PartialView("ShowCategories", categories);

            }

            return PartialView("Edit", viewModel);
        }


    }
}