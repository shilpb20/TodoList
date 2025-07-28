
using TodoList.Application.DTOs;
using TodoList.Application.Services;
using TodoList.TestDataBuilder;

namespace TodoList.Infrastructure.Services
{
    public class TodoItemService : ITodoItemService
    {
        public async Task<TodoItemDto> AddTodoItem(TodoItemCreateDto createDto)
        {
            return new TodoItemDto()
            {
                Id = 1,
                Title = "Default title",
                Description = "Default description",
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}