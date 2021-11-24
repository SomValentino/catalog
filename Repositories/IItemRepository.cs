using System;
using System.Collections.Generic;
using catalog.Entities;

namespace catalog.Repositories
{
    public interface IItemRepository
    {
        Item GetItem(Guid Id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid Id);
    }
}