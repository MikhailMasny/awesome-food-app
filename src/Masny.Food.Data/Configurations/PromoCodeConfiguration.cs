using Masny.Food.Data.Constants;
using Masny.Food.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Masny.Food.Data.Configurations
{
    /// <summary>
    /// EF Configuration for PromoCode entity.
    /// </summary>
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.PromoCodes, Schema.Ad)
                .HasKey(promoCode => promoCode.Id);

            builder.Property(promoCode => promoCode.Code)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(promoCode => promoCode.Comment)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);
        }
    }
}
