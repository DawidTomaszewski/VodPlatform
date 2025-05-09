using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Exceptions.Aggregates;

namespace VodPlatform.Tests.Aggregates
{
    public class WatchlistTests
    {
        private Watchlist CreateWatchlist(string userId)
        {
            return new Watchlist(userId);
        }

        [Fact]
        public void Constructor_ShouldInitializeWatchlist_WhenValidUserIdIsProvided()
        {
            // Arrange
            var userId = "user123";

            // Act
            var watchlist = new Watchlist(userId);

            // Assert
            Assert.Equal(userId, watchlist.UserId);
            Assert.Empty(watchlist.GetWatchItems());
        }

        [Fact]
        public void AddMovie_ShouldAddMovie_WhenMovieIsNotAlreadyInWatchlist()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");

            // Act
            watchlist.AddMovie(1);

            // Assert
            var watchItem = watchlist.GetWatchItems().FirstOrDefault();
            Assert.NotNull(watchItem);
            Assert.Equal(1, watchItem.MovieId);
        }

        [Fact]
        public void AddMovie_ShouldThrowMovieAlreadyInWatchlistException_WhenMovieIsAlreadyInWatchlist()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");
            watchlist.AddMovie(1);

            // Act
            Action act = () => watchlist.AddMovie(1);

            // Assert
            act.Should().Throw<MovieAlreadyInWatchlistException>()
                .WithMessage("The film is already on the list.");
        }

        [Fact]
        public void AddSeries_ShouldAddSeries_WhenSeriesIsNotAlreadyInWatchlist()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");

            // Act
            watchlist.AddSeries(1);

            // Assert
            var watchItem = watchlist.GetWatchItems().FirstOrDefault();
            Assert.NotNull(watchItem);
            Assert.Equal(1, watchItem.SeriesGroupId);
        }

        [Fact]
        public void AddSeries_ShouldThrowSeriesAlreadyInWatchlistException_WhenSeriesIsAlreadyInWatchlist()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");
            watchlist.AddSeries(1);

            // Act
            Action act = () => watchlist.AddSeries(1);

            // Assert
            act.Should().Throw<SeriesAlreadyInWatchlistException>()
                .WithMessage("The series is already on the list.");
        }

        [Fact]
        public void RemoveItem_ShouldRemoveItem_WhenItemExists()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");
            watchlist.AddMovie(1);
            watchlist.Items.First().SetIdForTest(1);
            // Act
            watchlist.RemoveItem(1);

            // Assert
            var watchItem = watchlist.GetWatchItems().FirstOrDefault();
            Assert.Null(watchItem);
        }

        [Fact]
        public void RemoveItem_ShouldThrowWatchItemNotFoundException_WhenItemDoesNotExist()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");

            // Act
            Action act = () => watchlist.RemoveItem(999);

            // Assert
            act.Should().Throw<WatchItemNotFoundException>()
                .WithMessage("Item not found in the list.");
        }

        [Fact]
        public void GetWatchItemById_ShouldReturnWatchItem_WhenItExists()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");
            watchlist.AddMovie(1);
            watchlist.Items.First().SetIdForTest(1);
            // Act
            var watchItem = watchlist.GetWatchItemById(1);

            // Assert
            Assert.NotNull(watchItem);
            Assert.Equal(1, watchItem.MovieId);
        }

        [Fact]
        public void GetWatchItemById_ShouldReturnNull_WhenItemDoesNotExist()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");

            // Act
            var watchItem = watchlist.GetWatchItemById(999);

            // Assert
            Assert.Null(watchItem);
        }

        [Fact]
        public void GetWatchItems_ShouldReturnAllWatchItems()
        {
            // Arrange
            var watchlist = CreateWatchlist("user123");
            watchlist.AddMovie(1);
            watchlist.AddSeries(2);

            // Act
            var items = watchlist.GetWatchItems();

            // Assert
            Assert.Equal(2, items.Count);
            Assert.Contains(items, item => item.MovieId == 1);
            Assert.Contains(items, item => item.SeriesGroupId == 2);
        }
    }
}
