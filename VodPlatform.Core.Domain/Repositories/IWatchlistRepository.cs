using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodPlatform.Core.Domain.Aggregates;

namespace VodPlatform.Core.Domain.Repositories
{
    public interface IWatchlistRepository
    {
        Task<Watchlist?> GetByUserIdAsync(string userId);
        Task AddAsync(Watchlist watchlist);
        Task RemoveAsync(Watchlist watchlist);
        Task SaveChangesAsync();
    }
}
