using AutoMapper;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;

namespace TodoList.Infrastructure.Mapper
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemCreateDto, TodoItem>()
                .ConstructUsing(dto => new TodoItem(dto.Title, dto.Description))
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(destination => destination.CreatedAt, opt => opt.Ignore())
                .ForMember(destination => destination.Status, opt => opt.Ignore());

            CreateMap<TodoItem, TodoItemDto>()
                .ForMember(destination => destination.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<TodoItemDto, TodoItem>()
                        .ForMember(destination => destination.Status, opt => opt.MapFrom(src => ParseStatus(src.Status)));
        }

        private static TodoStatus ParseStatus(string status)
        {
            return Enum.TryParse<TodoStatus>(status, out var parsedStatus)
                ? parsedStatus
                : TodoStatus.Pending;
        }
    }
}
