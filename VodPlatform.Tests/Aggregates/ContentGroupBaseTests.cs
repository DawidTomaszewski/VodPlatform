using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Enums;
using VodPlatform.Core.Domain.Exceptions.Aggregates;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Tests.Aggregates
{
    public class ContentGroupBaseTests
    {
        private class TestContentGroup : ContentGroupBase
        {
            public TestContentGroup(string title, IEnumerable<Category> categories)
                : base(title, categories)
            {
            }

            public override Duration GetTotalDuration()
            {
                return Duration.FromTotalSeconds(120);
            }
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidTitleException_WhenTitleIsNullOrWhiteSpace()
        {
            // Arrange
            string invalidTitle = string.Empty;
            var categories = new List<Category> { Category.Action, Category.Drama };

            // Act
            Action act = () => new TestContentGroup(invalidTitle, categories);

            // Assert
            act.Should().Throw<InvalidTitleException>()
                .WithMessage("Title cannot be null or empty.");
        }

        [Fact]
        public void Constructor_ShouldInitializeContentGroup_WhenValidDataIsProvided()
        {
            // Arrange
            string validTitle = "Test Title";
            var categories = new List<Category> { Category.Action, Category.Drama };

            // Act
            var contentGroup = new TestContentGroup(validTitle, categories);

            // Assert
            Assert.Equal(validTitle, contentGroup.TitleObject);
            Assert.Equal(2, contentGroup.GetCategories().Count);
        }

        [Fact]
        public void AddCategory_ShouldThrowCategoryAlreadyExistsException_WhenCategoryAlreadyExists()
        {
            // Arrange
            string validTitle = "Test Title";
            var categories = new List<Category> { Category.Action, Category.Drama };
            var contentGroup = new TestContentGroup(validTitle, categories);

            // Act
            Action act = () => contentGroup.AddCategory(Category.Action);

            // Assert
            act.Should().Throw<CategoryAlreadyExistsException>()
                .WithMessage($"Category '{Category.Action}' already exists.");
        }

        [Fact]
        public void AddCategory_ShouldAddCategory_WhenValidCategoryIsProvided()
        {
            // Arrange
            string validTitle = "Test Title";
            var categories = new List<Category> { Category.Action, Category.Drama };
            var contentGroup = new TestContentGroup(validTitle, categories);

            // Act
            contentGroup.AddCategory(Category.Comedy);

            // Assert
            var updatedCategories = contentGroup.GetCategories();
            Assert.Contains(Category.Comedy, updatedCategories);
        }

        [Fact]
        public void RemoveCategory_ShouldThrowCategoryNotFoundException_WhenCategoryDoesNotExist()
        {
            // Arrange
            string validTitle = "Test Title";
            var categories = new List<Category> { Category.Action, Category.Drama };
            var contentGroup = new TestContentGroup(validTitle, categories);

            // Act
            Action act = () => contentGroup.RemoveCategory(Category.Comedy);

            // Assert
            act.Should().Throw<CategoryNotFoundException>()
                .WithMessage($"Category '{Category.Comedy}' not found.");
        }

        [Fact]
        public void RemoveCategory_ShouldRemoveCategory_WhenValidCategoryIsProvided()
        {
            // Arrange
            string validTitle = "Test Title";
            var categories = new List<Category> { Category.Action, Category.Drama };
            var contentGroup = new TestContentGroup(validTitle, categories);

            // Act
            contentGroup.RemoveCategory(Category.Action);

            // Assert
            var updatedCategories = contentGroup.GetCategories();
            Assert.DoesNotContain(Category.Action, updatedCategories);
        }

        [Fact]
        public void GetCategories_ShouldThrowInvalidCategoryException_WhenInvalidCategoryStringIsProvided()
        {
            // Arrange
            string invalidCategories = "Action,InvalidCategory";
            var contentGroup = new TestContentGroup("Test Title", new List<Category>());

            // Act
            Action act = () => contentGroup.GetCategories();

            // Assert
            act.Should().Throw<InvalidCategoryException>()
                .WithMessage($"'' is not a valid category.");
        }
    }
}
