using WebApplication2Test.Core.CoreModels;
using WebApplication2Test.Models;

namespace WebApplication2Test.Interfaces
{
    public interface IProductService
    {
        public Task<Result<Product>> GetProductById(int productId);

        public Task<Result<List<Product>>> GetAllProducts();

        public Task<Result<Product>> UpdateProduct(Product product);

        public Task<Result<Product>> CreateProduct(Product product);

        public Task<Result<bool>> DeleteProduct(int productId);
    }
}
