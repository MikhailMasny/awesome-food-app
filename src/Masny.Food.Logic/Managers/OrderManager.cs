using Masny.Food.Common.Enums;
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
        private readonly IRepositoryManager<OrderProduct> _orderProductManager;

        public OrderManager(
            IRepositoryManager<Order> orderManager,
            IRepositoryManager<OrderProduct> orderProductManager)
        {
            _orderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));
            _orderProductManager = orderProductManager ?? throw new ArgumentNullException(nameof(orderProductManager));
        }

        public async Task<int> CreateOrderAsync(OrderDto orderDto)
        {
            var order = new Order
            {
                Number = orderDto.Number,
                Creation = orderDto.Creation,
                UserId = orderDto.UserId,
                Name = orderDto.Name,
                Phone = orderDto.Phone,
                InPlace = orderDto.InPlace,
                Address = orderDto.Address,
                PromoCode = orderDto.PromoCode,
                TotalPrice = orderDto.TotalPrice,
                Payment = orderDto.Payment,
                Comment = orderDto.Comment,
                Status = orderDto.Status,
            };

            await _orderManager.CreateAsync(order);
            await _orderManager.SaveChangesAsync();

            return order.Id;
        }

        public async Task CreateOrderProductsAsync(int orderId, IEnumerable<int> productIds)
        {
            var orderProducts = new List<OrderProduct>();
            foreach (var productId in productIds)
            {
                orderProducts.Add(new OrderProduct
                {
                    OrderId = orderId,
                    ProductId = productId,
                });
            }

            await _orderProductManager.CreateRangeAsync(orderProducts);
            await _orderProductManager.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderManager
                .GetAll()
                .ToListAsync();

            var orderDtos = new List<OrderDto>();
            foreach (var order in orders)
            {
                // TODO: change to yield return
                orderDtos.Add(new OrderDto
                {
                    Id = order.Id,
                    Number = order.Number,
                    Creation = order.Creation,
                    UserId = order.UserId,
                    Name = order.Name,
                    Phone = order.Phone,
                    InPlace = order.InPlace,
                    Address = order.Address,
                    PromoCode = order.PromoCode,
                    Payment = order.Payment,
                    TotalPrice = order.TotalPrice,
                    Comment = order.Comment,
                    Status = order.Status,
                });
            }

            return orderDtos;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderManager.GetEntityWithoutTrackingAsync(o => o.Id == id);

            var orderDto = new OrderDto
            {
                Number = order.Number,
                Creation = order.Creation,
                Name = order.Name,
                Phone = order.Phone,
                InPlace = order.InPlace,
                Address = order.Address,
                PromoCode = order.PromoCode,
                Payment = order.Payment,
                TotalPrice = order.TotalPrice,
                Comment = order.Comment,
                Status = order.Status,
            };

            return orderDto;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
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
                    Payment = order.Payment,
                    TotalPrice = order.TotalPrice,
                    Comment = order.Comment,
                    Status = order.Status,
                });
            }

            return orderDtos;
        }

        public async Task UpdateOrderStatusByIdAsync(int orderId, StatusType statusType)
        {
            var order = await _orderManager.GetEntityAsync(o => o.Id == orderId);

            order.Status = statusType;

            await _orderManager.SaveChangesAsync();
        }
    }
}
