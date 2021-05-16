using Masny.Food.Data.Contexts;
using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Masny.Food.Logic.Tests.Managers
{
    public class PromoCodeManagerTest
    {
        // SUT
        private readonly IPromoCodeManager _promoCodeManager;

        // Dependencies
        private readonly FoodAppContext _foodAppContext;

        private readonly IRepositoryManager<PromoCode> _promoCodeRepository;

        public PromoCodeManagerTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<FoodAppContext>(options =>
                    options.UseInMemoryDatabase($"{nameof(PromoCodeManagerTest)}_Db")
                        .UseInternalServiceProvider(
                            new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider()))
                .AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>))
                .BuildServiceProvider();

            _foodAppContext = serviceProvider.GetRequiredService<FoodAppContext>();
            _promoCodeRepository = serviceProvider.GetRequiredService<IRepositoryManager<PromoCode>>();

            _promoCodeManager = new PromoCodeManager(_promoCodeRepository);
        }

        [Fact]
        public void Constructor_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PromoCodeManager(null));
        }

        [Fact]
        public void Method_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentException>(() =>
                _promoCodeManager.GetPromoCodeByCodeAsync(null)
                    .GetAwaiter()
                    .GetResult());
        }

        [Fact]
        public void GetPromoCodeByCodeAsync_PromoCodeExist_PromoCodeDtoRetrieved()
        {
            // Arrange
            var code = "PROMOCODE123";
            var value = 50M;
            var promoCode = new PromoCode
            {
                Id = 1,
                Code = code,
                Value = value,
            };

            _foodAppContext.PromoCodes.Add(promoCode);
            _foodAppContext.SaveChanges();

            // Act
            var result = _promoCodeManager
                .GetPromoCodeByCodeAsync(code)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(code, result.Code);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void GetPromoCodeByCodeAsync_PromoCodeNotExist_EmptyPromoCodeDtoRetrieved()
        {
            // Arrange

            // Act
            var result = _promoCodeManager
                .GetPromoCodeByCodeAsync("PROMOCODE123")
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Code);
            Assert.Equal(0M, result.Value);
        }
    }
}
