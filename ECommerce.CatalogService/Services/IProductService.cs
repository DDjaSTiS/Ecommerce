using ECommerce.CatalogService.Models;

namespace ECommerce.CatalogService.Services
{
    public interface IProductService
    {
        Product? GetProduct(int productId);
        Task<IEnumerable<Product>> GetAll();        
        void UpdateProduct(Product product);
        Task<int?> DeleteProduct(int productId);
        Task CreateProduct(Product product);
    }
}
