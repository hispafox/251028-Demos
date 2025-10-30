using TodoApp.Api.Models;
using TodoApp.Api.Services;
using Xunit;

namespace TodoApp.UnitTests.Services;

/// <summary>
/// Pruebas unitarias para TodoService.
/// Prueban la lógica de negocio en aislamiento.
/// </summary>
public class TodoServiceTests
{
    private readonly TodoService _todoService;

    public TodoServiceTests()
    {
        _todoService = new TodoService();
    }

    [Fact]
    public void GetAll_CuandoNoHayItems_DevuelveColeccionVacia()
    {
        // Act
 var result = _todoService.GetAll();

// Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetAll_CuandoHayItems_DevuelveTodosLosItems()
    {
        // Arrange
 _todoService.Add(new TodoItem { Title = "Tarea 1" });
   _todoService.Add(new TodoItem { Title = "Tarea 2" });

        // Act
        var result = _todoService.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void Add_ConItemValido_DevuelveItemConId()
    {
        // Arrange
        var item = new TodoItem { Title = "Test Todo" };

      // Act
        var result = _todoService.Add(item);

        // Assert
        Assert.NotEqual(0, result.Id);
        Assert.Equal(item.Title, result.Title);
  Assert.False(result.IsComplete);
    }

    [Fact]
    public void Add_ConTituloVacio_LanzaArgumentException()
    {
        // Arrange
        var item = new TodoItem { Title = "" };

      // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _todoService.Add(item));
     Assert.Contains("título no puede estar vacío", exception.Message);
    }

    [Fact]
    public void Add_ConTituloSoloEspacios_LanzaArgumentException()
  {
        // Arrange
      var item = new TodoItem { Title = "   " };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _todoService.Add(item));
        Assert.Contains("título no puede estar vacío", exception.Message);
    }

    [Fact]
    public void Add_ConItemNulo_LanzaArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _todoService.Add(null!));
    }

    [Fact]
    public void Add_AsignaIdsSecuenciales()
 {
        // Arrange
    var item1 = new TodoItem { Title = "Tarea 1" };
        var item2 = new TodoItem { Title = "Tarea 2" };

        // Act
      var result1 = _todoService.Add(item1);
        var result2 = _todoService.Add(item2);

        // Assert
        Assert.Equal(1, result1.Id);
        Assert.Equal(2, result2.Id);
    }

    [Fact]
    public void GetById_ConIdExistente_DevuelveItem()
  {
        // Arrange
        var item = _todoService.Add(new TodoItem { Title = "Test Todo" });

        // Act
        var result = _todoService.GetById(item.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(item.Id, result.Id);
        Assert.Equal(item.Title, result.Title);
    }

[Fact]
    public void GetById_ConIdInexistente_DevuelveNull()
    {
        // Act
     var result = _todoService.GetById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_ConIdExistente_ActualizaYDevuelveItem()
  {
   // Arrange
    var item = _todoService.Add(new TodoItem { Title = "Título Original" });
        var updatedItem = new TodoItem 
    { 
            Title = "Título Actualizado",
            IsComplete = true
        };

    // Act
        var result = _todoService.Update(item.Id, updatedItem);

   // Assert
Assert.NotNull(result);
     Assert.Equal(item.Id, result.Id);
        Assert.Equal("Título Actualizado", result.Title);
    Assert.True(result.IsComplete);
    }

    [Fact]
    public void Update_ConIdInexistente_DevuelveNull()
    {
     // Arrange
        var updatedItem = new TodoItem { Title = "Título Actualizado" };

        // Act
     var result = _todoService.Update(999, updatedItem);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_ConTituloVacio_LanzaArgumentException()
    {
        // Arrange
        var item = _todoService.Add(new TodoItem { Title = "Título Original" });
        var updatedItem = new TodoItem { Title = "" };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _todoService.Update(item.Id, updatedItem));
    Assert.Contains("título no puede estar vacío", exception.Message);
    }

    [Fact]
    public void Update_ConItemNulo_LanzaArgumentNullException()
    {
        // Arrange
        var item = _todoService.Add(new TodoItem { Title = "Título Original" });

     // Act & Assert
     Assert.Throws<ArgumentNullException>(() => _todoService.Update(item.Id, null!));
    }

    [Fact]
    public void Delete_ConIdExistente_EliminaYDevuelveTrue()
    {
        // Arrange
  var item = _todoService.Add(new TodoItem { Title = "Test Todo" });

 // Act
        var result = _todoService.Delete(item.Id);

        // Assert
        Assert.True(result);
        Assert.Null(_todoService.GetById(item.Id));
    }

    [Fact]
    public void Delete_ConIdInexistente_DevuelveFalse()
    {
     // Act
     var result = _todoService.Delete(999);

        // Assert
      Assert.False(result);
    }

[Fact]
    public void Delete_NoAfectaOtrosItems()
    {
        // Arrange
        var item1 = _todoService.Add(new TodoItem { Title = "Tarea 1" });
        var item2 = _todoService.Add(new TodoItem { Title = "Tarea 2" });

     // Act
        _todoService.Delete(item1.Id);

        // Assert
        Assert.Null(_todoService.GetById(item1.Id));
        Assert.NotNull(_todoService.GetById(item2.Id));
  Assert.Single(_todoService.GetAll());
    }
}
