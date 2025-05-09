using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class MovieGroupConfiguration : IEntityTypeConfiguration<MovieGroup>
    {
        public void Configure(EntityTypeBuilder<MovieGroup> builder)
        {
            builder.HasKey(mg => mg.Id);

            builder.Property(mg => mg.TitleObject)
                .HasColumnName("Title")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(mg => mg.CategoriesAsString)
                .HasColumnName("Categories")
                .IsRequired();

            builder.OwnsOne(mg => mg.Title);
        }
    }
}
