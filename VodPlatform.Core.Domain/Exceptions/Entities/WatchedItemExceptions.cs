using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Entities
{
    public class WatchedItemInvalidTargetException : Exception
    {
        public WatchedItemInvalidTargetException()
            : base("A watched item must have either a MovieId or an EpisodeId.") { }
    }

    public class WatchedItemInvalidDurationException : Exception
    {
        public WatchedItemInvalidDurationException()
            : base("Watched duration must be greater than 0 seconds.") { }
    }
}
