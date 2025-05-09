using System.Collections.Generic;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Enums;
using VodPlatform.Core.Domain.Exceptions.Aggregates;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Domain.Aggregates
{
    public class MovieGroup : ContentGroupBase
    {
        public int Id { get;  set; }
        public List<Movie> Movies { get;  set; } = new List<Movie>();

        public MovieGroup() { }

        public MovieGroup(Title title, IEnumerable<Category> categories)
            : base(title?.ToString() ?? throw new MovieGroupInvalidTitleException(), categories)
        {
            Title = title;
        }

        public void AddMovie(Movie movie)
        {
            if (movie == null)
                throw new MovieGroupNullMovieException();

            if (Movies.Any(m => m.Id == movie.Id))
                throw new MovieGroupMovieAlreadyExistsException(movie.Id);

            Movies.Add(movie);
        }

        public void RemoveMovieById(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                throw new MovieGroupMovieNotFoundException(id);

            Movies.Remove(movie);
        }

        public Movie? GetMovieById(int id) => Movies.FirstOrDefault(m => m.Id == id);

        public List<Movie?> GetMovies() => Movies.ToList();

        public override Duration GetTotalDuration()
        {
            var totalSeconds = Movies.Sum(m => m.Duration.TotalSeconds);
            return Duration.FromTotalSeconds(totalSeconds);
        }
    }
}
