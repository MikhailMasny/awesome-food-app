using Masny.Food.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Masny.Food.App.Utils
{
    /// <summary>
    /// Fill in the database after creation.
    /// </summary>
    public static class ContextSeed
    {
        /// <summary>
        /// Context seed.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Run(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var isSeeded = FoodAppContextSeed.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
                if (isSeeded)
                {
                    Log.Information("The database is successfully seeded."); // ContextSeedSuccessful
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred seeding the DB."); // ContextSeedError
            }
        }
    }
}
