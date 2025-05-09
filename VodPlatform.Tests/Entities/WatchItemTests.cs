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
    public class WatchItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateWatchItem_WhenValidDataIsProvided()
        {
            // Arrange
            int validMovieId = 1;
            int? validSeriesGroupId = null;
            int validWatchlistId = 2;

            // Act
            var watchItem = new WatchItem(validMovieId, validSeriesGroupId, validWatchlistId);

            // Assert
            Assert.Equal(validMovieId, watchItem.MovieId);
            Assert.Null(watchItem.SeriesGroupId);
            Assert.Equal(validWatchlistId, watchItem.WatchlistId);
        }

        [Fact]
        public void Constructor_ShouldThrowWatchItemInvalidTargetException_WhenMovieIdAndSeriesGroupIdAreBothNull()
        {
            // Arrange
            int? invalidMovieId = null;
            int? invalidSeriesGroupId = null;
            int validWatchlistId = 2;

            // Act
            Action act = () => new WatchItem(invalidMovieId, invalidSeriesGroupId, validWatchlistId);

            // Assert
            act.Should().Throw<WatchItemInvalidTargetException>()
                .WithMessage("A watch item must have either a MovieId or a SeriesGroupId.");
        }

        [Fact]
        public void Constructor_ShouldNotThrowException_WhenMovieIdIsNullAndSeriesGroupIdIsNotNull()
        {
            // Arrange
            int? validMovieId = null;
            int validSeriesGroupId = 1;
            int validWatchlistId = 2;

            // Act
            var watchItem = new WatchItem(validMovieId, validSeriesGroupId, validWatchlistId);

            // Assert
            Assert.Null(watchItem.MovieId);
            Assert.Equal(validSeriesGroupId, watchItem.SeriesGroupId);
            Assert.Equal(validWatchlistId, watchItem.WatchlistId);
        }

        [Fact]
        public void Constructor_ShouldNotThrowException_WhenMovieIdIsNotNullAndSeriesGroupIdIsNull()
        {
            // Arrange
            int validMovieId = 1;
            int? validSeriesGroupId = null;
            int validWatchlistId = 2;

            // Act
            var watchItem = new WatchItem(validMovieId, validSeriesGroupId, validWatchlistId);

            // Assert
            Assert.Equal(validMovieId, watchItem.MovieId);
            Assert.Null(watchItem.SeriesGroupId);
            Assert.Equal(validWatchlistId, watchItem.WatchlistId);
        }
    }
}
