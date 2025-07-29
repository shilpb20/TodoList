using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TodoList.Application.DTOs;
using TodoList.Application.IRepositories;
using TodoList.Application.Services;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Mapper;
using TodoList.Infrastructure.Services;
using TodoList.TestDataBuilder;

namespace TodoList.UnitTests.Application.Services
{
    public class TodoItemServiceTests
    {
        #region fields and properties

        private readonly ITodoItemService _service;
        private readonly TodoItemCreateDtoBuilder _createDtoBuilder = new();

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ITodoItemRepository> _mockRepo;

        #endregion

        #region constructors and initialisors

        public TodoItemServiceTests()
        {
            var loggerFactory = LoggerFactory.Create(builder => { });

            _mockMapper = new Mock<IMapper>();
            _mockRepo = new Mock<ITodoItemRepository>();

            _service = new TodoItemService(_mockMapper.Object, _mockRepo.Object);

            SetupMapperBehaviors();
            SetupRepositoryBehaviors();
        }

        private void SetupRepositoryBehaviors()
        {
            _mockRepo
               .Setup(repo => repo.AddAsync(It.IsAny<TodoItem>()))
               .ReturnsAsync((TodoItem todo) => todo);
        }

        private void SetupMapperBehaviors()
        {
            _mockMapper
               .Setup(m => m.Map<TodoItem>(It.IsAny<TodoItemCreateDto>()))
               .Returns((TodoItemCreateDto dto) => new TodoItem(dto.Title, dto.Description));

            _mockMapper
                .Setup(m => m.Map<TodoItemDto>(It.IsAny<TodoItem>()))
                .Returns((TodoItem item) => new TodoItemDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Status = item.Status.ToString()
                });
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

            _mockMapper.Verify(m => m.Map<TodoItem>(createDto), Times.Once);
            _mockMapper.Verify(m => m.Map<TodoItemDto>(It.IsAny<TodoItem>()), Times.Once);
            _mockRepo.Verify(repo => repo.AddAsync(It.Is<TodoItem>(x => x.Title == createDto.Title)), Times.Once);
        }

        [Fact]
        public async Task AddTodoItem_ShouldAddMultipleItems_WithUniqueIds()
        {
            // Arrange
            var createDto1 = _createDtoBuilder.WithTitle("Task 1").Build();
            var createDto2 = _createDtoBuilder.WithTitle("Task 2").Build();

            // Act
            var firstItem = await _service.AddItem(createDto1);
            var secondItem = await _service.AddItem(createDto2);

            // Assert
            firstItem.Should().NotBeNull();
            secondItem.Should().NotBeNull();
            secondItem.Id.Should().NotBe(firstItem.Id);
            firstItem.Id.Should().NotBeEmpty();

            firstItem.Title.Should().Be(createDto1.Title);
            firstItem.Status.Should().Be("Pending");

            secondItem.Title.Should().Be(createDto2.Title);
            secondItem.Status.Should().Be("Pending");

            _mockMapper.Verify(m => m.Map<TodoItem>(createDto1), Times.Once);
            _mockMapper.Verify(m => m.Map<TodoItem>(createDto2), Times.Once);
            _mockMapper.Verify(m => m.Map<TodoItemDto>(It.IsAny<TodoItem>()), Times.Exactly(2));

            _mockRepo.Verify(repo => repo.AddAsync(It.Is<TodoItem>(x => x.Title == "Task 1")), Times.Once);
            _mockRepo.Verify(repo => repo.AddAsync(It.Is<TodoItem>(x => x.Title == "Task 2")), Times.Once);
        }


        [Fact]
        public async Task AddItem_ShouldAllowDuplicateTitles()
        {
            // Arrange
            var title = "Duplicate Task";
            var createDto1 = _createDtoBuilder.WithTitle(title).Build();
            var createDto2 = _createDtoBuilder.WithTitle(title).Build();

            // Act
            var firstItem = await _service.AddItem(createDto1);
            var secondItem = await _service.AddItem(createDto2);

            // Assert
            firstItem.Should().NotBeNull();
            secondItem.Should().NotBeNull();
            firstItem.Title.Should().Be(title);
            secondItem.Title.Should().Be(title);
            firstItem.Id.Should().NotBe(secondItem.Id);

            _mockRepo.Verify(repo => repo.AddAsync(It.Is<TodoItem>(x => x.Title == "Duplicate Task")), Times.Exactly(2));
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
            _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<TodoItem>()), Times.Once);

            deletedItem.Should().BeEquivalentTo(item1);
            remainingItems.Should().NotContain(i => i.Id == item1.Id);
            remainingItems.Should().ContainSingle(i => i.Id == item2.Id);
        }

        [Fact]
        public async Task DeleteItem_ShouldReturnNull_WhenItemNotFound()
        {
            // Act
            var deletedItem = await _service.DeleteItem(Guid.NewGuid());

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
