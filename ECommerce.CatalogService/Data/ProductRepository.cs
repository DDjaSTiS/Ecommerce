using ECommerce.CatalogService.Models;
using System.Collections.Concurrent;

namespace ECommerce.CatalogService.Data
{
    public class ProductRepository : IProductRepository
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

                await Task.Delay(100);
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
            if (_products.ContainsKey(productId))
            {
                await _semaphoreSlim.WaitAsync();
                try
                {
                    if (!_products.TryRemove(productId, out Product? deletedProduct))
                    {
                        throw new Exception("Failed to delete");
                    }

                    await Task.Delay(500);
                    deletedProductId = deletedProduct.Id;
                }
                finally
                {
                    _semaphoreSlim.Release();
                }
            }

            return deletedProductId;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await Task.Run(() => _products.Values);
        }

        public async Product? GetProduct(int productId)
        {
            
            if (_products.TryGetValue(productId, out var product))
                return product;

            return null;
        }

        public void Update(Product product)
        {
            //_products.TryUpdate(product.Id, product); 
        }
    }
}
