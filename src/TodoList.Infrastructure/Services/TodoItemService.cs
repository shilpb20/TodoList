
using AutoMapper;
using TodoList.Application.DTOs;
using TodoList.Application.IRepositories;
using TodoList.Application.IServices;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Services
{
    public class TodoItemService : ITodoItemService
    {
        #region fields and properties

        private readonly IMapper _mapper;
        private readonly ITodoItemRepository _repository;

        #endregion

        #region constructors and initialisors

        public TodoItemService(IMapper mapper, ITodoItemRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region add-item

        public async Task<TodoItemDto> AddItemAsync(TodoItemCreateDto createDto)
        {
            var todoItem = _mapper.Map<TodoItem>(createDto);
            await _repository.AddAsync(todoItem);
            return  _mapper.Map<TodoItemDto>(todoItem); 
        }

        #endregion

        #region get-all items

        public async Task<IEnumerable<TodoItemDto>> GetAllItemsAsync()
        {
            var todoItems = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);
            return dtos;
        }

        #endregion

        #region delete-item

        public async Task<TodoItemDto?> DeleteItemAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result == null)
            {
                return null;
            }

            var deletedDto = _mapper.Map<TodoItemDto>(result);
            return deletedDto;
        }

        #endregion
    }
}