using ECommerce.CatalogService.Models;

namespace ECommerce.CatalogService.Services
{
    public interface IProductService
    {
        Task<Product?> GetProduct(int productId);
        Task<IEnumerable<Product>> GetAll();        
        Task UpdateProduct(Product product);
        Task<int?> DeleteProduct(int productId);
        Task CreateProduct(Product product);
        Task UpdateAll();
    }
}
