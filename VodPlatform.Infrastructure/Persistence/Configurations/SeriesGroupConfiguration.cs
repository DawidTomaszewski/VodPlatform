using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class SeriesGroupConfiguration : IEntityTypeConfiguration<SeriesGroup>
    {
        public void Configure(EntityTypeBuilder<SeriesGroup> builder)
        {
            builder.HasKey(sg => sg.Id);

            builder.Property(sg => sg.TitleObject)
                .HasColumnName("Title")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(sg => sg.CategoriesAsString)
                .HasColumnName("Categories")
                .IsRequired();

            builder.OwnsOne(sg => sg.Title);
        }
    }
}
