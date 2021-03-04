using Masny.Food.Data.Constants;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for ProductDetail entity.
    /// </summary>
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.ProductDetails, Schema.Product)
                .HasKey(productDetail => productDetail.Id);

            builder.Property(productDetail => productDetail.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.Property(productDetail => productDetail.Description)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.Property(productDetail => productDetail.Comment)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);
        }
    }
}
