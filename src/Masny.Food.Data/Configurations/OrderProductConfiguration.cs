using Masny.Food.Data.Constants;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for OrderProduct entity.
    /// </summary>
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.OrderProducts, Schema.Order)
                .HasKey(orderProduct => orderProduct.Id);

            builder.HasOne(orderProduct => orderProduct.Order)
                .WithMany(order => order.OrderProducts)
                .HasForeignKey(orderProduct => orderProduct.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(orderProduct => orderProduct.Product)
                .WithMany(product => product.OrderProducts)
                .HasForeignKey(orderProduct => orderProduct.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
