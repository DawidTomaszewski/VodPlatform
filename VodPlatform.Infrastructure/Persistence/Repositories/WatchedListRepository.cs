using Microsoft.EntityFrameworkCore;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Infrastructure.Persistence.Contexts;

public class WatchedListRepository : IWatchedListRepository
{
    private readonly VodPlatformDbContext _context;

    public WatchedListRepository(VodPlatformDbContext context) => _context = context;

    public async Task<WatchedList?> GetByUserIdAsync(string userId)
        => await _context.WatchedLists
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId);

    public async Task AddAsync(WatchedList list)
        => await _context.WatchedLists.AddAsync(list);

    public Task RemoveAsync(WatchedList list)
    {
        _context.WatchedLists.Remove(list);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
