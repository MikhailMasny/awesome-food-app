using Masny.Food.Data.Configurations;
using Masny.Food.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Masny.Food.Data.Contexts
{
    /// <summary>
    /// Food database context.
    /// </summary>
    public class FoodAppContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Contructor with params.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public FoodAppContext(DbContextOptions<FoodAppContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for Profiles.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// DbSet for Orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// DbSet for OrderProducts.
        /// </summary>
        public DbSet<OrderProduct> OrderProducts { get; set; }

        /// <summary>
        /// DbSet for Products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// DbSet for ProductDetails.
        /// </summary>
        public DbSet<ProductDetail> ProductDetails { get; set; }

        /// <summary>
        /// DbSet for ProductIngredients.
        /// </summary>
        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        /// <summary>
        /// DbSet for Ingredients.
        /// </summary>
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ApplyConfiguration(new ProfileConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderProductConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductDetailConfiguration());
            builder.ApplyConfiguration(new ProductIngredientConfiguration());
            builder.ApplyConfiguration(new IngredientConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
