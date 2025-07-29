using TodoList.Application.DTOs;

namespace TodoList.Application.IServices
{
    public interface ITodoItemService
    {
        Task<TodoItemDto> AddItemAsync(TodoItemCreateDto createDto);
        Task<TodoItemDto?> DeleteItem(Guid id);
        Task<IEnumerable<TodoItemDto>> GetAllItemsAsync();
    }
}
