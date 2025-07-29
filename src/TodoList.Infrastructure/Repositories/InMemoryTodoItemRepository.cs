using TodoList.Application.IRepositories;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Repositories
{
    public class InMemoryTodoItemRepository : ITodoItemRepository
    {
        private readonly List<TodoItem> _items = new();

        public async Task<TodoItem> AddAsync(TodoItem todoItem)
        {
            _items.Add(todoItem);
            return await Task.FromResult(todoItem);
        }
    }
}