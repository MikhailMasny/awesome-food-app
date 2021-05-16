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
        private readonly IRepositoryManager<PromoCode> _promoCodeRepository;

        public PromoCodeManager(IRepositoryManager<PromoCode> promoCodeManager)
        {
            _promoCodeRepository = promoCodeManager ?? throw new ArgumentNullException(nameof(promoCodeManager));
        }

        public async Task<PromoCodeDto> GetPromoCodeByCodeAsync(string code)
        {
            code = code ?? throw new ArgumentException($"'{nameof(code)}' cannot be null or empty.", nameof(code));

            var promoCode = await _promoCodeRepository
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
