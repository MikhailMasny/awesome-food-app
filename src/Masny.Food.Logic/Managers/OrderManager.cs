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
        private readonly IRepositoryManager<Order> _orderRepository;
        private readonly IRepositoryManager<OrderProduct> _orderProductRepository;

        private IQueryable<Order> OrderQuery =>
                    _orderRepository.GetAll();

        public OrderManager(
            IRepositoryManager<Order> orderRepository,
            IRepositoryManager<OrderProduct> orderProductRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderProductRepository = orderProductRepository ?? throw new ArgumentNullException(nameof(orderProductRepository));
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

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task CreateOrderProductsAsync(int orderId, IEnumerable<int> productIds)
        {
            IEnumerable<OrderProduct> GetOrderProducts()
            {
                foreach (var productId in productIds)
                {
                    yield return new OrderProduct
                    {
                        OrderId = orderId,
                        ProductId = productId,
                    };
                }
            }

            await _orderProductRepository.CreateRangeAsync(GetOrderProducts());
            await _orderProductRepository.SaveChangesAsync();
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetEntityWithoutTrackingAsync(o => o.Id == id);

            return GetOrder(order);
        }

        public async Task<OrderDto> GetLastAsync()
        {
            var lastOrder = await OrderQuery
                .OrderBy(orderDto => orderDto.Id)
                .LastOrDefaultAsync();

            return GetOrder(lastOrder);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync() =>
            GetOrders(await OrderQuery.ToListAsync());

        public async Task<IEnumerable<OrderDto>> GetAllByUserIdAsync(string userId) =>
            GetOrders(await OrderQuery
                .Where(order => order.UserId == userId)
                .ToListAsync());

        public async Task UpdateOrderStatusByIdAsync(int orderId, StatusType statusType)
        {
            var order = await _orderRepository
                .GetEntityAsync(o => o.Id == orderId);

            order.Status = statusType;

            await _orderRepository.SaveChangesAsync();
        }

        private static IEnumerable<OrderDto> GetOrders(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                yield return new OrderDto
                {
                    Id = order.Id,
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
            }
        }

        private OrderDto GetOrder(Order order)
        {
            return order is null
                ? new OrderDto()
                : new OrderDto
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
        }
    }
}
