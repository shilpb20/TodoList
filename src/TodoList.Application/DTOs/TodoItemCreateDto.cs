using System.ComponentModel.DataAnnotations;
using TodoList.Domain;

namespace TodoList.TestDataBuilder
{
    public class TodoItemCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public DateTime? DueAt { get; set; }
    }
}