using AutoMapper;
using BLL.Interfaces;
using Common.DTO;
using Common.Entitites;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        IUnitOfWork Database { get; set; }

        public CategoryService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(Database.Categories.GetAll());
        }

        public CategoryDTO GetCategory(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return mapper.Map<Category, CategoryDTO>(Database.Categories.Get(id));
        }

        public void Update(CategoryDTO categoryDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();
            var category = mapper.Map<CategoryDTO, Category>(categoryDTO);
            Database.Categories.Update(category);
            Database.Save();
        }

        public void DeleteCategory(int id)
        {
            Database.Categories.Delete(id);
            Database.Save();
        }

        public void CreateGenre(CategoryDTO categoryDTO)
        {
            var category = new  Category { CategoryName = categoryDTO.CategoryName };
            Database.Categories.Create(category);
            Database.Save();
        }
    }
}
