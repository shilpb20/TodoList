using Microsoft.AspNetCore.Mvc;
using TodoList.Application.DTOs;
using TodoList.Application.IServices;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/todo-list")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoListController(ITodoItemService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TodoItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoItemDto>>> GetTodoItems()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoItemDto>> AddTodoItem(TodoItemCreateDto createDto)
        {
            if(createDto == null)
            {
                return BadRequest("Todo item cannot be null.");
            }

            var result = await _service.AddItemAsync(createDto);

            //TODO:Update to CreatedAtAction after implementing location header
            return Ok(result);
        }
    }
}
