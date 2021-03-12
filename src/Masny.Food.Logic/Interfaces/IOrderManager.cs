using Masny.Food.Common.Enums;
using Masny.Food.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Interfaces
{
    /// <summary>
    /// Order manager.
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// Get order by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Order data transfer object.</returns>
        Task<OrderDto> GetOrderByIdAsync(int id);

        /// <summary>
        /// Get orders.
        /// </summary>
        /// <returns>List of order data transfer objects.</returns>
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

        /// <summary>
        /// Get orders by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of order data transfer objects.</returns>
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);

        /// <summary>
        /// Create new order.
        /// </summary>
        /// <param name="orderDto">Order data transfer object.</param>
        /// <returns>Order identifier.</returns>
        Task<int> CreateOrderAsync(OrderDto orderDto);

        /// <summary>
        /// Create order products.
        /// </summary>
        /// <param name="orderId">Order identifier.</param>
        /// <param name="productIds">Product identifiers.</param>
        Task CreateOrderProductsAsync(int orderId, IEnumerable<int> productIds);

        /// <summary>
        /// Update order status by identifier.
        /// </summary>
        /// <param name="orderId">Order identifier.</param>
        /// <param name="statusType">Status type.</param>
        Task UpdateOrderStatusByIdAsync(int orderId, StatusType statusType);
    }
}
