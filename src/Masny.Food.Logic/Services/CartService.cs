using Masny.Food.Common.Enums;
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
                cartDto = new CartDto
                {
                    UserId = userId,
                    ProductIds = new List<int>(),
                };
            }

            return Task.FromResult(cartDto);
        }

        public Task AddOrUpdateAsync(CartOperationType cartOperationType, string userId, int productId)
        {
            if (!_memoryCache.TryGetValue(userId, out CartDto cartDto))
            {
                cartDto = new CartDto
                {
                    UserId = userId,
                    ProductIds = new List<int>(),
                };
            }

            switch (cartOperationType)
            {
                case CartOperationType.Add:
                    {
                        cartDto.ProductIds.Add(productId);
                    }
                    break;
                case CartOperationType.Remove:
                    {
                        cartDto.ProductIds.Remove(productId);
                    }
                    break;
                case CartOperationType.Unknown:
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

        public Task ClearAsync(string userId)
        {
            _memoryCache.Remove(userId);

            return Task.CompletedTask;
        }
    }
}
