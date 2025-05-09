using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Infrastructure.Persistence.Configurations
{
    public class WatchedListConfiguration : IEntityTypeConfiguration<WatchedList>
    {
        public void Configure(EntityTypeBuilder<WatchedList> builder)
        {
            builder.HasKey(wl => wl.Id);

            builder.Property(wl => wl.UserId)
                .IsRequired();
        }
    }
}
