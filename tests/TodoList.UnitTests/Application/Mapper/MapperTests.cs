using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Infrastructure.Mapper;

namespace TodoList.UnitTests.Application.Mapper
{
    public class MapperTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            using var loggerFactory = LoggerFactory.Create(builder => { /* no providers needed */ });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TodoItemProfile>();
            }, loggerFactory);

            // Act & Assert
            config.AssertConfigurationIsValid();
        }
    }
}
