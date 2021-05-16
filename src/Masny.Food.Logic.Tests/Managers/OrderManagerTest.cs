using Masny.Food.Common.Enums;
using Masny.Food.Data.Contexts;
using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Managers;
using Masny.Food.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Masny.Food.Logic.Tests.Managers
{
    public class OrderManagerTest
    {
        // SUT
        private readonly IOrderManager _orderManager;

        // Dependencies
        private readonly FoodAppContext _foodAppContext;

        private readonly IRepositoryManager<Order> _orderRepository;
        private readonly IRepositoryManager<OrderProduct> _orderProductRepository;

        public OrderManagerTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<FoodAppContext>(options =>
                    options.UseInMemoryDatabase($"{nameof(OrderManagerTest)}_Db")
                        .UseInternalServiceProvider(
                            new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider()))
                .AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>))
                .BuildServiceProvider();

            _foodAppContext = serviceProvider.GetRequiredService<FoodAppContext>();
            _orderRepository = serviceProvider.GetRequiredService<IRepositoryManager<Order>>();
            _orderProductRepository = serviceProvider.GetRequiredService<IRepositoryManager<OrderProduct>>();

            _orderManager = new OrderManager(
                _orderRepository,
                _orderProductRepository);
        }

        [Fact]
        public void Constructor_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new OrderManager(null, null));

            Assert.Throws<ArgumentNullException>(() =>
                new OrderManager(_orderRepository, null));
        }

        [Fact]
        public void Method_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _orderManager.CreateOrderAsync(null)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentException>(() =>
                _orderManager.GetAllByUserIdAsync(null)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentException>(() =>
                _orderManager.GetAllByUserIdAsync(string.Empty)
                    .GetAwaiter()
                    .GetResult());
        }

        [Fact]
        public void CreateOrderAsync_OrderDto_OrderAdded()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                UserId = $"{nameof(ProfileManagerTest)}_userId",
            };

            // Act
            var result = _orderManager
                .CreateOrderAsync(orderDto)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(1, _foodAppContext.Orders.Count());
            Assert.Equal(1, result);
        }

        [Fact]
        public void CreateOrderProductsAsync_OrderIdAndProductIds_OrderProductsAdded()
        {
            // Arrange
            var orderId = 1;
            var productIds = new List<int>
            {
                1,
                2,
                3,
            };

            // Act
            _orderManager
                .CreateOrderProductsAsync(orderId, productIds)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(3, _foodAppContext.OrderProducts.Count());
        }

        [Fact]
        public void GetByIdAsync_OrderExist_OrderDtoRetrieved()
        {
            // Arrange
            var orderId = 1;
            var order = new Order
            {
                Id = orderId,
                Name = "OrderName",
            };

            _foodAppContext.Orders.Add(order);
            _foodAppContext.SaveChanges();

            // Act
            var result = _orderManager
                .GetByIdAsync(orderId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(orderId, result.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetByIdAsync_OrderNotExist_EmptyOrderDtoRetrieved()
        {
            // Arrange
            var orderId = 1;

            // Act
            var result = _orderManager
                .GetByIdAsync(orderId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(0, result.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetLastAsync_OrdersExist_OrderDtoRetrieved()
        {
            // Arrange
            var orderId = 2;
            var order1 = new Order();
            var order2 = new Order();

            _foodAppContext.Orders.AddRange(order1, order2);
            _foodAppContext.SaveChanges();

            // Act
            var result = _orderManager
                .GetLastAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(orderId, result.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetLastAsync_OrdersNotExist_EmptyOrderDtoRetrieved()
        {
            // Arrange

            // Act
            var result = _orderManager
                .GetLastAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(0, result.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllAsync_OrdersExist_OrderDtosRetrieved()
        {
            // Arrange
            var order1 = new Order();
            var order2 = new Order();

            _foodAppContext.Orders.AddRange(order1, order2);
            _foodAppContext.SaveChanges();

            // Act
            var result = _orderManager
                .GetAllAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetAllAsync_OrdersNotExist_EmptyCollectionRetrieved()
        {
            // Arrange

            // Act
            var result = _orderManager
                .GetAllAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllByUserIdAsync_OrdersExist_OrderDtosRetrieved()
        {
            // Arrange
            var userId = $"{nameof(ProfileManagerTest)}_userId";
            var order1 = new Order
            {
                UserId = userId,
            };
            var order2 = new Order
            {
                UserId = userId,
            };
            var order3 = new Order
            {
                UserId = $"{nameof(ProfileManagerTest)}_anotherUserId",
            };

            _foodAppContext.Orders.AddRange(order1, order2, order3);
            _foodAppContext.SaveChanges();

            // Act
            var result = _orderManager
                .GetAllByUserIdAsync(userId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllByUserIdAsync_OrdersNotExist_EmptyCollectionRetrieved()
        {
            // Arrange

            // Act
            var result = _orderManager
                .GetAllByUserIdAsync("value")
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void UpdateOrderStatusByIdAsync_OrdersExist_OrderUpdated()
        {
            // Arrange
            var orderId = 2;
            var newOrderStatus = StatusType.InProgress;
            var order1 = new Order
            {
                Id = 1,
                Status = StatusType.Todo,
            };
            var order2 = new Order
            {
                Id = orderId,
                Status = StatusType.Todo,
            };

            _foodAppContext.Orders.AddRange(order1, order2);
            _foodAppContext.SaveChanges();

            // Act
            _orderManager
                .UpdateOrderStatusByIdAsync(orderId, newOrderStatus)
                .GetAwaiter()
                .GetResult();

            var updatedOrder = _foodAppContext.Orders
                .AsNoTracking()
                .FirstOrDefault(order => order.Id == orderId);

            // Assert
            Assert.Equal(newOrderStatus, updatedOrder.Status);
        }

        [Fact]
        public void UpdateOrderStatusByIdAsync_OrdersNotExist_OrderNotUpdated()
        {
            // Arrange
            var orderId = 2;
            var newOrderStatus = StatusType.InProgress;

            // Act

            // Assert
            Assert.Throws<KeyNotFoundException>(() =>
                _orderManager
                    .UpdateOrderStatusByIdAsync(orderId, newOrderStatus)
                    .GetAwaiter()
                    .GetResult());
        }
    }
}
