using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using TodoList.Application.DTOs;


namespace TodoList.Api.Tests.Controllers
{
    public class TodoListControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public TodoListControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        #region get-all tests

        [Fact]
        public async Task GetAll_ReturnsOkWithEmptyList_WhenNoItemsExist()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync("/api/todo-list");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var todoList = await response.Content.ReadFromJsonAsync<List<TodoItemDto>>();
            todoList.Should().BeEmpty();
        }

        #endregion
    }
}
