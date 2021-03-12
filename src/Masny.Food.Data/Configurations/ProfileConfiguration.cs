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
    /// EF Configuration for Profile entity.
    /// </summary>
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Profiles, Schema.User)
                .HasKey(profile => profile.Id);

            builder.Property(profile => profile.UserId)
                .IsRequired();

            builder.Property(profile => profile.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);

            builder.Property(profile => profile.Gender)
                .HasDefaultValue(GenderType.Unknown)
                .HasConversion(new EnumToNumberConverter<GenderType, int>());

            builder.Property(profile => profile.BirthDate)
                .HasDefaultValue(DateTime.UnixEpoch)
                .HasColumnType(SqlConfiguration.SqlDateFormat);

            builder.Property(profile => profile.Address)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);

            builder.HasOne(profile => profile.User)
                .WithOne(user => user.Profile)
                .HasForeignKey<Profile>(profile => profile.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
