using Masny.Food.Common.Enums;
using Masny.Food.Data.Constants;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Product entity.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Products, Schema.Product)
                .HasKey(product => product.Id);

            builder.Property(product => product.Comment)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.Property(product => product.Diameter)
                .HasConversion(new EnumToNumberConverter<DiameterType, int>());

            builder.Property(product => product.Kind)
                .HasConversion(new EnumToNumberConverter<KindType, int>());

            builder.HasOne(product => product.ProductDetail)
                .WithMany(productDetail => productDetail.Products)
                .HasForeignKey(product => product.ProductDetailId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
