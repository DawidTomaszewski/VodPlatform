using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
    {
        public void Configure(EntityTypeBuilder<Watchlist> builder)
        {
            builder.HasKey(wl => wl.Id);

            builder.Property(wl => wl.UserId)
                .IsRequired();
        }
    }
}
