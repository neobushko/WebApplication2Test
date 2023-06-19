using Moq;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2Test.Controllers;
using WebApplication2Test.Interfaces;
using WebApplication2Test.Models;
using WebApplication2Test.Services;
using WebApplication2Test.Core.CoreModels;

namespace TestProductProject1
{
    public class UnitTestController
    {
        private readonly Mock<IProductService> _productService;

        public UnitTestController()
        {
            _productService = new Mock<IProductService>();
        }

        [Fact]
        public async Task GetAllProduct_ProductListAsync()
        {
            //arrange
            var productList = GetProductsData();
            var ListProductsAsync = GetProductDataAsync();
            _productService.Setup(x => x.GetAllProducts())
                .Returns(ListProductsAsync);
            var productController = new ProductController(_productService.Object);
            //act
            var productResult = await productController.GetAllProducts();
            //assert
            Assert.NotNull(productResult.Data);
            Assert.True(productResult.IsSuccess);
            Assert.Equal(GetProductsData().Count(), productResult.Data.Count());
            Assert.Equal(GetProductsData().ToString(), productResult.Data.ToString());
        }

        /// <summary>
        /// Checking if return and search works correctly in Product controller
        /// Expected: Returns product item with id 1
        /// </summary>
        [Fact]
        public async Task GetProductById_ProductItem()
        {
            //arrange
            int productId = 2;
            var productList = GetProductsData();
            var productAsync = GetProductAsync(1);
            _productService.Setup(service => service.GetProductById(productId))
                .Returns(productAsync);
            var productController = new ProductController(_productService.Object);
            //act
            var productResult = await productController.GetProductById(productId);
            //assert
            Assert.NotNull(productResult);
            Assert.True(productResult.IsSuccess);
            Assert.Equal(productList[1].Id, productResult.Data.Id);
            Assert.True(productList[1].Id == productResult.Data.Id);
        }

        /// <summary>
        /// Checking if parameter name is saved when we using method GetAllProducts
        /// Expected: Same name as predifined
        /// </summary>
        /// <param name="productName">Predifined name for first element in Mock List of Products</param>
        [Theory]
        [InlineData("Test1")]
        public async Task CheckProductExistOrNotByProductName_Product(string productName)
        {
            //arrange
            var productList = GetProductsData();
            var listProductsAsync = GetProductDataAsync();
            _productService.Setup(service => service.GetAllProducts())
                .Returns(listProductsAsync);
            var productController = new ProductController(_productService.Object);
            //act
            var productResult = await productController.GetAllProducts();
            var expectedProductName = productResult.Data.ToList()[0].Name;
            //assert
            Assert.True(productResult.IsSuccess);
            Assert.Equal(productName, expectedProductName);
        }

        [Fact]
        public async Task AddNewProductToMockList_Product()
        {
            //arrange
            var productList = GetProductsData();
            var productAsync = GetProductAsync(2);
            _productService.Setup(service => service.CreateProduct(productList[2]))
                .Returns(productAsync);
            var productController = new ProductController(_productService.Object);
            //act
            var productResult = await productController.CreateNewProduct(productList[2]);
            //assert
            Assert.NotNull(productResult);
            Assert.True(productResult.IsSuccess);
            Assert.Equal(productList[2].Id, productResult.Data.Id);
            Assert.True(productList[2].Id == productResult.Data.Id);
        }
        
        private List<Product> GetProductsData()
        {
            List<Product> products = new List<Product>()
            {
                    new Product
                    {
                        Id = 1,
                        Description = "Test1",
                        Name = "Test1",
                        Price = 10,
                        Stock = 13
                    },
                    new Product
                    {
                        Id = 2,
                        Description = "Test2",
                        Name = "Test2",
                        Price = 43,
                        Stock = 28564
                    },
                    new Product
                    {
                        Id = 2,
                        Description = "Test3",
                        Name = "Test3",
                        Price = 254,
                        Stock = 327
                    }
            };

            return products;
        }

        private async Task<Result<List<Product>>> GetProductDataAsync()
        {
            List<Product> products = GetProductsData();

            return Result<List<Product>>.Success(products);
        }

        private async Task<Result<Product>> GetProductAsync(int productId)
        {
            var product = GetProductsData()[productId];

            return Result<Product>.Success(product);
        }
    }
}
