using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.TitleObject)
                .HasColumnName("Title")
                .HasMaxLength(255)
                .IsRequired();

            builder.OwnsOne(m => m.Duration);
            builder.OwnsOne(m => m.Title);
        }
    }
}
