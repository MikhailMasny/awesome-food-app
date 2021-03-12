using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using System;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Managers
{
    /// <inheritdoc cref="IProfileManager"/>
    public class ProfileManager : IProfileManager
    {
        private readonly IRepositoryManager<Profile> _profileManager;

        public ProfileManager(IRepositoryManager<Profile> profileManager)
        {
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
        }

        public async Task CreateProfileAsync(string userId, string name)
        {
            var profile = new Profile
            {
                UserId = userId,
                Name = name,
            };

            await _profileManager.CreateAsync(profile);
            await _profileManager.SaveChangesAsync();
        }

        public async Task<ProfileDto> GetProfileByUserIdAsync(string userId)
        {
            var profile = await _profileManager.GetEntityWithoutTrackingAsync(p => p.UserId == userId);

            return new ProfileDto
            {
                UserId = profile.UserId,
                Name = profile.Name,
                Gender = profile.Gender,
                BirthDate = profile.BirthDate,
                Address = profile.Address,
                Avatar = profile.Avatar,
            };
        }

        public async Task UpdateProfileAsync(ProfileDto profileDto)
        {
            var profile = await _profileManager.GetEntityAsync(p => p.UserId == profileDto.UserId);

            profile.Name = profileDto.Name;
            profile.Gender = profileDto.Gender;
            profile.BirthDate = profileDto.BirthDate;
            profile.Address = profileDto.Address;

            if (profileDto.Avatar is not null)
            {
                profile.Avatar = profileDto.Avatar;
            }

            await _profileManager.SaveChangesAsync();
        }
    }
}
