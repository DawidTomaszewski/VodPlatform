using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Exceptions.Entities;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Tests.Entities
{
    public class MovieTests
    {
        [Fact]
        public void Constructor_ShouldCreateMovie_WhenValidDataIsProvided()
        {
            // Arrange
            var validTitle = "Inception";
            var validDuration = new Duration(7200); // 2 hours in seconds

            // Act
            var movie = new Movie(validTitle, validDuration);

            // Assert
            Assert.Equal(validTitle, movie.TitleObject);
            Assert.Equal(validDuration, movie.Duration);
            Assert.Equal(validTitle, movie.GetTitle().ToString());
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidMovieTitleException_WhenTitleIsNullOrEmpty()
        {
            // Arrange
            var invalidTitle = ""; // invalid title
            var validDuration = new Duration(7200);

            // Act
            Action act = () => new Movie(invalidTitle, validDuration);

            // Assert
            act.Should().Throw<InvalidMovieTitleException>()
                .WithMessage($"Movie title '{invalidTitle}' is invalid. It cannot be null, empty, or whitespace.");
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidMovieTitleException_WhenTitleIsWhitespace()
        {
            // Arrange
            var invalidTitle = "   "; // invalid title (only whitespace)
            var validDuration = new Duration(7200);

            // Act
            Action act = () => new Movie(invalidTitle, validDuration);

            // Assert
            act.Should().Throw<InvalidMovieTitleException>()
                .WithMessage($"Movie title '{invalidTitle}' is invalid. It cannot be null, empty, or whitespace.");
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidMovieDurationException_WhenDurationIsNullOrZeroOrNegative()
        {
            // Arrange
            var validTitle = "Inception";
            var invalidDuration = new Duration(0); // invalid duration (0 seconds)

            // Act
            Action act = () => new Movie(validTitle, invalidDuration);

            // Assert
            act.Should().Throw<InvalidMovieDurationException>()
                .WithMessage("Movie duration must be greater than 0.");
        }
    }
}
