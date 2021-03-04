using Masny.Food.Data.Constants;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Ingredient entity.
    /// </summary>
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Ingredients, Schema.Product)
                .HasKey(ingredient => ingredient.Id);

            builder.Property(productDetail => productDetail.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);
        }
    }
}