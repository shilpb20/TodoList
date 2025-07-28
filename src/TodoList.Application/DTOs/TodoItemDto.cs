namespace TodoList.Application.DTOs
{
    public class TodoItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }
}