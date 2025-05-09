using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Exceptions.Entities;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Domain.Entities
{
    public class Movie
    {
        public int Id { get; private set; }
        public Title Title { get; protected set; }

        public string TitleObject { get; private set; }
        public Duration Duration { get; private set; }

        public int MovieGroupId { get; set; }
        public MovieGroup MovieGroup { get; set; }

        public Movie() { }

        public Movie(string title, Duration duration)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidMovieTitleException(title);

            if (duration == null || duration.ToTotalSeconds() <= 0)
                throw new InvalidMovieDurationException();

            TitleObject = title;
            Duration = duration;
            Title = new Title(title);
        }
#if DEBUG
        public void SetIdForTest(int id)
        {
            Id = id;
        }
#endif
        public Title GetTitle()
        {
            return new Title(TitleObject);
        }
    }
}
