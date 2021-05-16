using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using System;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Managers
{
    /// <inheritdoc cref="IPromoCodeManager"/>
    public class PromoCodeManager : IPromoCodeManager
    {
        private readonly IRepositoryManager<PromoCode> _promoCodeManager;

        public PromoCodeManager(IRepositoryManager<PromoCode> promoCodeManager)
        {
            _promoCodeManager = promoCodeManager ?? throw new ArgumentNullException(nameof(promoCodeManager));
        }

        public async Task<PromoCodeDto> GetPromoCodeByCodeAsync(string code)
        {
            var promoCode = await _promoCodeManager
                .GetEntityWithoutTrackingAsync(promo => promo.Code == code.ToUpper());

            return promoCode is null
                ? new PromoCodeDto()
                : new PromoCodeDto
                {
                    Id = promoCode.Id,
                    Code = promoCode.Code,
                    Value = promoCode.Value,
                    Comment = promoCode.Comment,
                };
        }
    }
}
