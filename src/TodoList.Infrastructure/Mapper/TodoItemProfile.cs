using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities;
using TodoList.TestDataBuilder;

namespace TodoList.Infrastructure.Mapper
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDto>()
                .ForMember(destination => destination.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<TodoItemCreateDto, TodoItem>();
        }
    }
}
