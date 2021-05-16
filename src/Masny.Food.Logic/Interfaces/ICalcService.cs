using Masny.Food.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Interfaces
{
    /// <summary>
    /// Calculation service.
    /// </summary>
    public interface ICalcService
    {
        /// <summary>
        /// Get new order number.
        /// </summary>
        /// <param name="lastOrderDto">Last order data transfer object.</param>
        /// <param name="dateTime">Date time.</param>
        /// <returns>Number.</returns>
        Task<int> GetNewOrderNumberAsync(
            OrderDto lastOrderDto,
            DateTime dateTime);

        /// <summary>
        /// Check for valid promo code.
        /// </summary>
        /// <param name="promoCodeValue">Promo code value.</param>
        /// <returns>Result.</returns>
        Task<bool> IsValidPromoCodeAsync(decimal promoCodeValue);

        /// <summary>
        /// Get total price with promo code.
        /// </summary>
        /// <param name="selectedProductDtos">Selected product data transfer objects.</param>
        /// <param name="promoCodeValue">Promo code value.</param>
        /// <returns>Total price.</returns>
        Task<decimal> GetTotalPriceAsync(
            IEnumerable<ProductDto> selectedProductDtos,
            decimal promoCodeValue);
    }
}
