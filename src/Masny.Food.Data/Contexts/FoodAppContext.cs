using System;
using System.Collections.Generic;
using System.Text;

namespace Masny.Food.Data.Contexts
{
    public class FoodAppContext
    {
        public FoodAppContext(DbContextOptions<FoodAppContext> options)
            : base(options)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<ProductDetail> ProductDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
    }
}
