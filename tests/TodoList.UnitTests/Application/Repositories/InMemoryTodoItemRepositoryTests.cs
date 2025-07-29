using FluentAssertions;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Repositories;

namespace TodoList.UnitTests.Application.Repositories
{
    public class InMemoryTodoItemRepositoryTests
    {
        private readonly InMemoryTodoItemRepository _repo = new();

        [Fact]
        public async Task AddAsync_ShouldAddItem()
        {
            // Arrange
            var item = new TodoItem("Title");

            // Act
            var result = await _repo.AddAsync(item);
            var allItems = await _repo.GetAllAsync();

            // Assert
            result.Should().Be(item);
            allItems.Should().ContainSingle(i => i.Id == item.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveExistingItem()
        {
            // Arrange
            var item = new TodoItem("ToDelete");
            await _repo.AddAsync(item);

            // Act
            var deleted = await _repo.DeleteAsync(item.Id);
            var allItems = await _repo.GetAllAsync();

            // Assert
            deleted.Should().Be(item);
            allItems.Should().NotContain(i => i.Id == item.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNullForNonExistingItem()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var deleted = await _repo.DeleteAsync(nonExistingId);

            // Assert
            deleted.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllItems()
        {
            // Arrange
            var item1 = new TodoItem("Item 1");
            var item2 = new TodoItem("Item 2");
            await _repo.AddAsync(item1);
            await _repo.AddAsync(item2);

            // Act
            var items = await _repo.GetAllAsync();

            // Assert
            items.Should().HaveCount(2);
            items.Should().Contain(i => i.Id == item1.Id);
            items.Should().Contain(i => i.Id == item2.Id);
        }
    }
}
