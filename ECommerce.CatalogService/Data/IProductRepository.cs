using ECommerce.CatalogService.Models;

namespace ECommerce.CatalogService.Data
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);
        void Update(Product product);
        Task<int?> Delete(int productId);
        Product? GetProduct(int productId);
        Task<IEnumerable<Product>> GetAll();
    }
}
