using FullStackBE.Data;
using FullStackBE.Models;
using FullStackBE.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public bool CategoryExists(string name)
        {
            bool value = _dbContext.Categories.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool CategoryExists(int id)
        {
            bool value = _dbContext.Categories.Any(c => c.Id == id);
            return value;
        }

        public bool CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _dbContext.Categories.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public bool Save()
        {
            int value = _dbContext.SaveChanges();
            if (value >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            return Save();
        }
    }
}
