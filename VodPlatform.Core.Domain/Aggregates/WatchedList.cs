using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Exceptions.Aggregates;

namespace VodPlatform.Core.Domain.Aggregates
{
    public class WatchedList
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public ICollection<WatchedItem> Items { get; private set; } = new List<WatchedItem>();

        public WatchedList() { }

        public WatchedList(string userId)
        {
            UserId = userId;
        }

        public void AddMovie(int? movieId, int time)
        {
            if (Items.Any(i => i.MovieId == movieId))
                throw new MovieAlreadyWatchedException(movieId ?? 0);

            Items.Add(new WatchedItem(movieId, null, Id, time));
        }

        public void AddEpisode(int? episodeId, int time)
        {
            if (Items.Any(i => i.EpisodeId == episodeId))
                throw new EpisodeAlreadyWatchedException(episodeId ?? 0);

            Items.Add(new WatchedItem(null, episodeId, Id, time));
        }

        public List<WatchedItem?> GetWatchedItems() => Items.ToList();

        public WatchedItem? GetWatchedItemById(int id) => Items.FirstOrDefault(m => m.Id == id);
    }
}
