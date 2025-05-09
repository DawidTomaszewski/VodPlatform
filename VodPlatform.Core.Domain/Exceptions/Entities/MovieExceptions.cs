using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Entities
{
    public class InvalidMovieTitleException : Exception
    {
        public InvalidMovieTitleException(string title)
            : base($"Movie title '{title}' is invalid. It cannot be null, empty, or whitespace.") { }
    }

    public class InvalidMovieDurationException : Exception
    {
        public InvalidMovieDurationException()
            : base("Movie duration must be greater than 0.") { }
    }
}
