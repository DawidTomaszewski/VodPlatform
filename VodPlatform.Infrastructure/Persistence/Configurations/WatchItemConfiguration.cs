using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class WatchItemConfiguration : IEntityTypeConfiguration<WatchItem>
    {
        public void Configure(EntityTypeBuilder<WatchItem> builder)
        {
            builder.HasKey(wi => wi.Id);

            builder.HasOne(wi => wi.Movie)
                .WithMany()
                .HasForeignKey(wi => wi.MovieId);

            builder.HasOne(wi => wi.SeriesGroup)
                .WithMany()
                .HasForeignKey(wi => wi.SeriesGroupId);

            builder.HasOne(wi => wi.Watchlist)
                .WithMany(wl => wl.Items)
                .HasForeignKey(wi => wi.WatchlistId);
        }
    }
}
