using ECommerce.CatalogService.Models;

namespace ECommerce.CatalogService.Data
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);
        Task Update(Product product);
        Task<int?> Delete(int productId);
        Task<Product?> GetProduct(int productId);
        Task<IEnumerable<Product>> GetAll();
    }
}
