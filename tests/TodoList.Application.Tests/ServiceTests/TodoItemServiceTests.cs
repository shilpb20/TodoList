using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using TodoList.Application.Services;
using TodoList.Infrastructure.Mapper;
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
            var loggerFactory = LoggerFactory.Create(builder => { });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TodoItemProfile>();
            }, loggerFactory);

            var mapper = mapperConfig.CreateMapper();

            _service = new TodoItemService(mapper);
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
            newItem.Should().NotBeNull();
            newItem.Title.Should().Be(createDto.Title);
            newItem.Description.Should().Be(createDto.Description);
            newItem.Status.Should().Be("Pending");
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
            newItem1.Should().NotBeNull();
            newItem2.Should().NotBeNull();
            newItem2.Id.Should().NotBe(newItem1.Id);

            newItem1.Title.Should().Be(createDto1.Title);
            newItem1.Status.Should().Be("Pending");

            newItem2.Title.Should().Be(createDto2.Title);
            newItem2.Status.Should().Be("Pending");
        }

        
        [Fact]
        public async Task AddItem_ShouldAllowDuplicateTitles()
        {
            // Arrange
            var title = "Duplicate Task";
            var createDto1 = _createDtoBuilder.WithTitle(title).Build();
            var createDto2 = _createDtoBuilder.WithTitle(title).Build();

            // Act
            var newItem1 = await _service.AddItem(createDto1);
            var newItem2 = await _service.AddItem(createDto2);

            // Assert
            newItem1.Should().NotBeNull();
            newItem2.Should().NotBeNull();
            newItem1.Title.Should().Be(title);
            newItem2.Title.Should().Be(title);
            newItem1.Id.Should().NotBe(newItem2.Id);
        }

        #endregion

        #region get-all-items tests

        [Fact]
        public async Task GetAll_ShouldReturnEmpty_WhenNoItemsAdded()
        {
            // Act
            var allItems = await _service.GetAllItems();

            // Assert
            allItems.Should().NotBeNull();
            allItems.Should().BeEmpty();
        }

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
            allItems.Should().NotBeNull();

            var itemsList = allItems.ToList();
            itemsList.Count.Should().Be(2);
            itemsList[1].Id.Should().NotBe(itemsList[0].Id);

            itemsList.Should().Contain(i => i.Title == "Task 1");
            itemsList.Should().Contain(i => i.Title == "Task 2");

        }

        #endregion

        #region delete-item tests

        [Fact]
        public async Task DeleteTodoItem_ShouldRemoveItem_WhenIdIsValid()
        {
            // Arrange
            var createDto1 = _createDtoBuilder.WithTitle("Item 1").Build();
            var createDto2 = _createDtoBuilder.WithTitle("Item 2").Build();

            var item1 = await _service.AddItem(createDto1);
            var item2 = await _service.AddItem(createDto2);

            // Act
            var deletedItem = await _service.DeleteItem(item1.Id);
            var remainingItems = await _service.GetAllItems();

            // Assert
            deletedItem.Should().BeEquivalentTo(item1);
            remainingItems.Should().NotContain(i => i.Id == item1.Id);
            remainingItems.Should().ContainSingle(i => i.Id == item2.Id);
        }

        [Fact]
        public async Task DeleteItem_ShouldReturnNull_WhenItemNotFound()
        {
            // Act
            var deletedItem = await _service.DeleteItem(999);

            // Assert
            deletedItem.Should().BeNull();
        }

        [Fact]
        public async Task DeleteItem_ShouldReturnNull_WhenDeletingSameItemTwice()
        {
            // Arrange
            var createDto = _createDtoBuilder.WithTitle("Item").Build();
            var addedItem = await _service.AddItem(createDto);

            // Act
            var firstDelete = await _service.DeleteItem(addedItem.Id);
            var secondDelete = await _service.DeleteItem(addedItem.Id);

            // Assert
            firstDelete.Should().NotBeNull();
            secondDelete.Should().BeNull();
        }


        #endregion
    }
}
