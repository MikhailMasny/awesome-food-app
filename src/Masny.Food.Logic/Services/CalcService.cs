using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Services
{
    /// <inheritdoc cref="ICartService"/>
    public class CalcService : ICalcService
    {
        public Task<int> GetNewOrderNumberAsync(
            OrderDto lastOrderDto,
            DateTime dateTime)
        {
            return Task.Run(() =>
            {
                const int orderNumber = 1;
                return lastOrderDto is not null && lastOrderDto.Creation.Date == dateTime.Date
                    ? ++lastOrderDto.Number
                    : orderNumber;
            });
        }

        public Task<bool> IsValidPromoCodeAsync(decimal promoCodeValue) =>
            Task.Run(() =>
            {
                return promoCodeValue > 0;
            });

        public Task<decimal> GetTotalPriceAsync(
            IEnumerable<ProductDto> selectedProductDtos,
            PromoCodeDto promoCodeDto)
        {
            return Task.Run(async () =>
            {
                var totalPrice = selectedProductDtos
                    .Select(p => p.Price)
                    .Sum();

                return await IsValidPromoCodeAsync(promoCodeDto.Value)
                    ? totalPrice - totalPrice * promoCodeDto.Value / 100
                    : totalPrice;
            });
        }
    }
}
