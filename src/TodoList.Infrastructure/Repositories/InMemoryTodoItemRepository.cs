using TodoList.Application.IRepositories;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Repositories
{
    public class InMemoryTodoItemRepository : ITodoItemRepository
    {
        private readonly List<TodoItem> _items = new();

        public InMemoryTodoItemRepository()
        {
            _items = new List<TodoItem>
            {
                new TodoItem("Buy groceries", "Almond milk, vegetable and nut butter"),
                new TodoItem("Book car service", "Service due next week, call the dealership"),
                new TodoItem("Complete assignment", "Finish the backend and Angular UI"),
                new TodoItem("Walk the dog"),
                new TodoItem("Read a book", "Read at least 30 pages of 'Clean Architecture'"),
                new TodoItem("Clean the house"),
                new TodoItem("Pay electricity bill", "Due date is this Saturday"),
                new TodoItem("Reply to recruiter"),
                new TodoItem("Workout", "Cardio + core exercises, 30 minutes"),
                new TodoItem("Plan weekend trip")
            };
        }
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

        public void Clear()
        {
            _items.Clear();
        }
    }
}
