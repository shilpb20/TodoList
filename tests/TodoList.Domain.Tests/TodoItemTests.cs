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
    }
}
