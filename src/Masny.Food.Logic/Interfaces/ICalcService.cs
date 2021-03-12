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
        /// <param name="dateTime">Date time.</param>
        /// <returns>Number.</returns>
        Task<int> GetNewOrderNumberAsync(DateTime dateTime);

        /// <summary>
        /// Get total price by product identifiers.
        /// </summary>
        /// <param name="productIds">Product identifiers.</param>
        /// <returns>Total price.</returns>
        Task<decimal> GetTotalPriceByProductIdsAsync(IEnumerable<int> productIds);

        /// <summary>
        /// Check is exist promo code.
        /// </summary>
        /// <param name="code">Promo code.</param>
        /// <returns>Is exist.</returns>
        Task<bool> IsExistPromoCodeAsync(string code);

        /// <summary>
        /// Apply promo code.
        /// </summary>
        /// <param name="code">Promo code.</param>
        /// <param name="totalPrice">Total price.</param>
        /// <returns>Total price with promo code.</returns>
        Task<decimal> ApplyPromoCodeAsync(string code, decimal totalPrice);
    }
}
