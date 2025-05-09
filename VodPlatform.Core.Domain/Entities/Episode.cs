using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Exceptions.Entities;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Domain.Entities
{
    public class Episode
    {
        public int Id { get; private set; }
        public int EpisodeNumber { get; private set; }
        public int SeasonNumber { get; private set; }
        public Duration Duration { get; private set; }

        public int SeriesGroupId { get; set; }
        public SeriesGroup SeriesGroup { get; set; }

        public Episode() { }

        public Episode(int episodeNumber, int seasonNumber, Duration duration)
        {
            if (episodeNumber <= 0)
                throw new InvalidEpisodeNumberException(episodeNumber);

            if (seasonNumber <= 0)
                throw new InvalidSeasonNumberException(seasonNumber);

            if (duration == null || duration.ToTotalSeconds() <= 0)
                throw new InvalidEpisodeDurationException();

            EpisodeNumber = episodeNumber;
            SeasonNumber = seasonNumber;
            Duration = duration;
        }
    }
}
