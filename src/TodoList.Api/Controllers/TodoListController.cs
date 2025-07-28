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
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(new List<TodoItemDto>());
        }
    }
}
