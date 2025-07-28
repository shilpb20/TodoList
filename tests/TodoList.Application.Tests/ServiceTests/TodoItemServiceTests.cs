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

        #region add-item tests

        [Fact]
        public async Task AddTodoItem_ShouldAddItem_WhenValid()
        {
            //Arrange
            var createDto = _createDtoBuilder.Build();

            //Act
            var newItem = await _service.AddItem(createDto);

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
            var newItem1 = await _service.AddItem(createDto1);
            var newItem2 = await _service.AddItem(createDto2);

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

        #region get-all-items tests

        [Fact]
        public async Task GetAll_ShouldReturnAllAddedTodoItems()
        {
            // Arrange
            var createDto1 = _createDtoBuilder.WithTitle("Task 1").Build();
            var createDto2 = _createDtoBuilder.WithTitle("Task 2").Build();

            await _service.AddItem(createDto1);
            await _service.AddItem(createDto2);

            // Act
            var allItems = await _service.GetAllItems();

            // Assert
            Assert.NotNull(allItems);

            var itemsList = allItems.ToList();
            Assert.Equal(2, itemsList.Count);
            Assert.NotEqual(itemsList[0].Id, itemsList[1].Id);

            Assert.Contains(itemsList, item => item.Title == "Task 1");
            Assert.Contains(itemsList, item => item.Title == "Task 2");
        }

        #endregion
    }
}
