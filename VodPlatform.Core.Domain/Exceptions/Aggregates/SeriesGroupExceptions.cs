using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Aggregates
{
    public class SeriesGroupInvalidTitleException : Exception
    {
        public SeriesGroupInvalidTitleException() : base("SeriesGroup must have a valid title.") { }
    }

    public class EpisodeNotFoundException : Exception
    {
        public EpisodeNotFoundException(int id) : base($"Episode with ID {id} was not found in this SeriesGroup.") { }
    }
}
