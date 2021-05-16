using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Masny.Food.Logic.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Masny.Food.Logic.Tests.Services
{
    public class CalcServiceTest
    {
        // SUT
        private readonly ICalcService _calcService;

        public CalcServiceTest()
        {
            _calcService = new CalcService();
        }

        [Fact]
        public void GetNewOrderNumberAsync_LastOrderExist_NextNumber()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var lastOrderDto = new OrderDto
            {
                Number = 2,
                Creation = DateTime.Now,
            };

            // Act
            var result = _calcService
                .GetNewOrderNumberAsync(
                    lastOrderDto,
                    dateTime)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetNewOrderNumberAsync_LastOrderExist_NewNumber()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var lastOrderDto = new OrderDto
            {
                Number = 2,
                Creation = DateTime.Now.AddDays(-1),
            };

            // Act
            var result = _calcService
                .GetNewOrderNumberAsync(
                    lastOrderDto,
                    dateTime)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetNewOrderNumberAsync_LastOrderNotExist_NewNumber()
        {
            // Arrange

            // Act
            var result = _calcService
                .GetNewOrderNumberAsync(
                    null,
                    DateTime.Now)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void IsValidPromoCodeAsync_PositiveValue_IsValid()
        {
            // Arrange
            var promoCodeDto = new PromoCodeDto
            {
                Value = 5M
            };

            // Act
            var result = _calcService
                .IsValidPromoCodeAsync(promoCodeDto.Value)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidPromoCodeAsync_ZeroValue_IsNotValid()
        {
            // Arrange
            var promoCodeDto = new PromoCodeDto();

            // Act
            var result = _calcService
                .IsValidPromoCodeAsync(promoCodeDto.Value)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetTotalPriceAsync_ProductsAndPromoCodeExist_TotalPriceWithPromoCode()
        {
            // Arrange
            var productDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    Price = 2M,
                },
                new ProductDto
                {
                    Price = 4M,
                }
            };

            var promoCodeDto = new PromoCodeDto
            {
                Value = 50M
            };

            // Act
            var result = _calcService
                .GetTotalPriceAsync(
                    productDtos,
                    promoCodeDto.Value)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(3M, result);
        }

        [Fact]
        public void GetTotalPriceAsync_ProductsExist_TotalPriceWithoutPromoCode()
        {
            // Arrange
            var productDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    Price = 2M,
                },
                new ProductDto
                {
                    Price = 4M,
                }
            };

            // Act
            var result = _calcService
                .GetTotalPriceAsync(
                    productDtos,
                    new PromoCodeDto().Value)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(6M, result);
        }

        [Fact]
        public void GetTotalPriceAsync_ProductsNotExist_TotalPriceWithoutPromoCode()
        {
            // Arrange

            // Act
            var result = _calcService
                .GetTotalPriceAsync(
                    new List<ProductDto>(),
                    new PromoCodeDto().Value)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(0M, result);
        }
    }
}
