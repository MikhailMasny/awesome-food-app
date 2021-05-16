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
        private readonly IRepositoryManager<Profile> _profileRepository;

        public ProfileManager(IRepositoryManager<Profile> profileRepository)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        }

        public async Task CreateProfileAsync(string userId, string name)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or empty.", nameof(userId));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            var profile = new Profile
            {
                UserId = userId,
                Name = name,
            };

            await _profileRepository.CreateAsync(profile);
            await _profileRepository.SaveChangesAsync();
        }

        public async Task<ProfileDto> GetProfileByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or empty.", nameof(userId));
            }

            var profile = await _profileRepository.GetEntityWithoutTrackingAsync(p => p.UserId == userId);

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
            profileDto = profileDto ?? throw new ArgumentNullException(nameof(profileDto));

            var profile = await _profileRepository.GetEntityAsync(p => p.UserId == profileDto.UserId);

            profile.Name = profileDto.Name;
            profile.Gender = profileDto.Gender;
            profile.BirthDate = profileDto.BirthDate;
            profile.Address = profileDto.Address;

            if (profileDto.Avatar is not null)
            {
                profile.Avatar = profileDto.Avatar;
            }

            await _profileRepository.SaveChangesAsync();
        }
    }
}
