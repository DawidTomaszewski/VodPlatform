using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Entities
{
    public class InvalidEpisodeNumberException : Exception
    {
        public InvalidEpisodeNumberException(int number)
            : base($"Episode number '{number}' must be greater than 0.") { }
    }

    public class InvalidSeasonNumberException : Exception
    {
        public InvalidSeasonNumberException(int number)
            : base($"Season number '{number}' must be greater than 0.") { }
    }

    public class InvalidEpisodeDurationException : Exception
    {
        public InvalidEpisodeDurationException()
            : base("Duration of the episode must be greater than 0.") { }
    }
}
