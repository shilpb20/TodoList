namespace TodoList.Application.DTOs
{
    public class TodoItemCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;
    }
}