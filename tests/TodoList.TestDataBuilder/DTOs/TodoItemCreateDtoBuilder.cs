using TodoList.Application.DTOs;

namespace TodoList.TestDataBuilder.DTOs
{
    public class TodoItemCreateDtoBuilder
    {
        private string _title = "Default title";
        private string _description = "Default description";


        private DateTime _dueDate = DateTime.Today.AddDays(1);

        public TodoItemCreateDtoBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public TodoItemCreateDtoBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public TodoItemCreateDtoBuilder WithDueDate(DateTime dueDate)
        {
            _dueDate = dueDate;
            return this;
        }

        public TodoItemCreateDtoBuilder WithEmptyTitle()
        {
            _title = string.Empty;
            return this;
        }

        public TodoItemCreateDtoBuilder WithNullTitle()
        {
            _title = null!;
            return this;
        }

        public TodoItemCreateDtoBuilder WithShortTitle()
        {
            _title = "ab";
            return this;
        }

        public TodoItemCreateDtoBuilder WithLongTitle()
        {
            _title = new string('a', 101);
            return this;
        }

        public TodoItemCreateDtoBuilder WithLongDescription()
        {
            _description = new string('a', 1001);
            return this;
        }

        public TodoItemCreateDtoBuilder WithMaxTitleLength()
        {
            _title = new string('a', 100);
            return this;
        }

        public TodoItemCreateDtoBuilder WithMaxDescriptionLength()
        {
            _description = new string('a', 1000);
            return this;
        }

        public TodoItemCreateDto Build()
        {
            return new TodoItemCreateDto
            {
                Description = _description,
                Title = _title,
            };
        }
    }
}
