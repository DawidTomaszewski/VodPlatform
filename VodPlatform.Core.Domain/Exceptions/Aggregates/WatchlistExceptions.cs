using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Aggregates
{
    public class WatchlistException : Exception
    {
        public WatchlistException(string message) : base(message) { }
    }

    public class MovieAlreadyInWatchlistException : WatchlistException
    {
        public MovieAlreadyInWatchlistException()
            : base("The film is already on the list.") { }
    }

    public class SeriesAlreadyInWatchlistException : WatchlistException
    {
        public SeriesAlreadyInWatchlistException()
            : base("The series is already on the list.") { }
    }

    public class WatchItemNotFoundException : WatchlistException
    {
        public WatchItemNotFoundException()
            : base("Item not found in the list.") { }
    }
}
