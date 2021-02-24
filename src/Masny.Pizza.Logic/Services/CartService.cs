using Masny.Pizza.Data.Contexts;
using Masny.Pizza.Data.Models;
using Masny.Pizza.Logic.Interfaces;
using Masny.Pizza.Logic.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masny.Pizza.Logic.Services
{
    public class CartService : ICartService
    {
        private readonly PizzaAppContext _pizzaAppContext;
        private readonly IMemoryCache _memoryCache;

        public CartService(
            PizzaAppContext pizzaAppContext,
            IMemoryCache memoryCache)
        {
            _pizzaAppContext = pizzaAppContext ?? throw new ArgumentNullException(nameof(pizzaAppContext));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public CartDto Get(string userId)
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

            return cartDto;
        }

        public Task Clear(string userId)
        {
            _memoryCache.Remove(userId);

            return Task.CompletedTask;
        }

        //
        public void AddOrUpdate(int operationType, string userId, Product product)
        {
            if (!_memoryCache.TryGetValue(userId, out CartDto cartDto))
            {
                cartDto = new CartDto
                {
                    UserId = userId,
                    Products = new List<Product>(),
                };
            }

            // TODO: switch and enum
            if (operationType == 1)
            {
                cartDto.Products.Add(product);
            }
            else if(operationType == 2)
            {
                cartDto.Products.Remove(product);
            }

            _memoryCache.Set(
                    userId,
                    cartDto,
                    new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));
        }
    }
}
