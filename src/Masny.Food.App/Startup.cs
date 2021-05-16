using Masny.Food.Data.Contexts;
using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Managers;
using Masny.Food.Logic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Masny.Food.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Managers
            services.AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>));
            services.AddScoped<IProfileManager, ProfileManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IPromoCodeManager, PromoCodeManager>();

            // Services
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICalcService, CalcService>();

            // Database context
            services.AddDbContext<FoodAppContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FoodAppConnection")));

            // ASP.NET Core Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FoodAppContext>();

            // Microsoft services
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddMemoryCache();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddRazorRuntimeCompilation();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "PizzaApp.Cookie";
                //config.LoginPath = "/Account/SignIn";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");
                app.UseExceptionHandler("/Error");
            }

            app.UseSerilogRequestLogging();

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
