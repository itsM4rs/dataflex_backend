using FullStackBE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE.Repository.IRepository
{
    public interface IItemRepository
    {
        ICollection<Item> GetItems();

        ICollection<Item> GetItemsInCategory(int categoryId);
        Item GetItem(int itemId);
        bool ItemExists(string name);
        bool ItemExists(int id);
        bool CreateItem(Item item);
        bool UpdateItem(Item item);
        bool DeleteItem(Item item);
        bool Save();
    }
}
