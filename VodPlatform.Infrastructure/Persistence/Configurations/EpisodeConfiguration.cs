using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.Duration);
        }
    }
}
