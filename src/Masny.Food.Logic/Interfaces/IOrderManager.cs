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
        /// Get order history by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of order data transfer objects.</returns>
        Task<IEnumerable<OrderDto>> GetOrdersByUserId(string userId);

    }
}
