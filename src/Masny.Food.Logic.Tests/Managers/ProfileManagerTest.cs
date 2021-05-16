using Masny.Food.Common.Enums;
using Masny.Food.Data.Contexts;
using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Managers;
using Masny.Food.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Masny.Food.Logic.Tests.Managers
{
    public class ProfileManagerTest
    {
        // SUT
        private readonly IProfileManager _profileManager;

        // Dependencies
        private readonly FoodAppContext _foodAppContext;

        private readonly IRepositoryManager<Profile> _profileRepository;

        public ProfileManagerTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<FoodAppContext>(options =>
                    options.UseInMemoryDatabase($"{nameof(ProfileManagerTest)}_Db")
                        .UseInternalServiceProvider(
                            new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider()))
                .AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>))
                .BuildServiceProvider();

            _foodAppContext = serviceProvider.GetRequiredService<FoodAppContext>();
            _profileRepository = serviceProvider.GetRequiredService<IRepositoryManager<Profile>>();

            _profileManager = new ProfileManager(_profileRepository);
        }

        [Fact]
        public void Constructor_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileManager(null));
        }

        [Fact]
        public void Method_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentException>(() =>
                _profileManager.CreateProfileAsync(null, null)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentException>(() =>
                _profileManager.CreateProfileAsync(string.Empty, string.Empty)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentException>(() =>
                _profileManager.CreateProfileAsync("value", null)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentException>(() =>
                _profileManager.CreateProfileAsync("value", string.Empty)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentException>(() =>
                _profileManager.GetProfileByUserIdAsync(null)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentException>(() =>
                _profileManager.GetProfileByUserIdAsync(string.Empty)
                    .GetAwaiter()
                    .GetResult());

            Assert.Throws<ArgumentNullException>(() =>
                _profileManager.UpdateProfileAsync(null)
                    .GetAwaiter()
                    .GetResult());
        }

        [Fact]
        public void CreateProfileAsync_ProfileDto_ProfileAdded()
        {
            // Arrange
            var profileDto = new ProfileDto
            {
                UserId = $"{nameof(ProfileManagerTest)}_userId",
                Name = "Name",
            };

            // Act
            _profileManager.CreateProfileAsync(
                    $"{nameof(ProfileManagerTest)}_userId",
                    "Name")
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(1, _foodAppContext.Profiles.Count());
        }

        [Fact]
        public void GetProfileByUserIdAsync_ProfileExist_ProfileDtoRetrieved()
        {
            // Arrange
            var userId = $"{nameof(ProfileManagerTest)}_userId";
            var profile = new Profile
            {
                UserId = userId,
                Name = "Name",
            };

            _foodAppContext.Profiles.Add(profile);
            _foodAppContext.SaveChanges();

            // Act
            var result = _profileManager
                .GetProfileByUserIdAsync(userId)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(profile.Name, result.Name);
        }

        [Fact]
        public void UpdateProfileAsync_ProfileExist_ProfileUpdated()
        {
            // Arrange
            var userId = $"{nameof(ProfileManagerTest)}_userId";
            var profile = new Profile
            {
                UserId = userId,
                Name = "Name",
                Gender = GenderType.Unknown,
                BirthDate = new DateTime(2000, 1, 1),
                Address = "Address",
                Avatar = BitConverter.GetBytes(200011)
            };

            _foodAppContext.Profiles.Add(profile);
            _foodAppContext.SaveChanges();

            // Act
            var profileDto = new ProfileDto
            {
                UserId = userId,
                Name = "NewName",
                Gender = GenderType.Male,
                BirthDate = new DateTime(2000, 2, 2),
                Address = "NewAddress",
                Avatar = BitConverter.GetBytes(20000202)
            };

            _profileManager
                .UpdateProfileAsync(profileDto)
                .GetAwaiter()
                .GetResult();

            var updatedProfile = _foodAppContext.Profiles
                .FirstOrDefault();

            // Assert
            Assert.Equal(userId, updatedProfile.UserId);
            Assert.Equal(profileDto.Name, updatedProfile.Name);
            Assert.Equal(profileDto.Gender, updatedProfile.Gender);
            Assert.Equal(profileDto.BirthDate, updatedProfile.BirthDate);
            Assert.Equal(profileDto.Address, updatedProfile.Address);
            Assert.Equal(profileDto.Avatar, updatedProfile.Avatar);
        }
    }
}
