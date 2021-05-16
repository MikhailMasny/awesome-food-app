using Masny.Food.Common.Enums;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Masny.Food.Logic.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Masny.Food.Logic.Tests.Services
{
    public class CartServiceTest
    {
        // SUT
        private readonly ICartService _cartService;

        // Dependencies
        private readonly IMemoryCache _memoryCache;

        public CartServiceTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddMemoryCache()
                .BuildServiceProvider();

            _memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();

            _cartService = new CartService(_memoryCache);
        }

        [Fact]
        public void Constructor_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CartService(null));
        }

        [Fact]
        public void GetAsync_MemoryCacheNotEmpty_RetrievedCartDto()
        {
            // Arrange
            var userId = $"{nameof(CartServiceTest)}_userId";
            var cartDto = new CartDto
            {
                UserId = userId,
                ProductIds = new List<int>
                {
                    1,
                    2,
                    3,
                },
            };

            _memoryCache.Set(
                userId,
                cartDto,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

            // Act
            var result = _cartService
                .GetAsync(userId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal(3, result.ProductIds.Count);
        }

        [Fact]
        public void AddOrRemoveAsync_MemoryCacheEmpty_ProductIdAdded()
        {
            // Arrange
            var userId = $"{nameof(CartServiceTest)}_userId";
            var productId = 1;

            // Act
            _cartService.AddOrRemoveAsync(
                    CartOperationType.Add,
                    userId,
                    productId)
                .GetAwaiter()
                .GetResult();

            var result = (CartDto)_memoryCache.Get(userId);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.NotEmpty(result.ProductIds);
        }

        [Fact]
        public void AddOrRemoveAsync_MemoryCacheNotEmpty_ProductIdRemoved()
        {
            // Arrange
            var userId = $"{nameof(CartServiceTest)}_userId";
            var productId = 1;
            var cartDto = new CartDto
            {
                UserId = userId,
                ProductIds = new List<int>
                {
                    productId,
                    2,
                    3,
                },
            };

            _memoryCache.Set(
                userId,
                cartDto,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

            // Act
            _cartService.AddOrRemoveAsync(
                    CartOperationType.Remove,
                    userId,
                    productId)
                .GetAwaiter()
                .GetResult();

            var result = (CartDto)_memoryCache.Get(userId);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.NotEmpty(result.ProductIds);
            Assert.Equal(2, result.ProductIds.Count);
        }

        [Fact]
        public void AddOrRemoveAsync_MemoryCacheEmpty_ProductIdNotFound()
        {
            // Arrange
            var userId = $"{nameof(CartServiceTest)}_userId";
            var productId = 1;

            // Act
            _cartService.AddOrRemoveAsync(
                    CartOperationType.Remove,
                    userId,
                    productId)
                .GetAwaiter()
                .GetResult();

            var result = (CartDto)_memoryCache.Get(userId);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Empty(result.ProductIds);
        }

        [Fact]
        public void AddOrRemoveAsync_MemoryCacheNotEmpty_UnknownCartOperationType()
        {
            // Arrange
            var userId = $"{nameof(CartServiceTest)}_userId";
            var productId = 1;
            var cartDto = new CartDto
            {
                UserId = userId,
                ProductIds = new List<int>
                {
                    productId,
                    2,
                    3,
                },
            };

            _memoryCache.Set(
                userId,
                cartDto,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

            // Act
            _cartService.AddOrRemoveAsync(
                    CartOperationType.Unknown,
                    userId,
                    productId)
                .GetAwaiter()
                .GetResult();

            var result = (CartDto)_memoryCache.Get(userId);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.NotEmpty(result.ProductIds);
            Assert.Equal(3, result.ProductIds.Count);
        }

        [Fact]
        public void GetAsync_MemoryCacheEmpty_RetrievedNewCartDto()
        {
            // Arrange
            var userId = $"{nameof(CartServiceTest)}_userId";

            // Act
            var result = _cartService
                .GetAsync(userId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Empty(result.ProductIds);
        }

        [Fact]
        public void ClearAsync_MemoryCacheNotEmpty_CacheCleaned()
        {
            // Arrange
            var userId = $"{nameof(CartServiceTest)}_userId";
            var cartDto = new CartDto
            {
                UserId = userId,
                ProductIds = new List<int>
                {
                    1,
                    2,
                    3,
                },
            };

            _memoryCache.Set(
                userId,
                cartDto,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

            // Act
            _cartService.ClearAsync(userId)
                .GetAwaiter()
                .GetResult();

            var result = _memoryCache.Get(userId);

            // Assert
            Assert.Null(result);
        }
    }
}
