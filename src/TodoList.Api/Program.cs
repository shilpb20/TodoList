using AutoMapper;
using TodoList.Application.IRepositories;
using TodoList.Application.IServices;
using TodoList.Infrastructure.Mapper;
using TodoList.Infrastructure.Repositories;
using TodoList.Infrastructure.Services;

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
builder.Services.AddSingleton<ITodoItemRepository, InMemoryTodoItemRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
