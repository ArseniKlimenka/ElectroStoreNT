using Common.Entitites;
using DLL.Context;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DLL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private EFdbContext db;

        public CategoryRepository(EFdbContext context)
        {
            db = context;
        }

        public void Create(Category item)
        {
            db.Categories.Add(item);
        }

        public void Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (!ReferenceEquals(category, null))
            {
                db.Categories.Remove(category);
            }
        }   

        public Category Get(int id)
        {
            return db.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public void Update(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
