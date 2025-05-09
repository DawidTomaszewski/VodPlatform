using System.Collections.Generic;
using System.Threading.Tasks;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Core.Domain.Repositories
{
    public interface IMovieGroupRepository
    {
        Task<MovieGroup?> GetByIdAsync(int id);
        Task<IEnumerable<MovieGroup>> GetAllAsync();
        Task AddAsync(MovieGroup group);
        Task RemoveAsync(MovieGroup group);
        Task SaveChangesAsync();
    }
}
