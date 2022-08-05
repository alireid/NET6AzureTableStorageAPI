using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6AzureTableStorageAPI.Models;
using Net6AzureTableStorageAPI.Services;

namespace Net6AzureTableStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ITableStorageService _storageService;

        public ItemsController(ITableStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        [HttpGet]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromQuery] string category, string id)
        {
            return Ok(await _storageService.GetEntityAsync(category, id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AzureItemEntity entity)
        {
            entity.PartitionKey = entity.Category;

            string Id = Guid.NewGuid().ToString();
            entity.Id = Id;
            entity.RowKey = Id;

            var createdEntity = await _storageService.UpsertEntityAsync(entity);
            return CreatedAtAction(nameof(GetAsync), createdEntity);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] AzureItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            entity.RowKey = entity.Id;

            await _storageService.UpsertEntityAsync(entity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id)
        {
            await _storageService.DeleteEntityAsync(category, id);
            return NoContent();
        }
    }
}
