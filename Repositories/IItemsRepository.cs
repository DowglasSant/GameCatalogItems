using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        Task<Item> GetItem(Guid id);
        Task<IEnumerable<Item>> GetItems();
        public Task CreateItem(Item item);
        public Task UpdateItem(Item item);
        public Task DeleteItem(Guid id);
    }

}