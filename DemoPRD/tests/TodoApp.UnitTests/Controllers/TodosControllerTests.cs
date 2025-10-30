using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using TodoApp.Api.Services;
using Xunit;

namespace TodoApp.UnitTests.Controllers;

/// <summary>
/// Pruebas unitarias para TodosController.
/// Usa Moq para aislar el controlador del servicio.
/// </summary>
public class TodosControllerTests
{
    private readonly Mock<ITodoService> _mockService;
    private readonly TodosController _controller;

    public TodosControllerTests()
    {
        _mockService = new Mock<ITodoService>();
   _controller = new TodosController(_mockService.Object);
    }

    [Fact]
    public void GetAll_LlamaAlServicioYDevuelveOkResult()
    {
        // Arrange
        var expectedTodos = new List<TodoItem>
        {
       new TodoItem { Id = 1, Title = "Tarea 1" },
            new TodoItem { Id = 2, Title = "Tarea 2" }
        };
        _mockService.Setup(s => s.GetAll()).Returns(expectedTodos);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todos = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(okResult.Value);
        Assert.Equal(2, todos.Count());
        _mockService.Verify(s => s.GetAll(), Times.Once);
    }

    [Fact]
    public void GetAll_ConColeccionVacia_DevuelveOkConColeccionVacia()
    {
        // Arrange
        _mockService.Setup(s => s.GetAll()).Returns(new List<TodoItem>());

        // Act
      var result = _controller.GetAll();

  // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todos = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(okResult.Value);
  Assert.Empty(todos);
    }

    [Fact]
    public void GetById_ConIdExistente_DevuelveOkResult()
    {
 // Arrange
        var expectedTodo = new TodoItem { Id = 1, Title = "Test Todo" };
        _mockService.Setup(s => s.GetById(1)).Returns(expectedTodo);

// Act
        var result = _controller.GetById(1);

        // Assert
var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todo = Assert.IsType<TodoItem>(okResult.Value);
        Assert.Equal(expectedTodo.Id, todo.Id);
        Assert.Equal(expectedTodo.Title, todo.Title);
   _mockService.Verify(s => s.GetById(1), Times.Once);
    }

    [Fact]
    public void GetById_ConIdInexistente_DevuelveNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetById(999)).Returns((TodoItem?)null);

        // Act
      var result = _controller.GetById(999);

      // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        _mockService.Verify(s => s.GetById(999), Times.Once);
    }

    [Fact]
    public void Create_ConDatosValidos_DevuelveCreatedAtAction()
    {
// Arrange
        var newTodo = new TodoItem { Title = "Nueva Tarea" };
      var createdTodo = new TodoItem { Id = 1, Title = "Nueva Tarea", IsComplete = false };
      _mockService.Setup(s => s.Add(It.IsAny<TodoItem>())).Returns(createdTodo);

// Act
        var result = _controller.Create(newTodo);

   // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
  Assert.Equal(nameof(TodosController.GetById), createdResult.ActionName);
        Assert.Equal(1, createdResult.RouteValues?["id"]);
        var todo = Assert.IsType<TodoItem>(createdResult.Value);
        Assert.Equal(createdTodo.Id, todo.Id);
        Assert.Equal(createdTodo.Title, todo.Title);
        _mockService.Verify(s => s.Add(It.IsAny<TodoItem>()), Times.Once);
    }

    [Fact]
    public void Create_ConTituloVacio_DevuelveBadRequest()
    {
        // Arrange
     var newTodo = new TodoItem { Title = "" };
        _mockService.Setup(s => s.Add(It.IsAny<TodoItem>()))
  .Throws(new ArgumentException("El título no puede estar vacío"));

        // Act
        var result = _controller.Create(newTodo);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
     Assert.Contains("título no puede estar vacío", badRequestResult.Value?.ToString());
    }

    [Fact]
    public void Update_ConDatosValidos_DevuelveOkResult()
    {
        // Arrange
        var updatedTodo = new TodoItem { Title = "Tarea Actualizada", IsComplete = true };
        var resultTodo = new TodoItem { Id = 1, Title = "Tarea Actualizada", IsComplete = true };
        _mockService.Setup(s => s.Update(1, It.IsAny<TodoItem>())).Returns(resultTodo);

        // Act
        var result = _controller.Update(1, updatedTodo);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
 var todo = Assert.IsType<TodoItem>(okResult.Value);
        Assert.Equal(resultTodo.Id, todo.Id);
   Assert.Equal(resultTodo.Title, todo.Title);
        Assert.True(todo.IsComplete);
        _mockService.Verify(s => s.Update(1, It.IsAny<TodoItem>()), Times.Once);
    }

    [Fact]
    public void Update_ConIdInexistente_DevuelveNotFound()
    {
        // Arrange
    var updatedTodo = new TodoItem { Title = "Tarea Actualizada" };
      _mockService.Setup(s => s.Update(999, It.IsAny<TodoItem>())).Returns((TodoItem?)null);

        // Act
        var result = _controller.Update(999, updatedTodo);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        _mockService.Verify(s => s.Update(999, It.IsAny<TodoItem>()), Times.Once);
    }

    [Fact]
    public void Update_ConTituloVacio_DevuelveBadRequest()
    {
        // Arrange
        var updatedTodo = new TodoItem { Title = "" };
        _mockService.Setup(s => s.Update(1, It.IsAny<TodoItem>()))
   .Throws(new ArgumentException("El título no puede estar vacío"));

        // Act
        var result = _controller.Update(1, updatedTodo);

   // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("título no puede estar vacío", badRequestResult.Value?.ToString());
    }

    [Fact]
    public void Delete_ConIdExistente_DevuelveNoContent()
    {
        // Arrange
        _mockService.Setup(s => s.Delete(1)).Returns(true);

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
     _mockService.Verify(s => s.Delete(1), Times.Once);
    }

    [Fact]
    public void Delete_ConIdInexistente_DevuelveNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.Delete(999)).Returns(false);

        // Act
        var result = _controller.Delete(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    _mockService.Verify(s => s.Delete(999), Times.Once);
    }
}
