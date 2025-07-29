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

        public Task<TodoItem?> DeleteAsync(Guid id)
        {
            var matchingItem = _items.FirstOrDefault(item => item.Id == id);
            if(matchingItem != null)
            {
                _items.Remove(matchingItem);
                return Task.FromResult<TodoItem?>(matchingItem);
            }

            return Task.FromResult<TodoItem?>(null);
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TodoItem>>(_items.ToList());
        }
    }
}