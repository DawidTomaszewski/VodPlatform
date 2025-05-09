using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Aggregates
{
    public class MovieAlreadyWatchedException : Exception
    {
        public MovieAlreadyWatchedException(int movieId)
            : base($"Movie with ID {movieId} has already been watched.") { }
    }

    public class EpisodeAlreadyWatchedException : Exception
    {
        public EpisodeAlreadyWatchedException(int episodeId)
            : base($"Episode with ID {episodeId} has already been watched.") { }
    }
}
