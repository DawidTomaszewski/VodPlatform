using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Exceptions.ValueObjects;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Tests.ValueObjects
{
    public class DurationTests
    {
        [Fact]
        public void Constructor_ShouldCreateDuration_WhenTotalSecondsIsPositive()
        {
            // Arrange
            var totalSeconds = 3661; // 1h 1m 1s

            // Act
            var duration = new Duration(totalSeconds);

            // Assert
            duration.TotalSeconds.Should().Be(3661);
            duration.Hours.Should().Be(1);
            duration.Minutes.Should().Be(1);
            duration.Seconds.Should().Be(1);
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidDurationException_WhenTotalSecondsIsNegative()
        {
            // Arrange
            var totalSeconds = -10;

            // Act
            Action act = () => new Duration(totalSeconds);

            // Assert
            act.Should().Throw<InvalidDurationException>()
               .WithMessage($"Duration value '{totalSeconds}' is invalid. It must be greater than or equal to 0.");
        }
    }
}
