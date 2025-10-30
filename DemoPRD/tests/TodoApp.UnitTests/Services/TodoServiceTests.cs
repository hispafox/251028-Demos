using AutoMapper;
using Moq;
using TodoApp.Api.Data.Entities;
using TodoApp.Api.Data.Repositories;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services;
using Xunit;

namespace TodoApp.UnitTests.Services;

/// <summary>
/// Pruebas unitarias para TodoService.
/// Prueban la lógica de negocio con mocks del repository.
/// </summary>
public class TodoServiceTests
{
private readonly Mock<ITodoRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
  private readonly TodoService _todoService;

    public TodoServiceTests()
    {
 _mockRepository = new Mock<ITodoRepository>();
        _mockMapper = new Mock<IMapper>();
_todoService = new TodoService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_CuandoNoHayItems_DevuelveColeccionVacia()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TodoEntity>());
        _mockMapper.Setup(m => m.Map<IEnumerable<TodoItemDto>>(It.IsAny<IEnumerable<TodoEntity>>()))
      .Returns(new List<TodoItemDto>());

        // Act
        var result = await _todoService.GetAllAsync();

      // Assert
        Assert.NotNull(result);
   Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_CuandoHayItems_DevuelveTodosLosItems()
    {
// Arrange
        var entities = new List<TodoEntity>
    {
            new TodoEntity { Id = 1, Title = "Tarea 1" },
            new TodoEntity { Id = 2, Title = "Tarea 2" }
  };
        var dtos = new List<TodoItemDto>
        {
    new TodoItemDto { Id = 1, Title = "Tarea 1" },
 new TodoItemDto { Id = 2, Title = "Tarea 2" }
    };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.Map<IEnumerable<TodoItemDto>>(entities)).Returns(dtos);

        // Act
        var result = await _todoService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateAsync_ConItemValido_DevuelveItemConId()
    {
        // Arrange
        var dto = new CreateTodoItemDto { Title = "Test Todo" };
     var entity = new TodoEntity { Title = "Test Todo" };
        var createdEntity = new TodoEntity { Id = 1, Title = "Test Todo", CreatedAt = DateTime.UtcNow };
        var resultDto = new TodoItemDto { Id = 1, Title = "Test Todo" };

        _mockMapper.Setup(m => m.Map<TodoEntity>(dto)).Returns(entity);
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<TodoEntity>())).ReturnsAsync(createdEntity);
 _mockMapper.Setup(m => m.Map<TodoItemDto>(createdEntity)).Returns(resultDto);

 // Act
        var result = await _todoService.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
     Assert.Equal(1, result.Id);
Assert.Equal("Test Todo", result.Title);
    }

    [Fact]
    public async Task CreateAsync_ConTituloVacio_LanzaArgumentException()
    {
        // Arrange
        var dto = new CreateTodoItemDto { Title = "" };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _todoService.CreateAsync(dto));
    Assert.Contains("título no puede estar vacío", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_ConTituloSoloEspacios_LanzaArgumentException()
    {
        // Arrange
        var dto = new CreateTodoItemDto { Title = "   " };

        // Act & Assert
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => _todoService.CreateAsync(dto));
        Assert.Contains("título no puede estar vacío", exception.Message);
    }

    [Fact]
    public async Task GetByIdAsync_ConIdExistente_DevuelveItem()
    {
        // Arrange
        var entity = new TodoEntity { Id = 1, Title = "Test Todo" };
     var dto = new TodoItemDto { Id = 1, Title = "Test Todo" };

        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
  _mockMapper.Setup(m => m.Map<TodoItemDto>(entity)).Returns(dto);

        // Act
        var result = await _todoService.GetByIdAsync(1);

        // Assert
      Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Todo", result.Title);
    }

    [Fact]
  public async Task GetByIdAsync_ConIdInexistente_DevuelveNull()
    {
// Arrange
  _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((TodoEntity?)null);

        // Act
        var result = await _todoService.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ConIdExistente_ActualizaYDevuelveItem()
    {
        // Arrange
        var updateDto = new UpdateTodoItemDto { Title = "Título Actualizado", IsComplete = true };
        var existingEntity = new TodoEntity { Id = 1, Title = "Título Original", IsComplete = false };
      var updatedEntity = new TodoEntity { Id = 1, Title = "Título Actualizado", IsComplete = true, UpdatedAt = DateTime.UtcNow };
        var resultDto = new TodoItemDto { Id = 1, Title = "Título Actualizado", IsComplete = true };

  _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingEntity);
      _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<TodoEntity>())).ReturnsAsync(updatedEntity);
        _mockMapper.Setup(m => m.Map<TodoItemDto>(updatedEntity)).Returns(resultDto);

        // Act
   var result = await _todoService.UpdateAsync(1, updateDto);

        // Assert
      Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Título Actualizado", result.Title);
        Assert.True(result.IsComplete);
    }

    [Fact]
    public async Task UpdateAsync_ConIdInexistente_DevuelveNull()
    {
    // Arrange
        var updateDto = new UpdateTodoItemDto { Title = "Título Actualizado" };
        _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((TodoEntity?)null);

        // Act
        var result = await _todoService.UpdateAsync(999, updateDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ConTituloVacio_LanzaArgumentException()
    {
        // Arrange
        var updateDto = new UpdateTodoItemDto { Title = "" };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _todoService.UpdateAsync(1, updateDto));
        Assert.Contains("título no puede estar vacío", exception.Message);
    }

    [Fact]
 public async Task DeleteAsync_ConIdExistente_EliminaYDevuelveTrue()
    {
    // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _todoService.DeleteAsync(1);

// Assert
        Assert.True(result);
        _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ConIdInexistente_DevuelveFalse()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(999)).ReturnsAsync(false);

        // Act
        var result = await _todoService.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetCompletedAsync_DevuelveSoloTareasCompletadas()
    {
        // Arrange
        var entities = new List<TodoEntity>
        {
     new TodoEntity { Id = 1, Title = "Tarea 1", IsComplete = true },
   new TodoEntity { Id = 2, Title = "Tarea 2", IsComplete = true }
   };
        var dtos = new List<TodoItemDto>
 {
            new TodoItemDto { Id = 1, Title = "Tarea 1", IsComplete = true },
        new TodoItemDto { Id = 2, Title = "Tarea 2", IsComplete = true }
 };

        _mockRepository.Setup(r => r.GetCompletedAsync()).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.Map<IEnumerable<TodoItemDto>>(entities)).Returns(dtos);

     // Act
        var result = await _todoService.GetCompletedAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, item => Assert.True(item.IsComplete));
    }

    [Fact]
  public async Task GetPendingAsync_DevuelveSoloTareasPendientes()
    {
        // Arrange
    var entities = new List<TodoEntity>
 {
    new TodoEntity { Id = 1, Title = "Tarea 1", IsComplete = false },
 new TodoEntity { Id = 2, Title = "Tarea 2", IsComplete = false }
        };
        var dtos = new List<TodoItemDto>
        {
    new TodoItemDto { Id = 1, Title = "Tarea 1", IsComplete = false },
 new TodoItemDto { Id = 2, Title = "Tarea 2", IsComplete = false }
        };

        _mockRepository.Setup(r => r.GetPendingAsync()).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.Map<IEnumerable<TodoItemDto>>(entities)).Returns(dtos);

        // Act
        var result = await _todoService.GetPendingAsync();

        // Assert
    Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, item => Assert.False(item.IsComplete));
    }
}
