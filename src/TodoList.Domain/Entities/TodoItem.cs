namespace TodoList.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public TodoStatus Status { get;  private set; }

        public TodoItem(int id, string title, string description = null)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            this.Id = id;
            this.Title = title;
            Description = description ?? string.Empty;

            CreatedAt = DateTime.UtcNow;
            Status = TodoStatus.Pending;
        }

    }
}