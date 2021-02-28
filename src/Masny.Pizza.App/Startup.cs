using Masny.Pizza.Data.Contexts;
using Masny.Pizza.Data.Models;
using Masny.Pizza.Logic.Interfaces;
using Masny.Pizza.Logic.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Pizza.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();

            // Database context
            services.AddDbContext<PizzaAppContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PizzaAppConnection")));

            // ASP.NET Core Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PizzaAppContext>();

            // Microsoft services
            services.AddMemoryCache();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
)
                .AddRazorRuntimeCompilation();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => //CookieAuthenticationOptions
            //    {
            //        options.Cookie.Name = "PizzaApp.Cookie";
            //        options.Cookie.Expiration
            //        //options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            //    });

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "PizzaApp.Cookie";
                //config.LoginPath = "/Account/SignIn";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
