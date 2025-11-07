using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.ValueObjects;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Infrastructure.Persistence.Contexts
{
    public class VodPlatformDbContext : IdentityDbContext<ApplicationUser>
    {
        public VodPlatformDbContext(DbContextOptions<VodPlatformDbContext> options)
            : base(options) { }

        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<WatchedList> WatchedLists { get; set; }
        public DbSet<WatchItem> WatchItems { get; set; }
        public DbSet<WatchedItem> WatchedItems { get; set; }

        public DbSet<MovieGroup> MovieGroups { get; set; }
        public DbSet<SeriesGroup> SeriesGroups { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Duration> Durations { get; set; }
        public DbSet<ApplicationUser> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VodPlatformDbContext).Assembly);
        }
    }
}
