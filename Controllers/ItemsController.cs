using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using Catalog.DTOs;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var items = (await repository.GetItems()).Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id)
        {
            var item = await repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updateItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItem(updateItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            await repository.DeleteItem(id);

            return NoContent();
        }
    }
}