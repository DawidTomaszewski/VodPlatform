using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Enums;
using VodPlatform.Core.Domain.Exceptions.Aggregates;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Tests.Aggregates
{
    public class MovieGroupTests
    {
        private class TestMovie : Movie
        {
            public TestMovie(string title, Duration duration)
                : base(title, duration)
            {
            }
        }

        private MovieGroup CreateMovieGroup()
        {
            var title = new Title("Test Movie Group");
            var categories = new List<Category> { Category.Action, Category.Drama };
            return new MovieGroup(title, categories);
        }

        [Fact]
        public void Constructor_ShouldInitializeMovieGroup_WhenValidDataIsProvided()
        {
            // Arrange
            var title = new Title("Test Movie Group");
            var categories = new List<Category> { Category.Action, Category.Drama };

            // Act
            var movieGroup = new MovieGroup(title, categories);

            // Assert
            Assert.Equal("Test Movie Group", movieGroup.TitleObject);
            Assert.Equal(2, movieGroup.GetCategories().Count);
        }

        [Fact]
        public void AddMovie_ShouldThrowMovieGroupNullMovieException_WhenMovieIsNull()
        {
            // Arrange
            var movieGroup = CreateMovieGroup();

            // Act
            Action act = () => movieGroup.AddMovie(null);

            // Assert
            act.Should().Throw<MovieGroupNullMovieException>()
                .WithMessage("Cannot add a null movie.");
        }

        [Fact]
        public void AddMovie_ShouldThrowMovieGroupMovieAlreadyExistsException_WhenMovieWithSameIdExists()
        {
            // Arrange
            var movieGroup = CreateMovieGroup();
            var movie = new TestMovie("Movie 1", Duration.FromTotalSeconds(120));
            movieGroup.AddMovie(movie);

            // Act
            Action act = () => movieGroup.AddMovie(movie);

            // Assert
            act.Should().Throw<MovieGroupMovieAlreadyExistsException>()
                .WithMessage($"Movie with ID {movie.Id} already exists in the group.");
        }

        [Fact]
        public void AddMovie_ShouldAddMovie_WhenValidMovieIsProvided()
        {
            // Arrange
            var movieGroup = CreateMovieGroup();
            var movie = new TestMovie("Movie 1", Duration.FromTotalSeconds(120));

            // Act
            movieGroup.AddMovie(movie);

            // Assert
            var movies = movieGroup.GetMovies();
            Assert.Contains(movie, movies);
        }

        [Fact]
        public void RemoveMovieById_ShouldThrowMovieGroupMovieNotFoundException_WhenMovieDoesNotExist()
        {
            // Arrange
            var movieGroup = CreateMovieGroup();

            // Act
            Action act = () => movieGroup.RemoveMovieById(999);

            // Assert
            act.Should().Throw<MovieGroupMovieNotFoundException>()
                .WithMessage($"Movie with ID {999} not found in the group.");
        }

        [Fact]
        public void RemoveMovieById_ShouldRemoveMovie_WhenMovieExists()
        {
            // Arrange
            var movieGroup = CreateMovieGroup();
            var movie = new TestMovie("Movie 1", Duration.FromTotalSeconds(120));
            movieGroup.AddMovie(movie);

            // Act
            movieGroup.RemoveMovieById(movie.Id);

            // Assert
            var movies = movieGroup.GetMovies();
            Assert.DoesNotContain(movie, movies);
        }

        [Fact]
        public void GetTotalDuration_ShouldReturnCorrectTotalDuration()
        {
            // Arrange
            var movieGroup = CreateMovieGroup();
            var movie1 = new TestMovie("Movie 1", Duration.FromTotalSeconds(120));
            movie1.SetIdForTest(1);
            var movie2 = new TestMovie("Movie 2", Duration.FromTotalSeconds(180));
            movie2.SetIdForTest(2);
            movieGroup.AddMovie(movie1);
            movieGroup.AddMovie(movie2);

            // Act
            var totalDuration = movieGroup.GetTotalDuration();

            // Assert
            Assert.Equal(Duration.FromTotalSeconds(300), totalDuration);
        }
    }
}
