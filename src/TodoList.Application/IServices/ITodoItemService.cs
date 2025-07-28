using TodoList.Application.DTOs;
using TodoList.TestDataBuilder;

namespace TodoList.Application.Services
{
    public interface ITodoItemService
    {
        Task<TodoItemDto> AddItem(TodoItemCreateDto createDto);
        Task<TodoItemDto?> DeleteItem(int id);
        Task<IEnumerable<TodoItemDto>> GetAllItems();
    }
}
