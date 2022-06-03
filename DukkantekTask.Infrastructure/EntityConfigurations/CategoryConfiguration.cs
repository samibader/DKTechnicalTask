using DukkantekTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DukkantekTask.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// FluentApi configuration mapping for Category
    /// </summary>
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
