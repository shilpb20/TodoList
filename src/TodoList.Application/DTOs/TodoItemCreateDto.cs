namespace TodoList.TestDataBuilder
{
    public class TodoItemCreateDto
    {
        public string Title { get; set; }

        public string? Description { get; set; } = string.Empty;

        public DateTime? DueAt { get; set; }
    }
}