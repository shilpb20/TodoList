using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.TestDataBuilder
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
