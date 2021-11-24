using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Dtos;
using catalog.Entities;
using catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository inMemoryItemRepository;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
        {
            inMemoryItemRepository = itemRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = inMemoryItemRepository.GetItems().Select(item => item.AsDto());

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items.Count()} items.");
            
            return items;
        }

        [HttpGet("{Id}")]
        public ActionResult<ItemDto> GetItem(Guid Id)
        {
            var item = inMemoryItemRepository.GetItem(Id);

            if (item is null) return NotFound();

            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            inMemoryItemRepository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { Id = item.Id },item.AsDto());
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateItem(Guid Id, CreateItemDto createItemDto)
        {
            var item = inMemoryItemRepository.GetItem(Id);

            if (item is null) return NotFound();

            Item newItem = item with
            {
                Name = createItemDto.Name,
                Price = createItemDto.Price
            };

            inMemoryItemRepository.UpdateItem(newItem);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteItem(Guid Id)
        {
            var item = inMemoryItemRepository.GetItem(Id);

            if (item is null) return NotFound();

            inMemoryItemRepository.DeleteItem(Id);

            return NoContent();
        }
    }
}
