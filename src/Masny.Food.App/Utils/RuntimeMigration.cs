using Masny.Food.Common.Resources;
using Masny.Food.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Masny.Food.App.Utils
{
    /// <summary>
    /// Apply migration in real time.
    /// </summary>
    public static class RuntimeMigration
    {
        /// <summary>
        /// Apply migration.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Run(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            try
            {
                var hostEnironmentService = serviceProvider.GetRequiredService<IHostEnvironment>();
                if (hostEnironmentService.IsProduction())
                {
                    var appContextService = serviceProvider.GetRequiredService<FoodAppContext>();
                    appContextService.Database.Migrate();

                    Log.Information(CommonResource.DatabaseMigrateSuccessful);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonResource.DatabaseMigrateError);
            }
        }
    }
}
