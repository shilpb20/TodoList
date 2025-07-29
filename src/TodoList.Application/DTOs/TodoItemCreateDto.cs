using System.ComponentModel.DataAnnotations;

namespace TodoList.Application.DTOs
{
    public class TodoItemCreateDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; } = string.Empty;
    }
}