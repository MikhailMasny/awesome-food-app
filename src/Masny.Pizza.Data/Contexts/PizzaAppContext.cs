using Masny.Pizza.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Masny.Pizza.Data.Contexts
{
    public class PizzaAppContext : IdentityDbContext<User>
    {
        public PizzaAppContext(DbContextOptions<PizzaAppContext> options)
            : base(options)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductDetail> ProductDetails { get; set; }

        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
