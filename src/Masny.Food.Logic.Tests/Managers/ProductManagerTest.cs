using Masny.Food.Data.Contexts;
using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Masny.Food.Logic.Tests.Managers
{
    public class ProductManagerTest
    {
        // SUT
        private readonly IProductManager _productManager;

        // Dependencies
        private readonly FoodAppContext _foodAppContext;

        private readonly IRepositoryManager<Product> _productRepository;
        private readonly IRepositoryManager<ProductDetail> _productDetailRepository;

        public ProductManagerTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<FoodAppContext>(options =>
                    options.UseInMemoryDatabase($"{nameof(ProductManagerTest)}_Db")
                        .UseInternalServiceProvider(
                            new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider()))
                .AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>))
                .BuildServiceProvider();

            _foodAppContext = serviceProvider.GetRequiredService<FoodAppContext>();
            _productRepository = serviceProvider.GetRequiredService<IRepositoryManager<Product>>();
            _productDetailRepository = serviceProvider.GetRequiredService<IRepositoryManager<ProductDetail>>();

            _productManager = new ProductManager(
                _productRepository,
                _productDetailRepository);
        }

        [Fact]
        public void Constructor_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ProductManager(null, null));

            Assert.Throws<ArgumentNullException>(() =>
                new ProductManager(_productRepository, null));
        }

        [Fact]
        public void GetAllProductDetailsAsync_ProductDetailsExist_ProductDetailDtosRetrieved()
        {
            // Arrange
            var productDetail1 = new ProductDetail
            {
                Id = 1,
                Name = "ProductDetailName1",
                Description = "ProductDetailDescription1",
            };

            var productDetail2 = new ProductDetail
            {
                Id = 2,
                Name = "ProductDetailName2",
                Description = "ProductDetailDescription2",
            };

            var product1 = new Product
            {
                ProductDetailId = productDetail1.Id,
                IsArchived = true,
            };

            var product2 = new Product
            {
                ProductDetailId = productDetail2.Id,
            };

            _foodAppContext.ProductDetails.AddRange(productDetail1, productDetail2);
            _foodAppContext.Products.AddRange(product1, product2);
            _foodAppContext.SaveChanges();

            // Act
            var result = _productManager
                .GetAllProductDetailsAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void GetAllProductDetailsAsync_ProductDetailsNotExist_EmptyCollectionRetrieved()
        {
            // Arrange

            // Act
            var result = _productManager
                .GetAllProductDetailsAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllProductDetailsAsync_ProductsIsArchived_EmptyCollectionRetrieved()
        {
            // Arrange
            var productDetail1 = new ProductDetail
            {
                Id = 1,
                Name = "ProductDetailName1",
                Description = "ProductDetailDescription1",
            };

            var productDetail2 = new ProductDetail
            {
                Id = 2,
                Name = "ProductDetailName2",
                Description = "ProductDetailDescription2",
            };

            var product1 = new Product
            {
                ProductDetailId = productDetail1.Id,
                IsArchived = true,
            };

            var product2 = new Product
            {
                ProductDetailId = productDetail2.Id,
                IsArchived = true,
            };

            _foodAppContext.ProductDetails.AddRange(productDetail1, productDetail2);
            _foodAppContext.Products.AddRange(product1, product2);
            _foodAppContext.SaveChanges();

            // Act
            var result = _productManager
                .GetAllProductDetailsAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllProductsByIdsAsync_ProductsExist_ProductDtosRetrieved()
        {
            // Arrange
            var product1 = new Product();
            var product2 = new Product();

            _foodAppContext.Products.AddRange(product1, product2);
            _foodAppContext.SaveChanges();

            // Act
            var result = _productManager
                .GetAllProductsByIdsAsync(new int[] { 1, 2 })
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllProductsByIdsAsync_ProductsNotExist_EmptyCollectionRetrieved()
        {
            // Arrange

            // Act
            var result = _productManager
                .GetAllProductsByIdsAsync(new int[] { 1, 2 })
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllProductsByProductDetailIdAsync_ProductsExist_ProductsAndNameDtosRetrieved()
        {
            // Arrange
            var productDetailId = 1;
            var productDetail = new ProductDetail
            {
                Name = "ProductDetailName"
            };

            var product1 = new Product
            {
                ProductDetailId = productDetailId,
            };

            var product2 = new Product
            {
                ProductDetailId = productDetailId,
            };

            _foodAppContext.Products.AddRange(product1, product2);
            _foodAppContext.ProductDetails.Add(productDetail);
            _foodAppContext.SaveChanges();

            // Act
            var result = _productManager
                .GetAllProductsByProductDetailIdAsync(productDetailId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotEmpty(result.Products);
            Assert.Equal(2, result.Products.Count());
            Assert.NotEqual(string.Empty, result.Name);
        }

        [Fact]
        public void GetAllProductsByProductDetailIdAsync_ProductsNotExist_EmptyDtoRetrieved()
        {
            // Arrange
            var productDetailId = 1;

            // Act
            var result = _productManager
                .GetAllProductsByProductDetailIdAsync(productDetailId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Empty(result.Products);
            Assert.Equal(string.Empty, result.Name);
        }
    }
}
