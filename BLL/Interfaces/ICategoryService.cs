using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int id);
        void Update(CategoryDTO categoryDTO);
        void DeleteCategory(int id);
        void CreateGenre(CategoryDTO categoryDTO);
    }
}
