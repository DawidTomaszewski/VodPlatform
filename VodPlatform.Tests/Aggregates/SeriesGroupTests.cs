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
    public class SeriesGroupTests
    {
        private class TestEpisode : Episode
        {
            public TestEpisode(int episodeNumber, int seasonNumber, Duration duration)
                : base(episodeNumber, seasonNumber, duration)
            {
            }
        }

        private SeriesGroup CreateSeriesGroup()
        {
            var title = new Title("Test Series Group");
            var categories = new List<Category> { Category.Action, Category.Drama };
            return new SeriesGroup(title, categories);
        }

        [Fact]
        public void Constructor_ShouldInitializeSeriesGroup_WhenValidDataIsProvided()
        {
            // Arrange
            var title = new Title("Test Series Group");
            var categories = new List<Category> { Category.Action, Category.Drama };

            // Act
            var seriesGroup = new SeriesGroup(title, categories);

            // Assert
            Assert.Equal("Test Series Group", seriesGroup.TitleObject);
            Assert.Equal(2, seriesGroup.GetCategories().Count);
        }

        [Fact]
        public void AddEpisode_ShouldAddEpisode_WhenValidEpisodeIsProvided()
        {
            // Arrange
            var seriesGroup = CreateSeriesGroup();
            var episode = new TestEpisode(1, 1, Duration.FromTotalSeconds(45 * 60)); // numer odcinka, numer sezonu, czas trwania

            // Act
            seriesGroup.AddEpisode(episode);

            // Assert
            var episodes = seriesGroup.GetEpisodes();
            Assert.Contains(episode, episodes);
        }

        [Fact]
        public void RemoveEpisodeById_ShouldThrowEpisodeNotFoundException_WhenEpisodeDoesNotExist()
        {
            // Arrange
            var seriesGroup = CreateSeriesGroup();

            // Act
            Action act = () => seriesGroup.RemoveEpisodeById(999);

            // Assert
            act.Should().Throw<EpisodeNotFoundException>()
                .WithMessage($"Episode with ID {999} was not found in this SeriesGroup.");
        }

        [Fact]
        public void RemoveEpisodeById_ShouldRemoveEpisode_WhenEpisodeExists()
        {
            // Arrange
            var seriesGroup = CreateSeriesGroup();
            var episode = new TestEpisode(1, 1, Duration.FromTotalSeconds(45 * 60));
            seriesGroup.AddEpisode(episode);

            // Act
            seriesGroup.RemoveEpisodeById(episode.Id);

            // Assert
            var episodes = seriesGroup.GetEpisodes();
            Assert.DoesNotContain(episode, episodes);
        }

        [Fact]
        public void GetEpisodeById_ShouldThrowEpisodeNotFoundException_WhenEpisodeDoesNotExist()
        {
            // Arrange
            var seriesGroup = CreateSeriesGroup();

            // Act
            Action act = () => seriesGroup.GetEpisodeById(999);

            // Assert
            act.Should().Throw<EpisodeNotFoundException>()
                .WithMessage($"Episode with ID {999} was not found in this SeriesGroup.");
        }

        [Fact]
        public void GetEpisodeById_ShouldReturnEpisode_WhenEpisodeExists()
        {
            // Arrange
            var seriesGroup = CreateSeriesGroup();
            var episode = new TestEpisode(1, 1, Duration.FromTotalSeconds(45 * 60));
            seriesGroup.AddEpisode(episode);

            // Act
            var result = seriesGroup.GetEpisodeById(episode.Id);

            // Assert
            Assert.Equal(episode, result);
        }

        [Fact]
        public void GetTotalDuration_ShouldReturnCorrectTotalDuration()
        {
            // Arrange
            var seriesGroup = CreateSeriesGroup();
            var episode1 = new TestEpisode(1, 1, Duration.FromTotalSeconds(45 * 60));
            var episode2 = new TestEpisode(2, 1, Duration.FromTotalSeconds(60 * 60));
            seriesGroup.AddEpisode(episode1);
            seriesGroup.AddEpisode(episode2);

            // Act
            var totalDuration = seriesGroup.GetTotalDuration();

            // Assert
            Assert.Equal(Duration.FromTotalSeconds(45 * 60 + 60 * 60), totalDuration);
        }
    }
}
