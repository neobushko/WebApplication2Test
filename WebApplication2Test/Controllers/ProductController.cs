using Microsoft.AspNetCore.Mvc;
using WebApplication2Test.Core.CoreModels;
using WebApplication2Test.Interfaces;
using WebApplication2Test.Models;

namespace WebApplication2Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductById")]
        public async Task<Result<Product>> GetProductById(int id)
        {
            return await _productService.GetProductById(id);
        }

        [HttpGet("GetAllProducts")]
        public async Task<Result<List<Product>>> GetAllProducts() 
        {
            return await _productService.GetAllProducts();
        }

        [HttpPost("CreateNewProduct")]
        public async Task<Result<Product>> CreateNewProduct(Product product)
        {
            return await _productService.CreateProduct(product);
        }

        [HttpPut("UpdateProduct")]
        public async Task<Result<Product>> UpdateProduct(Product product) 
        {
            return await _productService.UpdateProduct(product);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<Result<bool>> DeleteProduct(int id)
        {
            return await _productService.DeleteProduct(id);
        }
    }
}
