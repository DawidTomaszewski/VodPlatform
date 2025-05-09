using System.Collections.Generic;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Enums;
using VodPlatform.Core.Domain.Exceptions.Aggregates;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Domain.Aggregates
{
    public class SeriesGroup : ContentGroupBase
    {
        public int Id { get; set; }
        public List<Episode> Episodes { get; set; } = new List<Episode>();

        public SeriesGroup() { }

        public SeriesGroup(Title title, IEnumerable<Category> categories)
            : base(title?.ToString() ?? throw new SeriesGroupInvalidTitleException(), categories)
        {
        }

        public void AddEpisode(Episode episode)
        {
            Episodes.Add(episode);
        }

        public void RemoveEpisodeById(int id)
        {
            var episode = Episodes.FirstOrDefault(e => e.Id == id)
                          ?? throw new EpisodeNotFoundException(id);
            Episodes.Remove(episode);
        }

        public Episode GetEpisodeById(int id)
        {
            return Episodes.FirstOrDefault(e => e.Id == id)
                   ?? throw new EpisodeNotFoundException(id);
        }

        public List<Episode> GetEpisodes() => Episodes.ToList();

        public override Duration GetTotalDuration()
        {
            var totalSeconds = Episodes.Sum(e => e.Duration.TotalSeconds);
            return Duration.FromTotalSeconds(totalSeconds);
        }
    }
}
