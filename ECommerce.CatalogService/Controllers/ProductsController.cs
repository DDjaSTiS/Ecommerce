using ECommerce.CatalogService.Models;
using ECommerce.CatalogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.CatalogService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet()]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _productService.GetProduct(id));
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task Create()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                var prod = new Product();
                if (i % 5 == 0 && i != 0)
                    tasks.Add(_productService.DeleteProduct(i / 5));
                else
                    tasks.Add(_productService.CreateProduct(prod));                
            }

            await Task.WhenAll(tasks);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> Delete(int productId)
        {
            var deletedId = await _productService.DeleteProduct(productId);
            if (deletedId != null)
                return Ok($"Deleted product with Id: {productId}");
            else
                return BadRequest("Failed to delete");
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update([FromBody] Product product)
        {
            try
            {
                await _productService.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
