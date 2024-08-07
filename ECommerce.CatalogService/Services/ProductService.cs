﻿using ECommerce.CatalogService.Data;
using ECommerce.CatalogService.Models;

namespace ECommerce.CatalogService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateProduct(Product product)
        {
            await _repository.CreateAsync(product);
        }

        public async Task<int?> DeleteProduct(int productId)
        {
            return await _repository.Delete(productId);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Product?> GetProduct(int productId)
        {
            return await _repository.GetProduct(productId);
        }

        public async Task UpdateProduct(Product product)
        {
            await _repository.Update(product);
        }
    }
}
