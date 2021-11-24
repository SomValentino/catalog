using System;
using System.Collections.Generic;
using System.Linq;
using catalog.Entities;
namespace catalog.Repositories
{
    public class InMemoryItemRepository : IItemRepository
    {
        private List<Item> Items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems() => Items;

        public Item GetItem(Guid Id) => Items.Where(x => x.Id == Id).SingleOrDefault();

        public void CreateItem(Item item)
        {
            Items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index = Items.FindIndex(x => x.Id == item.Id);

            Items[index] = item;

        }

        public void DeleteItem(Guid Id)
        {
            var index = Items.FindIndex(x => x.Id == Id);

            Items.RemoveAt(index);
        }
    }
}