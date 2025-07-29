using TodoList.Domain.Entities;

namespace TodoList.Application.IRepositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> AddAsync(TodoItem todoItem);
        Task<TodoItem?> DeleteAsync(Guid id);
        Task<IEnumerable<TodoItem>> GetAllAsync();

    }
}