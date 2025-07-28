
using TodoList.Application.DTOs;
using TodoList.Application.Services;
using TodoList.Domain.Entities;
using TodoList.TestDataBuilder;

namespace TodoList.Infrastructure.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly List<TodoItem> _items = new();
        private int _nextId = 1;

        public async Task<TodoItemDto> AddItem(TodoItemCreateDto createDto)
        {
            var todoItem = new TodoItem(_nextId++, createDto.Title, createDto.Description);

            _items.Add(todoItem);

            var dto = new TodoItemDto
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Status = todoItem.Status.ToString(),
                CreatedAt = todoItem.CreatedAt
            };

            return await Task.FromResult(dto);
        }

        public Task<IEnumerable<TodoItemDto>> GetAllItems()
        {
            var dtos = _items.Select(todoItem => new TodoItemDto
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Status = todoItem.Status.ToString(),
                CreatedAt = todoItem.CreatedAt
            });

            return Task.FromResult(dtos);
        }
    }
}