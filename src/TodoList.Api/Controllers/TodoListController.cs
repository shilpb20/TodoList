using Microsoft.AspNetCore.Mvc;
using TodoList.Application.DTOs;
using TodoList.TestDataBuilder;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/todo-list")]
    public class TodoListController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(new List<TodoItemDto>());
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddTodoItem(TodoItemCreateDto createDto)
        {
            var result = new TodoItemDto()
            {
                Id = 1,
                Title = "Default title",
                Description = "Default description",
                DueAt = DateTime.Today.AddDays(1),
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
            };

            return Created("", result);
        }
    }
}
