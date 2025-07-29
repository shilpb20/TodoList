
using AutoMapper;
using TodoList.Application.DTOs;
using TodoList.Application.Services;
using TodoList.Domain.Entities;
using TodoList.TestDataBuilder;

namespace TodoList.Infrastructure.Services
{
    public class TodoItemService : ITodoItemService
    {
        #region fields and properties

        private readonly IMapper _mapper;

        private readonly List<TodoItem> _items = new();
        private int _nextId = 1;

        #endregion

        #region constructors and initialisors

        public TodoItemService(IMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        #region add-item

        public async Task<TodoItemDto> AddItem(TodoItemCreateDto createDto)
        {
            var todoItem = new TodoItem(_nextId++, createDto.Title, createDto.Description);

            _items.Add(todoItem);

            var dto = _mapper.Map<TodoItemDto>(todoItem);
            return await Task.FromResult(dto);
        }

        #endregion

        #region get-all items

        public Task<IEnumerable<TodoItemDto>> GetAllItems()
        {
            var dtos = _mapper.Map<IEnumerable<TodoItemDto>>(_items);
            return Task.FromResult(dtos);
        }

        #endregion

        #region delete-item

        public Task<TodoItemDto?> DeleteItem(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item is null)
                return Task.FromResult<TodoItemDto?>(null);

            _items.Remove(item);
            var deletedDto = _mapper.Map<TodoItemDto>(item);

            return Task.FromResult<TodoItemDto?>(deletedDto);
        }

        #endregion
    }
}