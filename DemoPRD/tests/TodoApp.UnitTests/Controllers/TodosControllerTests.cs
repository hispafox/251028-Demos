using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.Api.Controllers;
using TodoApp.Api.DTOs;
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
    public async Task GetAll_LlamaAlServicioYDevuelveOkResult()
    {
    // Arrange
        var expectedTodos = new List<TodoItemDto>
        {
         new TodoItemDto { Id = 1, Title = "Tarea 1" },
         new TodoItemDto { Id = 2, Title = "Tarea 2" }
  };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedTodos);

        // Act
        var result = await _controller.GetAll();

        // Assert
      var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todos = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
        Assert.Equal(2, todos.Count());
   _mockService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAll_ConColeccionVacia_DevuelveOkConColeccionVacia()
    {
 // Arrange
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<TodoItemDto>());

     // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
   var todos = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
        Assert.Empty(todos);
    }

    [Fact]
    public async Task GetById_ConIdExistente_DevuelveOkResult()
    {
        // Arrange
 var expectedTodo = new TodoItemDto { Id = 1, Title = "Test Todo" };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(expectedTodo);

        // Act
        var result = await _controller.GetById(1);

        // Assert
    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todo = Assert.IsType<TodoItemDto>(okResult.Value);
  Assert.Equal(expectedTodo.Id, todo.Id);
        Assert.Equal(expectedTodo.Title, todo.Title);
        _mockService.Verify(s => s.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetById_ConIdInexistente_DevuelveNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((TodoItemDto?)null);

        // Act
        var result = await _controller.GetById(999);

     // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        _mockService.Verify(s => s.GetByIdAsync(999), Times.Once);
    }

    [Fact]
    public async Task Create_ConDatosValidos_DevuelveCreatedAtAction()
    {
        // Arrange
        var createDto = new CreateTodoItemDto { Title = "Nueva Tarea" };
      var createdDto = new TodoItemDto { Id = 1, Title = "Nueva Tarea", IsComplete = false };
   _mockService.Setup(s => s.CreateAsync(It.IsAny<CreateTodoItemDto>())).ReturnsAsync(createdDto);

        // Act
        var result = await _controller.Create(createDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
  Assert.Equal(nameof(TodosController.GetById), createdResult.ActionName);
        Assert.Equal(1, createdResult.RouteValues?["id"]);
        var todo = Assert.IsType<TodoItemDto>(createdResult.Value);
     Assert.Equal(createdDto.Id, todo.Id);
      Assert.Equal(createdDto.Title, todo.Title);
        _mockService.Verify(s => s.CreateAsync(It.IsAny<CreateTodoItemDto>()), Times.Once);
    }

    [Fact]
    public async Task Create_ConTituloVacio_DevuelveBadRequest()
    {
        // Arrange
        var createDto = new CreateTodoItemDto { Title = "" };
        _mockService.Setup(s => s.CreateAsync(It.IsAny<CreateTodoItemDto>()))
            .ThrowsAsync(new ArgumentException("El título no puede estar vacío"));

        // Act
        var result = await _controller.Create(createDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("título no puede estar vacío", badRequestResult.Value?.ToString());
    }

    [Fact]
    public async Task Update_ConDatosValidos_DevuelveOkResult()
    {
        // Arrange
        var updateDto = new UpdateTodoItemDto { Title = "Tarea Actualizada", IsComplete = true };
        var resultDto = new TodoItemDto { Id = 1, Title = "Tarea Actualizada", IsComplete = true };
_mockService.Setup(s => s.UpdateAsync(1, It.IsAny<UpdateTodoItemDto>())).ReturnsAsync(resultDto);

        // Act
        var result = await _controller.Update(1, updateDto);

        // Assert
      var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todo = Assert.IsType<TodoItemDto>(okResult.Value);
        Assert.Equal(resultDto.Id, todo.Id);
        Assert.Equal(resultDto.Title, todo.Title);
        Assert.True(todo.IsComplete);
        _mockService.Verify(s => s.UpdateAsync(1, It.IsAny<UpdateTodoItemDto>()), Times.Once);
    }

 [Fact]
 public async Task Update_ConIdInexistente_DevuelveNotFound()
    {
        // Arrange
        var updateDto = new UpdateTodoItemDto { Title = "Tarea Actualizada" };
 _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<UpdateTodoItemDto>())).ReturnsAsync((TodoItemDto?)null);

        // Act
    var result = await _controller.Update(999, updateDto);

   // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        _mockService.Verify(s => s.UpdateAsync(999, It.IsAny<UpdateTodoItemDto>()), Times.Once);
    }

[Fact]
    public async Task Update_ConTituloVacio_DevuelveBadRequest()
    {
        // Arrange
        var updateDto = new UpdateTodoItemDto { Title = "" };
        _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<UpdateTodoItemDto>()))
            .ThrowsAsync(new ArgumentException("El título no puede estar vacío"));

        // Act
        var result = await _controller.Update(1, updateDto);

        // Assert
  var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("título no puede estar vacío", badRequestResult.Value?.ToString());
  }

    [Fact]
    public async Task Delete_ConIdExistente_DevuelveNoContent()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

    // Act
        var result = await _controller.Delete(1);

  // Assert
  Assert.IsType<NoContentResult>(result);
     _mockService.Verify(s => s.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Delete_ConIdInexistente_DevuelveNotFound()
    {
     // Arrange
        _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

        // Act
     var result = await _controller.Delete(999);

   // Assert
 Assert.IsType<NotFoundResult>(result);
    _mockService.Verify(s => s.DeleteAsync(999), Times.Once);
    }

    [Fact]
    public async Task GetCompleted_DevuelveOkConTareasCompletadas()
    {
        // Arrange
        var completedTodos = new List<TodoItemDto>
        {
      new TodoItemDto { Id = 1, Title = "Tarea 1", IsComplete = true },
    new TodoItemDto { Id = 2, Title = "Tarea 2", IsComplete = true }
        };
   _mockService.Setup(s => s.GetCompletedAsync()).ReturnsAsync(completedTodos);

        // Act
        var result = await _controller.GetCompleted();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todos = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
        Assert.Equal(2, todos.Count());
        Assert.All(todos, t => Assert.True(t.IsComplete));
    }

    [Fact]
    public async Task GetPending_DevuelveOkConTareasPendientes()
    {
        // Arrange
     var pendingTodos = new List<TodoItemDto>
  {
            new TodoItemDto { Id = 1, Title = "Tarea 1", IsComplete = false },
         new TodoItemDto { Id = 2, Title = "Tarea 2", IsComplete = false }
        };
    _mockService.Setup(s => s.GetPendingAsync()).ReturnsAsync(pendingTodos);

  // Act
        var result = await _controller.GetPending();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    var todos = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
        Assert.Equal(2, todos.Count());
        Assert.All(todos, t => Assert.False(t.IsComplete));
    }
}
