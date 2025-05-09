using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Core.Domain.Repositories
{
    public interface IWatchedListRepository
    {
        Task<WatchedList?> GetByUserIdAsync(string userId);
        Task AddAsync(WatchedList list);
        Task RemoveAsync(WatchedList list);
        Task SaveChangesAsync();
    }
}
