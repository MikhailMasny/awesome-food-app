﻿using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Services
{
    /// <inheritdoc cref="ICartService"/>
    public class CalcService : ICalcService
    {
        private readonly IRepositoryManager<Order> _orderManager;
        private readonly IRepositoryManager<Product> _productManager;

        public CalcService(
            IRepositoryManager<Order> orderManager,
            IRepositoryManager<Product> productManager)
        {
            _orderManager = orderManager ?? throw new System.ArgumentNullException(nameof(orderManager));
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        }

        public async Task<int> GetNewOrderNumberAsync(DateTime dateTime)
        {
            var orderNumber = 1;
            var lastOrder = await _orderManager
                .GetAll()
                .OrderBy(o => o.Id)
                .LastOrDefaultAsync();

            if (lastOrder is not null && lastOrder.Creation.Date == dateTime.Date)
            {
                orderNumber = ++lastOrder.Number;
            }

            return orderNumber;
        }

        public async Task<decimal> GetTotalPriceByProductIdsAsync(IEnumerable<int> ids)
        {
            return (await _productManager
                    .GetAll()
                    .Where(p => ids.Contains(p.Id))
                    .ToListAsync())
                .Select(p => p.Price)
                .Sum();
        }
    }
}
