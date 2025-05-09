using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Exceptions.Entities;
using VodPlatform.Core.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VodPlatform.Tests.Entities
{
    public class EpisodeTests
    {
        [Fact]
        public void Constructor_ShouldCreateEpisode_WhenValidDataIsProvided()
        {
            // Arrange
            var episodeNumber = 1;
            var seasonNumber = 1;
            var duration = new Duration(3600); // 1 hour in seconds

            // Act
            var episode = new Episode(episodeNumber, seasonNumber, duration);

            // Assert
            Assert.Equal(episodeNumber, episode.EpisodeNumber);
            Assert.Equal(seasonNumber, episode.SeasonNumber);
            Assert.Equal(duration, episode.Duration);
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidEpisodeNumberException_WhenEpisodeNumberIsZeroOrNegative()
        {
            // Arrange
            var invalidEpisodeNumber = 0; // invalid episode number
            var seasonNumber = 1;
            var duration = new Duration(3600);

            // Act
            Action act = () => new Episode(invalidEpisodeNumber, seasonNumber, duration);

            // Assert
            act.Should().Throw<InvalidEpisodeNumberException>()
                .WithMessage($"Episode number '{invalidEpisodeNumber}' must be greater than 0.");
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidSeasonNumberException_WhenSeasonNumberIsZeroOrNegative()
        {
            // Arrange
            var episodeNumber = 1;
            var invalidSeasonNumber = 0; // invalid season number
            var duration = new Duration(3600);

            // Act
            Action act = () => new Episode(episodeNumber, invalidSeasonNumber, duration);

            // Assert
            act.Should().Throw<InvalidSeasonNumberException>()
                .WithMessage($"Season number '{invalidSeasonNumber}' must be greater than 0.");
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidEpisodeDurationException_WhenDurationIsZeroOrNegative()
        {
            // Arrange
            var episodeNumber = 1;
            var seasonNumber = 1;
            var invalidDuration = new Duration(0); // invalid duration (0 seconds)

            // Act
            Action act = () => new Episode(episodeNumber, seasonNumber, invalidDuration);

            // Assert
            act.Should().Throw<InvalidEpisodeDurationException>()
                .WithMessage("Duration of the episode must be greater than 0.");
        }
    }
}
