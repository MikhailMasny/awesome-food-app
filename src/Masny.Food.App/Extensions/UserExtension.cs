using System.Security.Claims;

namespace Masny.Food.App.Extensions
{
    /// <summary>
    /// User extensions.
    /// </summary>
    public static class UserExtension
    {
        /// <summary>
        /// Get user identifier by claims principal.
        /// </summary>
        /// <param name="claimsPrincipal">Claims principal object.</param>
        /// <returns>User identifier.</returns>
        public static string GetUserIdByClaimsPrincipal(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
