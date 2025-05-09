using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Core.Domain.Repositories
{
    public interface ISeriesGroupRepository
    {
        Task<SeriesGroup?> GetByIdAsync(int id);
        Task<IEnumerable<SeriesGroup>> GetAllAsync();
        Task AddAsync(SeriesGroup group);
        Task RemoveAsync(SeriesGroup group);
        Task SaveChangesAsync();
    }
}
