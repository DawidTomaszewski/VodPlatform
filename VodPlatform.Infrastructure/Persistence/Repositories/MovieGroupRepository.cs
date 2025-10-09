using Microsoft.EntityFrameworkCore;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Infrastructure.Persistence.Contexts;

public class MovieGroupRepository : IMovieGroupRepository
{
    private readonly VodPlatformDbContext _context;

    public MovieGroupRepository(VodPlatformDbContext context) => _context = context;

    public async Task<MovieGroup?> GetByIdAsync(int id)
    {
        return await _context.MovieGroups
                             .Include(g => g.Movies)
                             .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<MovieGroup>> GetAllAsync()
    {
        return await _context.MovieGroups
                             .Include(g => g.Movies)
                             .ToListAsync();
    }

    public async Task AddAsync(MovieGroup group)
        => await _context.MovieGroups.AddAsync(group);

    public Task RemoveAsync(MovieGroup group)
    {
        _context.MovieGroups.Remove(group);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
