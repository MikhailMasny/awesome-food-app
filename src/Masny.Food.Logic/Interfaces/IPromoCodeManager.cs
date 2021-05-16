using Masny.Food.Logic.Models;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Interfaces
{
    /// <summary>
    /// Promo code manager.
    /// </summary>
    public interface IPromoCodeManager
    {
        /// <summary>
        /// Get promo code by code.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <returns>Promo code data transfer object.</returns>
        Task<PromoCodeDto> GetPromoCodeByCodeAsync(string code);
    }
}
