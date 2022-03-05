using Catalog.Entities;

namespace Catalog.Repositories
{

    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new();
        // {
        //     new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
        //     new Item { Id = Guid.NewGuid(), Name = "Club", Price = 15, CreatedDate = DateTimeOffset.UtcNow },
        //     new Item { Id = Guid.NewGuid(), Name = "Wood Shield", Price = 13, CreatedDate = DateTimeOffset.UtcNow },
        //     new Item { Id = Guid.NewGuid(), Name = "Running Shoes", Price = 30, CreatedDate = DateTimeOffset.UtcNow },
        //     new Item { Id = Guid.NewGuid(), Name = "Cheap Food", Price = 5, CreatedDate = DateTimeOffset.UtcNow }
        // };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
        }
    }
}