using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Aggregates
{
    public class MovieGroupInvalidTitleException : Exception
    {
        public MovieGroupInvalidTitleException() : base("Title cannot be null or empty.") { }
    }

    public class MovieGroupNullMovieException : Exception
    {
        public MovieGroupNullMovieException() : base("Cannot add a null movie.") { }
    }

    public class MovieGroupMovieAlreadyExistsException : Exception
    {
        public MovieGroupMovieAlreadyExistsException(int movieId)
            : base($"Movie with ID {movieId} already exists in the group.") { }
    }

    public class MovieGroupMovieNotFoundException : Exception
    {
        public MovieGroupMovieNotFoundException(int movieId)
            : base($"Movie with ID {movieId} not found in the group.") { }
    }
}
