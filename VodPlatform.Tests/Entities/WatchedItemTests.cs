using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Exceptions.Entities;

namespace VodPlatform.Tests.Entities
{
    public class WatchedItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateWatchedItem_WhenValidDataIsProvided()
        {
            // Arrange
            int? validMovieId = 1;
            int? validEpisodeId = null;
            int validWatchedListId = 2;
            int validTime = 3600; // 1 hour in seconds

            // Act
            var watchedItem = new WatchedItem(validMovieId, validEpisodeId, validWatchedListId, validTime);

            // Assert
            Assert.Equal(validMovieId, watchedItem.MovieId);
            Assert.Null(watchedItem.EpisodeId);
            Assert.Equal(validWatchedListId, watchedItem.WatchedListId);
            Assert.True(watchedItem.EndWatch.ToTotalSeconds() > 0); // Assert that the duration is valid
        }

        [Fact]
        public void Constructor_ShouldThrowWatchedItemInvalidTargetException_WhenMovieIdAndEpisodeIdAreNull()
        {
            // Arrange
            int? invalidMovieId = null;
            int? invalidEpisodeId = null;
            int validWatchedListId = 2;
            int validTime = 3600;

            // Act
            Action act = () => new WatchedItem(invalidMovieId, invalidEpisodeId, validWatchedListId, validTime);

            // Assert
            act.Should().Throw<WatchedItemInvalidTargetException>()
                .WithMessage("A watched item must have either a MovieId or an EpisodeId.");
        }

        [Fact]
        public void Constructor_ShouldThrowWatchedItemInvalidDurationException_WhenTimeIsLessThanOrEqualToZero()
        {
            // Arrange
            int? validMovieId = 1;
            int? validEpisodeId = null;
            int validWatchedListId = 2;
            int invalidTime = 0; // invalid duration

            // Act
            Action act = () => new WatchedItem(validMovieId, validEpisodeId, validWatchedListId, invalidTime);

            // Assert
            act.Should().Throw<WatchedItemInvalidDurationException>()
                .WithMessage("Watched duration must be greater than 0 seconds.");
        }

        [Fact]
        public void UpdateEndWatch_ShouldUpdateEndWatch_WhenValidTimeIsProvided()
        {
            // Arrange
            int? validMovieId = 1;
            int? validEpisodeId = null;
            int validWatchedListId = 2;
            int validTime = 3600;

            var watchedItem = new WatchedItem(validMovieId, validEpisodeId, validWatchedListId, validTime);

            // Act
            int newTime = 7200; // 2 hours in seconds
            watchedItem.UpdateEndWatch(newTime);

            // Assert
            Assert.Equal(newTime, watchedItem.EndWatch.ToTotalSeconds());
        }

        [Fact]
        public void UpdateEndWatch_ShouldThrowWatchedItemInvalidDurationException_WhenTimeIsLessThanOrEqualToZero()
        {
            // Arrange
            int? validMovieId = 1;
            int? validEpisodeId = null;
            int validWatchedListId = 2;
            int validTime = 3600;

            var watchedItem = new WatchedItem(validMovieId, validEpisodeId, validWatchedListId, validTime);

            // Act
            Action act = () => watchedItem.UpdateEndWatch(0);

            // Assert
            act.Should().Throw<WatchedItemInvalidDurationException>()
                .WithMessage("Watched duration must be greater than 0 seconds.");
        }
    }
}
