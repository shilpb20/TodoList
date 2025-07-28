using TodoList.Application.DTOs;
using TodoList.TestDataBuilder;

namespace TodoList.Application.Services
{
    public interface ITodoItemService
    {
        Task<TodoItemDto> AddTodoItem(TodoItemCreateDto createDto);
    }
}
