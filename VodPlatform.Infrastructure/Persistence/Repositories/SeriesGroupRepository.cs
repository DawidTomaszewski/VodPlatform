using Microsoft.EntityFrameworkCore;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Infrastructure.Persistence.Contexts;

public class SeriesGroupRepository : ISeriesGroupRepository
{
    private readonly VodPlatformDbContext _context;

    public SeriesGroupRepository(VodPlatformDbContext context) => _context = context;

    public async Task<SeriesGroup?> GetByIdAsync(int id)
        => await _context.SeriesGroups.FindAsync(id);

    public async Task<IEnumerable<SeriesGroup>> GetAllAsync()
        => await _context.SeriesGroups.ToListAsync();

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
