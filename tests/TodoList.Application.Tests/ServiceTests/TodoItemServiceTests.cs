using TodoList.Application.Services;
using TodoList.Infrastructure.Services;
using TodoList.TestDataBuilder;

namespace TodoList.Application.Tests.Services
{
    public class TodoItemServiceTests
    {
        #region fields and properties

        private ITodoItemService _service;

        private readonly TodoItemCreateDtoBuilder _createDtoBuilder = new TodoItemCreateDtoBuilder();
        private readonly TodoItemDtoBuilder _dtoBuilder = new TodoItemDtoBuilder();

        #endregion

        #region constructors and initialisors

        public TodoItemServiceTests()
        {
            _service = new TodoItemService();
        }

        #endregion

        #region add tests

        [Fact]
        public async Task AddTodoItem_ShouldAddItem_WhenValid()
        {
            //Arrange
            var createDto = _createDtoBuilder.Build();

            //Act
            var newItem = await _service.AddTodoItem(createDto);

            //Assert
            Assert.NotNull(newItem);
            Assert.NotNull(newItem);
            Assert.Equal(createDto.Title, newItem.Title);
            Assert.Equal(createDto.Description, newItem.Description);
            Assert.Equal("Pending", newItem.Status);
        }

        [Fact]
        public async Task AddTodoItem_ShouldAddMultipleItems_WithUniqueIds()
        {
            // Arrange
            var createDto1 = _createDtoBuilder.WithTitle("Task 1").Build();
            var createDto2 = _createDtoBuilder.WithTitle("Task 2").Build();

            // Act
            var newItem1 = await _service.AddTodoItem(createDto1);
            var newItem2 = await _service.AddTodoItem(createDto2);

            // Assert
            Assert.NotNull(newItem1);
            Assert.NotNull(newItem2);

            Assert.NotEqual(newItem1.Id, newItem2.Id);
            Assert.Equal(createDto1.Title, newItem1.Title);
            Assert.Equal(createDto2.Title, newItem2.Title);
            Assert.Equal("Pending", newItem1.Status);
            Assert.Equal("Pending", newItem2.Status);
        }


        #endregion
    }
}
