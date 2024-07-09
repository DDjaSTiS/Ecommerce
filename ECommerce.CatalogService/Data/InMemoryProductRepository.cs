using ECommerce.CatalogService.Models;
using System.Collections.Concurrent;

namespace ECommerce.CatalogService.Data
{
    public class InMemoryProductRepository : IProductRepository
    {
        private int _counter = 4;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        private ConcurrentDictionary<int, Product> _products =
            new ConcurrentDictionary<int, Product>(new List<KeyValuePair<int, Product>>
            {
                new KeyValuePair<int, Product>(1, new Product { Id = 1, Name = "Pizza", Price = 10}),
                new KeyValuePair<int, Product>(2, new Product { Id = 2, Name = "Pasta", Price = 7}),
                new KeyValuePair<int, Product>(3, new Product { Id = 3, Name = "Toast", Price = 2})
            });

        public async Task CreateAsync(Product product)
        {
            await _semaphoreSlim.WaitAsync();            
            try
            {
                product.Id = _counter;
                if (!_products.TryAdd(product.Id, product))
                {
                    throw new Exception("Failed to add");
                }

                Interlocked.Increment(ref _counter);
            }
            finally 
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<int?> Delete(int productId)
        {
            int? deletedProductId = null;
            await _semaphoreSlim.WaitAsync();
            try
            {
                if (!_products.TryRemove(productId, out Product? deletedProduct))
                {
                    throw new Exception("Failed to delete");
                }

                deletedProductId = deletedProduct.Id;
            }
            finally
            {
                _semaphoreSlim.Release();
            }

            return deletedProductId;
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            return Task.FromResult<IEnumerable<Product>>(_products.Values);
        }

        public Task<Product?> GetProduct(int productId)
        {
            if (!_products.TryGetValue(productId, out var product))
                throw new Exception("Failed to get");

            return Task.FromResult<Product?>(product);
        }

        public async Task Update(Product productRequest)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                if (!_products.TryGetValue(productRequest.Id, out Product product))
                    throw new Exception("Id not found");

                if (!_products.TryUpdate(productRequest.Id, productRequest, product!))
                    throw new Exception("Failed to update");
            }
            finally 
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
