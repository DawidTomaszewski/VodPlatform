using Microsoft.EntityFrameworkCore;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Infrastructure.Persistence.Contexts;

public class SeriesGroupRepository : ISeriesGroupRepository
{
    private readonly VodPlatformDbContext _context;

    public SeriesGroupRepository(VodPlatformDbContext context) => _context = context;

    public async Task<SeriesGroup?> GetByIdAsync(int id)
    {
        return await _context.SeriesGroups
                             .Include(g => g.Episodes)
                             .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<SeriesGroup>> GetAllAsync()
    {
        return await _context.SeriesGroups
                     .Include(g => g.Episodes)
                     .ToListAsync();
    }

    public async Task AddAsync(SeriesGroup group)
        => await _context.SeriesGroups.AddAsync(group);

    public Task RemoveAsync(SeriesGroup group)
    {
        _context.SeriesGroups.Remove(group);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
