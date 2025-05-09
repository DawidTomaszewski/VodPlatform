using Microsoft.EntityFrameworkCore;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Infrastructure.Persistence.Contexts;

public class WatchlistRepository : IWatchlistRepository
{
    private readonly VodPlatformDbContext _context;

    public WatchlistRepository(VodPlatformDbContext context) => _context = context;

    public async Task<Watchlist?> GetByUserIdAsync(string userId)
        => await _context.Watchlists
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId);

    public async Task AddAsync(Watchlist watchlist)
        => await _context.Watchlists.AddAsync(watchlist);

    public Task RemoveAsync(Watchlist watchlist)
    {
        _context.Watchlists.Remove(watchlist);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
