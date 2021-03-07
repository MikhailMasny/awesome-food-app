using Masny.Food.Logic.Models;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Interfaces
{
    /// <summary>
    /// Profile manager.
    /// </summary>
    public interface IProfileManager
    {
        /// <summary>
        /// Get profile by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Profile data transfer object.</returns>
        Task<ProfileDto> GetProfileByUserIdAsync(string userId);

        /// <summary>
        /// Create profile.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="name">User name.</param>
        Task CreateProfileAsync(string userId, string name);

        /// <summary>
        /// Update profile by user identifier.
        /// </summary>
        /// <param name="profileDto">Profile data transfer object.</param>
        Task UpdateProfileAsync(ProfileDto profileDto);
    }
}
