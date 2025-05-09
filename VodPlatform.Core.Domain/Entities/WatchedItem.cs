using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Exceptions.Entities;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Domain.Entities
{
    public class WatchedItem
    {
        public int Id { get; private set; }
        public DateOnly WatchedAt { get; private set; }
        public Duration EndWatch { get; private set; }

        public int? MovieId { get; private set; }
        public Movie Movie { get; set; }

        public int? EpisodeId { get; private set; }
        public Episode Episode { get; private set; }

        public WatchedList WatchedList { get; private set; }
        public int WatchedListId { get; private set; }

        public WatchedItem() { }

        public WatchedItem(int? movieId, int? episodeId, int watchedListId, int time)
        {
            if (movieId == null && episodeId == null)
                throw new WatchedItemInvalidTargetException();

            if (time <= 0)
                throw new WatchedItemInvalidDurationException();

            MovieId = movieId;
            EpisodeId = episodeId;
            WatchedListId = watchedListId;
            WatchedAt = DateOnly.FromDateTime(DateTime.Now);
            EndWatch = Duration.FromTotalSeconds(time);
        }

#if DEBUG
        public void SetIdForTest(int id)
        {
            Id = id;
        }
#endif
        public void UpdateEndWatch(int time)
        {
            if (time <= 0)
                throw new WatchedItemInvalidDurationException();

            WatchedAt = DateOnly.FromDateTime(DateTime.Now);
            EndWatch = Duration.FromTotalSeconds(time);
        }
    }
}
