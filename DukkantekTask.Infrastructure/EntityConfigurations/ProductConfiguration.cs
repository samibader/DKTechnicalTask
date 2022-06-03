using DukkantekTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DukkantekTask.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// FluentApi configuration mapping for Product
    /// </summary>
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.BarCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Weight)
                .HasPrecision(10,2)
                .IsRequired();

            builder.Property(c => c.Description)
                .IsFixedLength(false)
                .IsRequired(false);

        }
    }
}
