using Microsoft.AspNetCore.Mvc;
using TodoList.Application.DTOs;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/todo-list")]
    public class TodoListController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoItemDto>>> GetTodoItems()
        {
            return Ok(new List<TodoItemDto>());
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoItemDto>> AddTodoItem(TodoItemCreateDto createDto)
        {
            var result = new TodoItemDto()
            {
                Id = Guid.NewGuid(),
                Title = "Default title",
                Description = "Default description",
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
            };

            return Created("", result);
        }
    }
}
