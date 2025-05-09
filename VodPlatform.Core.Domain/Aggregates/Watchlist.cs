using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Exceptions.Aggregates;

namespace VodPlatform.Core.Domain.Aggregates
{
    public class Watchlist
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public ICollection<WatchItem> Items { get; private set; } = new List<WatchItem>();

        public Watchlist() { }

        public Watchlist(string userId)
        {
            UserId = userId;
        }

        public void AddMovie(int? movieId)
        {
            if (Items.Any(i => i.MovieId == movieId))
                throw new MovieAlreadyInWatchlistException();

            Items.Add(new WatchItem(movieId, null, Id));
        }

        public void AddSeries(int? seriesGroupId)
        {
            if (Items.Any(i => i.SeriesGroupId == seriesGroupId))
                throw new SeriesAlreadyInWatchlistException();

            Items.Add(new WatchItem(null, seriesGroupId, Id));
        }

        public void RemoveItem(int itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new WatchItemNotFoundException();

            Items.Remove(item);
        }

        public List<WatchItem?> GetWatchItems() => Items.ToList();

        public WatchItem? GetWatchItemById(int id) => Items.FirstOrDefault(m => m.Id == id);
    }
}
