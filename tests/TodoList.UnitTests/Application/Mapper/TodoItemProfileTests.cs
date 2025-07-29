using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;
using TodoList.Infrastructure.Mapper;

namespace TodoList.UnitTests.Application.Mapper
{
    public class TodoItemProfileTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public TodoItemProfileTests()
        {
            using var loggerFactory = LoggerFactory.Create(builder => { });
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TodoItemProfile>();
            }, loggerFactory);

            _mapper = _config.CreateMapper();
        }

        [Fact]
        public void AutoMapperConfiguration_ShouldBeValid()
        {
            //Arrange
            //Act
            //Assert
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void EntityToDtoMapping_ShouldSucceed()
        {
            //Arrange
            var todoItem = new TodoItem("Test title", "Test description");

            //Act
            var dto = _mapper.Map<TodoItemDto>(todoItem);

            //Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(todoItem.Id);
            dto.Title.Should().Be(todoItem.Title);
            dto.Description.Should().Be(todoItem.Description);
            dto.Status.Should().Be(todoItem.Status.ToString());
            dto.CreatedAt.Should().Be(todoItem.CreatedAt);
        }

        [Fact]
        public void DtoToEntityMapping_ShouldSucceed()
        {
            //Arrange
            var dto = new TodoItemDto
            {
                Id = Guid.NewGuid(),
                Title = "Test DTO",
                Description = "DTO description",
                Status = "Completed",
                CreatedAt = DateTime.UtcNow
            };

            //Act
            var todoItem = _mapper.Map<TodoItem>(dto);

            //Assert
            todoItem.Should().NotBeNull();
            todoItem.Id.Should().Be(dto.Id);
            todoItem.Title.Should().Be(dto.Title);
            todoItem.Description.Should().Be(dto.Description);
            todoItem.Status.Should().Be(TodoStatus.Pending);
            todoItem.CreatedAt.Should().Be(dto.CreatedAt);
        }

        [Fact]
        public void DtoToEntityMapping_ShouldThrowArgumentException_WhenStatusIsInvalid()
        {
            // Arrange
            var dto = new TodoItemDto
            {
                Status = "InvalidStatus"
            };

            // Act
            Action act = () => _mapper.Map<TodoItem>(dto);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CreateDtoToEntityMapping_ShouldSucceed()
        {
            // Arrange
            var createDto = new TodoItemCreateDto
            {
                Title = "New Task",
                Description = "Some details"
            };

            // Act
            var entity = _mapper.Map<TodoItem>(createDto);

            // Assert
            entity.Should().NotBeNull();
            entity.Title.Should().Be(createDto.Title);
            entity.Description.Should().Be(createDto.Description);
            entity.Id.Should().NotBeEmpty();
            entity.Status.Should().Be(TodoList.Domain.Enums.TodoStatus.Pending);
        }
    }
}
