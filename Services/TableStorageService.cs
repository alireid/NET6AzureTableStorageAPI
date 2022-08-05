using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Net6AzureTableStorageAPI.Models;

namespace Net6AzureTableStorageAPI.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string TableName = "Item";
        private readonly IConfiguration _configuration;

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AzureItemEntity> GetEntityAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<AzureItemEntity>(category, id);
        }

        public async Task<AzureItemEntity> UpsertEntityAsync(AzureItemEntity entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(category, id);
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);

            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
    }
}
