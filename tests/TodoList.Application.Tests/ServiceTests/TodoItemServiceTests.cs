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

        #endregion
    }
}
