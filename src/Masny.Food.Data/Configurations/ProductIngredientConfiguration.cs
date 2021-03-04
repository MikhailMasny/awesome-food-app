using Masny.Food.Data.Constants;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for ProductIngredient entity.
    /// </summary>
    public class ProductIngredientConfiguration : IEntityTypeConfiguration<ProductIngredient>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductIngredient> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.ProductIngredients, Schema.Product)
                .HasKey(productIngredient => productIngredient.Id);

            builder.HasOne(productIngredient => productIngredient.ProductDetail)
                .WithMany(ProductIngredient => ProductIngredient.ProductIngredients)
                .HasForeignKey(productIngredient => productIngredient.ProductDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(productIngredient => productIngredient.Ingredient)
                .WithMany(ingredient => ingredient.ProductIngredients)
                .HasForeignKey(productIngredient => productIngredient.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
