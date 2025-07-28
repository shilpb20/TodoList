using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

using FluentAssertions;

namespace TodoList.Domain.Tests
{
    public class TodoItemTests
    {
        #region valid-object-creation tests

        [Fact]
        public void CreateObject_ShouldCreateTodoItem_WithValidData()
        {
            //Arrange
            int id = 1;
            string title = "Write backend";
            string description = "Finish API implementation";

            //Act
            var todoItem = new TodoItem(id, title, description);

            //Assert
            todoItem.Id.Should().Be(id);
            todoItem.Title.Should().Be(title);
            todoItem.Description.Should().Be(description);
            todoItem.Status.Should().Be(TodoStatus.Pending);
            todoItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(0.5));
        }

        [Fact]
        public void CreateObject_ShouldCreateTodoItem_WithOptionalDescription()
        {
            // Arrange
            int id = 1;
            string title = "Write backend";

            // Act
            var todoItem = new TodoItem(id, title);

            // Assert
            todoItem.Id.Should().Be(id);
            todoItem.Title.Should().Be(title);
            todoItem.Description.Should().BeNullOrEmpty();
            todoItem.Status.Should().Be(TodoStatus.Pending);
            todoItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(0.5));
        }

        #endregion

        #region invalid-object-creation tests

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateObject_ShouldThrowArgumentException_WhenTitleIsInvalid(string invalidTitle)
        {
            Action act = () => new TodoItem(1, invalidTitle);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CreateObject_ShouldThrowArgumentOutOfRangeException_WhenIdIsInvalid()
        {
            Action act = () => new TodoItem(0, "Valid title");
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void CreateObject_ShouldThrowArgumentOutOfRangeException_WhenIdIsNegative(int invalidId)
        {
            Action act = () => new TodoItem(invalidId, "Valid title");
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        #endregion
    }
}
