using System.Threading.Tasks;
using Net6AzureTableStorageAPI.Models;

namespace Net6AzureTableStorageAPI.Services
{
    public interface ITableStorageService
{
    Task<AzureItemEntity> GetEntityAsync(string category, string id);
    Task<AzureItemEntity> UpsertEntityAsync(AzureItemEntity entity);
    Task DeleteEntityAsync(string category, string id);
}
}
