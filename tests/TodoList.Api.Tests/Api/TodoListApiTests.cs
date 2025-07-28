using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Json;
using TodoList.Application.DTOs;
using TodoList.Domain;
using TodoList.TestDataBuilder;

namespace TodoList.Api.Tests.Controllers
{
    public class TodoListApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly string _todoListApiUrl = "api/todo-list";

        private TodoItemCreateDtoBuilder _createDtoBuilder = new TodoItemCreateDtoBuilder();
        private TodoItemDtoBuilder _dtoBuilder = new TodoItemDtoBuilder();

        public TodoListApiTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        #region get-all tests

        [Fact]
        public async Task GetTodoItems_ReturnsOkWithEmptyList_WhenNoItemsExist()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync(_todoListApiUrl);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var todoList = await response.Content.ReadFromJsonAsync<List<TodoItemDto>>();
            todoList.Should().BeEmpty();
        }

        #endregion

        #region add tests

        [Fact]
        public async Task AddTodoItem_ReturnsCreatedResult_WithCreatedItem()
        {
            //Arrange
            var addItem = _createDtoBuilder.Build();

            //Act
            var response = await _httpClient.PostAsJsonAsync(_todoListApiUrl, addItem);

            //Assert
            response.Should().Be(HttpStatusCode.Created);

            var createdItem = await response.Content.ReadFromJsonAsync<TodoItemDto>();
            createdItem.Should().NotBeNull();

            createdItem.Id.Should().Be(1);
            createdItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(200));
            createdItem.Status.Should().Be(TodoStatus.Pending);

            createdItem.Title.Should().Be(addItem.Title);
            createdItem.Description.Should().Be(addItem.Description);
            createdItem.DueAt.Should().Be(addItem.DueAt);
        }

        #endregion
    }
}
