using Masny.Food.Common.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.Data.Contexts
{
    /// <summary>
    /// SocialMediaDashboard context seed.
    /// </summary>
    public static class FoodAppContextSeed
    {
        /// <summary>
        /// Seed roles.
        /// </summary>
        /// <param name="roleManager">Role manager.</param>
        public static async Task<bool> SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

            if (roleManager.Roles.Any())
            {
                return false;
            }

            if (!await roleManager.RoleExistsAsync(AppRole.Admin))
            {
                var adminRole = new IdentityRole(AppRole.Admin);
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(AppRole.User))
            {
                var userRole = new IdentityRole(AppRole.User);
                await roleManager.CreateAsync(userRole);
            }

            return true;
        }
    }
}
