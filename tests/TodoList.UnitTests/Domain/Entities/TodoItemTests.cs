using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

using FluentAssertions;
using TodoList.Domain;
using TodoList.Domain.Enums;

namespace TodoList.UnitTests.Domain.Entities
{
    public class TodoItemTests
    {
        #region valid-object-creation tests

        [Fact]
        public void CreateObject_ShouldCreateTodoItem_WithValidData()
        {
            //Arrange
            string title = "Write backend";
            string description = "Finish API implementation";

            //Act
            var todoItem = new TodoItem(title, description);

            //Assert
            todoItem.Id.Should().NotBe(Guid.Empty);
            todoItem.Title.Should().Be(title);
            todoItem.Description.Should().Be(description);
            todoItem.Status.Should().Be(TodoStatus.Pending);
            todoItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(0.5));
        }

        [Fact]
        public void CreateObject_ShouldCreateTodoItem_WithOptionalDescription()
        {
            // Arrange
            string title = "Write backend";

            // Act
            var todoItem = new TodoItem(title);

            // Assert
            todoItem.Id.Should().NotBe(Guid.Empty);
            todoItem.Title.Should().Be(title);
            todoItem.Description.Should().BeNullOrEmpty();
            todoItem.Status.Should().Be(TodoStatus.Pending);
            todoItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(0.5));
        }

        [Fact]
        public void CreateObject_ShouldTrimTitle_WhenTitleHasLeadingOrTrailingSpaces()
        {
            // Arrange
            string titleWithSpaces = "  Trim this title  ";
            string expectedTitle = "Trim this title";

            // Act
            var todoItem = new TodoItem(titleWithSpaces);

            // Assert
            todoItem.Title.Should().Be(expectedTitle);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void CreateObject_ShouldThrowArgumentException_WhenTitleIsWhitespaceOnly(string title)
        {
            // Arrange
            // Act
            Action act = () => new TodoItem(title);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Title cannot be null or empty.*");
        }

        [Fact]
        public void CreateObject_ShouldTrimDescription_WhenDescriptionHasLeadingOrTrailingSpaces()
        {
            // Arrange
            string title = "Task";
            string descriptionWithSpaces = "  some details here   ";
            string expectedDescription = "some details here";

            // Act
            var todoItem = new TodoItem(title, descriptionWithSpaces);

            // Assert
            todoItem.Description.Should().Be(expectedDescription);
        }

        [Fact]
        public void CreateObject_ShouldSetEmptyDescription_WhenDescriptionIsNull()
        {
            // Arrange
            string title = "Task";

            // Act
            var todoItem = new TodoItem(title, null);

            // Assert
            todoItem.Description.Should().BeEmpty();
        }

        #endregion

        #region invalid-object-creation tests

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateObject_ShouldThrowArgumentException_WhenTitleIsInvalid(string invalidTitle)
        {
            Action act = () => new TodoItem(invalidTitle);
            act.Should().Throw<ArgumentException>();
        }

        #endregion
    }
}
