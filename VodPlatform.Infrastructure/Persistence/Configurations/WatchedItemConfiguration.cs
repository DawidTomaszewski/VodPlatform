using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class WatchedItemConfiguration : IEntityTypeConfiguration<WatchedItem>
    {
        public void Configure(EntityTypeBuilder<WatchedItem> builder)
        {
            builder.HasKey(wi => wi.Id);

            builder.HasOne(wi => wi.Movie)
                .WithMany()
                .HasForeignKey(wi => wi.MovieId);

            builder.HasOne(wi => wi.Episode)
                .WithMany()
                .HasForeignKey(wi => wi.EpisodeId);

            builder.HasOne(wi => wi.WatchedList)
                .WithMany(wl => wl.Items)
                .HasForeignKey(wi => wi.WatchedListId);

            builder.OwnsOne(wi => wi.EndWatch);
        }
    }
}
