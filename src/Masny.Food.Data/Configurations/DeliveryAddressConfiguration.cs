using Masny.Food.Data.Constants;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for DeliveryAddress entity.
    /// </summary>
    public class DeliveryAddressConfiguration : IEntityTypeConfiguration<DeliveryAddress>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DeliveryAddress> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.DeliveryAddresses, Schema.User)
                .HasKey(deliveryAddress => deliveryAddress.Id);

            builder.Property(deliveryAddress => deliveryAddress.UserId)
                .IsRequired();

            builder.Property(deliveryAddress => deliveryAddress.Address)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);

            builder.HasOne(deliveryAddress => deliveryAddress.User)
                .WithMany(user => user.DeliveryAddresses)
                .HasForeignKey(deliveryAddress => deliveryAddress.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}