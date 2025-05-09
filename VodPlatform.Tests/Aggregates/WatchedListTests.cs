using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Entities;
using VodPlatform.Core.Domain.Exceptions.Aggregates;

namespace VodPlatform.Tests.Aggregates
{
    public class WatchedListTests
    {
        private WatchedList CreateWatchedList(string userId)
        {
            return new WatchedList(userId);
        }

        [Fact]
        public void Constructor_ShouldInitializeWatchedList_WhenValidUserIdIsProvided()
        {
            // Arrange
            var userId = "user123";

            // Act
            var watchedList = new WatchedList(userId);

            // Assert
            Assert.Equal(userId, watchedList.UserId);
            Assert.Empty(watchedList.GetWatchedItems());
        }

        [Fact]
        public void AddMovie_ShouldAddMovie_WhenMovieIsNotAlreadyWatched()
        {
            // Arrange
            var watchedList = CreateWatchedList("user123");

            // Act
            watchedList.AddMovie(1, 120);

            // Assert
            var watchedItem = watchedList.GetWatchedItems().FirstOrDefault();
            Assert.NotNull(watchedItem);
            Assert.Equal(1, watchedItem.MovieId);
        }

        [Fact]
        public void AddMovie_ShouldThrowMovieAlreadyWatchedException_WhenMovieIsAlreadyWatched()
        {
            // Arrange
            var watchedList = CreateWatchedList("user123");
            watchedList.AddMovie(1, 120);

            // Act
            Action act = () => watchedList.AddMovie(1, 90);

            // Assert
            act.Should().Throw<MovieAlreadyWatchedException>()
                .WithMessage($"Movie with ID {1} has already been watched.");
        }

        [Fact]
        public void AddEpisode_ShouldAddEpisode_WhenEpisodeIsNotAlreadyWatched()
        {
            // Arrange
            var watchedList = CreateWatchedList("user123");

            // Act
            watchedList.AddEpisode(1, 60);

            // Assert
            var watchedItem = watchedList.GetWatchedItems().FirstOrDefault();
            Assert.NotNull(watchedItem);
            Assert.Equal(1, watchedItem.EpisodeId);
        }

        [Fact]
        public void AddEpisode_ShouldThrowEpisodeAlreadyWatchedException_WhenEpisodeIsAlreadyWatched()
        {
            // Arrange
            var watchedList = CreateWatchedList("user123");
            watchedList.AddEpisode(1, 60);

            // Act
            Action act = () => watchedList.AddEpisode(1, 45);

            // Assert
            act.Should().Throw<EpisodeAlreadyWatchedException>()
                .WithMessage($"Episode with ID {1} has already been watched.");
        }

        [Fact]
        public void GetWatchedItemById_ShouldReturnWatchedItem_WhenItExists()
        {
            // Arrange
            var watchedList = CreateWatchedList("user123");
            watchedList.AddMovie(1, 120);
            watchedList.Items.First().SetIdForTest(1);

            // Act
            var watchedItem = watchedList.GetWatchedItemById(1);

            // Assert
            Assert.NotNull(watchedItem);
            Assert.Equal(1, watchedItem.MovieId);
        }

        [Fact]
        public void GetWatchedItemById_ShouldReturnNull_WhenItemDoesNotExist()
        {
            // Arrange
            var watchedList = CreateWatchedList("user123");

            // Act
            var watchedItem = watchedList.GetWatchedItemById(999);

            // Assert
            Assert.Null(watchedItem);
        }

        [Fact]
        public void GetWatchedItems_ShouldReturnAllWatchedItems()
        {
            // Arrange
            var watchedList = CreateWatchedList("user123");
            watchedList.AddMovie(1, 120);
            watchedList.AddEpisode(2, 45);

            // Act
            var items = watchedList.GetWatchedItems();

            // Assert
            Assert.Equal(2, items.Count);
            Assert.Contains(items, item => item.MovieId == 1);
            Assert.Contains(items, item => item.EpisodeId == 2);
        }
    }
}
