using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Managers
{
    /// <inheritdoc cref="IOrderManager"/>
    public class OrderManager : IOrderManager
    {
        private readonly IRepositoryManager<Order> _orderManager;

        public OrderManager(IRepositoryManager<Order> orderManager)
        {
            _orderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserId(string userId)
        {
            var orders = await _orderManager
                .GetAll()
                .Where(o => o.UserId == userId)
                .ToListAsync();

            var orderDtos = new List<OrderDto>();
            foreach (var order in orders)
            {
                // TODO: change to yield return
                orderDtos.Add(new OrderDto
                {
                    Number = order.Number,
                    Creation = order.Creation,
                    Name = order.Name,
                    Phone = order.Phone,
                    InPlace = order.InPlace,
                    Address = order.Address,
                    PromoCode = order.PromoCode,
                    TotalPrice = order.TotalPrice,
                    Comment = order.Comment,
                    Status = order.Status,
                });
            }

            return orderDtos;
        }
    }
}
