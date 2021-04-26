using FullStackBE.Data;
using FullStackBE.Models;
using FullStackBE.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE.Repository
{
    public class ItemRepository : IItemRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public ItemRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public bool CreateItem(Item item)
        {
            _dbContext.Items.Add(item);
            return Save();
        }

        public bool DeleteItem(Item item)
        {
            _dbContext.Items.Remove(item);
            return Save();
        }

        public Item GetItem(int itemId)
        {
            return _dbContext.Items.FirstOrDefault(i => i.Id == itemId);
        }

        public ICollection<Item> GetItems()
        {
            return _dbContext.Items.Include(i=>i.Category).ToList();
        }

        public ICollection<Item> GetItemsInCategory(int categoryId)
        {
            return _dbContext.Items
                .Include(i => i.Category)
                .Where(i => i.CategoryId == categoryId)
                .ToList();
        }

        public bool ItemExists(string name)
        {
            bool value = _dbContext.Items.Any(i => i.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ItemExists(int id)
        {
            bool value = _dbContext.Items.Any(i => i.Id == id);
            return value;
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

        public bool UpdateItem(Item item)
        {
            _dbContext.Items.Update(item);
            return Save();
        }
    }
}
