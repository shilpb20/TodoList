using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

using TodoList.Infrastructure.Mapper;
using TodoList.Application.Services;
using TodoList.Infrastructure.Services;
using TodoList.Application.IRepositories;
using TodoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var loggerFactory = LoggerFactory.Create(builder => { });

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<TodoItemProfile>();
}, loggerFactory);

var mapper = config.CreateMapper();

builder.Services.AddSingleton<IMapper>(mapper);

builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<ITodoItemRepository, InMemoryTodoItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TODO List API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
