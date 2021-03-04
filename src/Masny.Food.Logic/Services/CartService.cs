using Masny.Food.Logic.Enums;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Services
{
    /// <inheritdoc cref="ICartService"/>
    public class CartService : ICartService
    {
        private readonly IMemoryCache _memoryCache;

        public CartService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public Task<CartDto> GetAsync(string userId)
        {
            if (!_memoryCache.TryGetValue(userId, out CartDto cartDto))
            {
                //cartDto = new CartDto
                //{
                //    UserId = userId,
                //    Products = new List<ProductDetail>(),
                //};

                //_memoryCache.Set(
                //    userId,
                //    cartDto,
                //    new MemoryCacheEntryOptions()
                //        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));
            }

            return Task.FromResult(cartDto);
        }

        public Task ClearAsync(string userId)
        {
            _memoryCache.Remove(userId);

            return Task.CompletedTask;
        }

        public Task AddOrUpdateAsync(CartOperationType cartOperationType, string userId, ProductDto productDto)
        {
            if (!_memoryCache.TryGetValue(userId, out CartDto cartDto))
            {
                cartDto = new CartDto
                {
                    UserId = userId,
                    Products = new List<ProductDto>(),
                };
            }

            switch (cartOperationType)
            {
                case CartOperationType.Unknown:
                    break;
                case CartOperationType.Add:
                    {
                        cartDto.Products.Add(productDto);
                    }
                    break;
                case CartOperationType.Remove:
                    {
                        cartDto.Products.Remove(productDto);
                    }
                    break;
                default:
                    break;
            }

            _memoryCache.Set(
                    userId,
                    cartDto,
                    new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

            return Task.CompletedTask;
        }
    }
}
