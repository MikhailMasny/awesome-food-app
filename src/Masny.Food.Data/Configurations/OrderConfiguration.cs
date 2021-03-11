using Masny.Food.Data.Constants;
using Masny.Food.Data.Enums;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Order entity.
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Orders, Schema.Order)
                .HasKey(order => order.Id);

            builder.Property(order => order.Creation)
                .HasDefaultValue(DateTime.UnixEpoch);

            builder.Property(order => order.UserId)
                .IsRequired();

            builder.Property(order => order.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);

            builder.Property(order => order.Phone)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(order => order.Address)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);

            builder.Property(order => order.PromoCode)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(order => order.Comment)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.Property(order => order.Status)
                .HasDefaultValue(StatusType.Unknown) // UNDONE: delete it
                .HasConversion(new EnumToNumberConverter<StatusType, int>());

            builder.HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
