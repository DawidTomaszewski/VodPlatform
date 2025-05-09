using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Exceptions;
using VodPlatform.Core.Domain.Exceptions.Entities;

namespace VodPlatform.Core.Domain.Entities
{
    public class WatchItem
    {
        public int Id { get; private set; }

        public int? MovieId { get; private set; }
        public Movie Movie { get; private set; }

        public int? SeriesGroupId { get; private set; }
        public SeriesGroup SeriesGroup { get; private set; }

        public int WatchlistId { get; private set; }
        public Watchlist Watchlist { get; set; }

        public WatchItem() { }

        public WatchItem(int? movieId, int? seriesGroupId, int watchlistId)
        {
            if (movieId == null && seriesGroupId == null)
                throw new WatchItemInvalidTargetException();

            MovieId = movieId;
            SeriesGroupId = seriesGroupId;
            WatchlistId = watchlistId;
        }

#if DEBUG
        public void SetIdForTest(int id)
        {
            Id = id;
        }
#endif
    }
}
