using Microsoft.EntityFrameworkCore;
using WebApplication2Test.Core.CoreModels;
using WebApplication2Test.Data;
using WebApplication2Test.Interfaces;
using WebApplication2Test.Models;

namespace WebApplication2Test.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _context;

        public ProductService(DbContextClass context)
        {
            _context = context;
        }

        public async Task<Result<Product>> GetProductById(int productId)
        {
            var searchedProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);
            if (searchedProduct != null)
            {
                return Result<Product>.Success(searchedProduct);
            }
            return Result<Product>.Error($"Product with such ID: {productId} does not exists");
        }

        //implement check for empty products list
        public async Task<Result<List<Product>>> GetAllProducts()
        {
            return Result<List<Product>>.Success(await _context.Products.ToListAsync());
        }

        public async Task<Result<Product>> CreateProduct(Product product)
        {
            var searchedProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == product.Id);
            if (searchedProduct != null)
            {
                return Result<Product>.Error($"Product with such Id: {product.Id} already exist!");
            }

            var creationResult = await _context.Products.AddAsync(product);
            _context.SaveChanges();
            return Result<Product>.Success(creationResult.Entity);
        }

        public async Task<Result<Product>> UpdateProduct(Product product)
        {
            var searchedProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == product.Id);
            if (searchedProduct == null)
            {
                return Result<Product>.Error($"Product with such Id: {product.Id} does not exist!");
            }

            var updationResult = _context.Products.Update(product);
            _context.SaveChanges();
            return Result<Product>.Success(updationResult.Entity);
        }

        public async Task<Result<bool>> DeleteProduct(int productId)
        {
            var searchedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (searchedProduct == null)
            {
                return Result<bool>.Success(true);
            }

            var deletionResult = _context.Products.Remove(searchedProduct);
            bool result = false;
            if (deletionResult != null)
            {
                result = true;
            }

            return Result<bool>.Success(result);
        }
    }
}
