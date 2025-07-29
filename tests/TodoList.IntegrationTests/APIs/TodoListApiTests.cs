using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TodoList.Application.DTOs;
using TodoList.Domain.Enums;
using TodoList.TestDataBuilder.DTOs;

namespace TodoList.IntegrationTests.APIs
{
    public class TodoListApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        #region fields and properties

        private readonly HttpClient _httpClient;
        private readonly string _todoListApiUrl = "api/todo-list";

        private readonly TodoItemCreateDtoBuilder _createDtoBuilder = new();

        #endregion

        #region constructors and initialisors

        public TodoListApiTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        #endregion

        #region get-items tests

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

        #region add-items tests

        [Fact]
        public async Task AddTodoItem_ReturnsCreatedResult_WithCreatedItem()
        {
            //Arrange
            var addItem = _createDtoBuilder.Build();

            //Act
            var response = await _httpClient.PostAsJsonAsync(_todoListApiUrl, addItem);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdItem = await response.Content.ReadFromJsonAsync<TodoItemDto>();
            createdItem.Should().NotBeNull();

            createdItem.Id.Should().NotBe(Guid.Empty);
            createdItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(0.5));
            createdItem.Status.Should().Be(TodoStatus.Pending.ToString());

            createdItem.Title.Should().Be(addItem.Title);
            createdItem.Description.Should().Be(addItem.Description);
        }

        [Fact]
        public async Task AddTodoItem_SuccessfullyAddsItem_WhenTitleAndDescriptionAreMaxLength()
        {
            //Arrange
            var addItem = _createDtoBuilder.WithMaxTitleLength().WithMaxDescriptionLength().Build();

            //Act
            var response = await _httpClient.PostAsJsonAsync(_todoListApiUrl, addItem);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdItem = await response.Content.ReadFromJsonAsync<TodoItemDto>();
            createdItem.Should().NotBeNull();

            createdItem.Id.Should().NotBe(Guid.Empty);
            createdItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(0.5));
            createdItem.Status.Should().Be(TodoStatus.Pending.ToString());

            createdItem.Title.Should().Be(addItem.Title);
            createdItem.Description.Should().Be(addItem.Description);
        }


        [Fact]
        public async Task AddTodoItem_ReturnsBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var content = new StringContent("null", System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_todoListApiUrl, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddTodoItem_ReturnsBadRequest_WhenTitleIsTooShort()
        {
            // Arrange
            var invalidItem = _createDtoBuilder.WithShortTitle().Build();

            // Act
            var response = await _httpClient.PostAsJsonAsync(_todoListApiUrl, invalidItem);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddTodoItem_ReturnsBadRequest_WhenTitleIsTooLong()
        {
            // Arrange
            var invalidItem = _createDtoBuilder.WithLongTitle().Build();

            // Act
            var response = await _httpClient.PostAsJsonAsync(_todoListApiUrl, invalidItem);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddTodoItem_ReturnsBadRequest_WhenDescriptionIsTooLong()
        {
            // Arrange
            var invalidItem = _createDtoBuilder.WithLongDescription().Build();

            // Act
            var response = await _httpClient.PostAsJsonAsync(_todoListApiUrl, invalidItem);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion
    }
}
