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
    public class TitleTests
    {
        [Fact]
        public void Constructor_ShouldCreateTitle_WhenValidStringIsProvided()
        {
            // Arrange
            var validTitle = "Inception";

            // Act
            var title = new Title(validTitle);

            // Assert
            title.TitleObject.Should().Be(validTitle);
            title.ToString().Should().Be(validTitle);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowInvalidTitleException_WhenTitleIsNullOrWhitespace(string invalidTitle)
        {
            // Act
            Action act = () => new Title(invalidTitle);

            // Assert
            act.Should().Throw<InvalidTitleException>()
               .WithMessage($"Title '{invalidTitle}' is invalid. It cannot be null, empty, or whitespace.");
        }
    }
}
