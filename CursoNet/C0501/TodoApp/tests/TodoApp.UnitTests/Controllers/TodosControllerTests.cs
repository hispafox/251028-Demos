using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using TodoApp.Api.Services;

namespace TodoApp.UnitTests.Controllers
{
    public class TodosControllerTests
    {
        private readonly Mock<ITodoService> _mockService;
        private readonly TodosController _controller;

        public TodosControllerTests()
        {
            // Arrange - Inicializamos el mock y el controlador para todas las pruebas
            _mockService = new Mock<ITodoService>();
            _controller = new TodosController(_mockService.Object);
        }

        [Fact]
        public void GetAll_LlamaAlServicioYDevuelveOkResult()
        {
            // Arrange - Configuramos el mock para devolver una lista de items
            var todoItems = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Test1", IsComplete = false },
                new TodoItem { Id = 2, Title = "Test2", IsComplete = true }
            };
            _mockService.Setup(s => s.GetAll()).Returns(todoItems);

            // Act - Llamamos al método del controlador
            var result = _controller.GetAll();

            // Assert - Verificamos que el resultado es correcto
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItems = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(okResult.Value);
            Assert.Equal(2, returnItems.Count());

            // También verificamos que se llamó al método correcto del servicio
            _mockService.Verify(s => s.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_ConIdExistente_DevuelveOkResult()
        {
            // Arrange - Configuramos el mock para devolver un item específico
            var todoItem = new TodoItem { Id = 1, Title = "Test", IsComplete = false };
            _mockService.Setup(s => s.GetById(1)).Returns(todoItem);

            // Act - Llamamos al método del controlador
            var result = _controller.GetById(1);

            // Assert - Verificamos que el resultado es correcto
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItem = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal(1, returnItem.Id);
            Assert.Equal("Test", returnItem.Title);
        }

        [Fact]
        public void Create_ConDatosValidos_DevuelveCreatedAtAction()
        {
            // Arrange - Configuramos el mock para simular la creación de un item
            var todoItem = new TodoItem { Title = "New Todo" };
            var createdItem = new TodoItem { Id = 1, Title = "New Todo" };
            _mockService.Setup(s => s.Add(It.IsAny<TodoItem>())).Returns(createdItem);

            // Act - Llamamos al método del controlador
            var result = _controller.Create(todoItem);

            // Assert - Verificamos que el resultado es correcto
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(TodosController.GetById), createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public void Delete_ConIdExistente_DevuelveNoContent()
        {
            // Arrange - Configuramos el mock para simular la eliminación exitosa
            _mockService.Setup(s => s.Delete(1)).Returns(true);

            // Act - Llamamos al método del controlador
            var result = _controller.Delete(1);

            // Assert - Verificamos que el resultado es correcto
            Assert.IsType<NoContentResult>(result);
        }
    }

}
