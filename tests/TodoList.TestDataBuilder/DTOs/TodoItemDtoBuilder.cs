using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;
using TodoList.Domain;
using TodoList.Domain.Enums;

namespace TodoList.TestDataBuilder
{
    public class TodoItemDtoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _title = "Default title";
        private string _description = "Default description";
        private DateTime _createdAt = DateTime.UtcNow;
        private TodoStatus _status = TodoStatus.Pending;

        public TodoItemDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TodoItemDtoBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public TodoItemDtoBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public TodoItemDtoBuilder WithCreatedAt(DateTime createdAt)
        {
            _createdAt = createdAt;
            return this;
        }

        public TodoItemDtoBuilder WithStatus(TodoStatus status)
        {
            _status = status;
            return this;
        }

        public TodoItemDto Build()
        {
            return new TodoItemDto
            {
                Id = _id,
                Title = _title,
                Description = _description,
                CreatedAt = _createdAt,
                Status = _status.ToString(),
            };
        }
    }
}
