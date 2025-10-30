using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using TodoApp.Api.Models;
using Xunit;

namespace TodoApp.IntegrationTests.Controllers;

/// <summary>
/// Pruebas de integración para TodosController.
/// Levanta la aplicación completa y hace peticiones HTTP reales.
/// </summary>
public class TodosControllerTests : IntegrationTestBase
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Fact]
    public async Task GetAll_DevuelveOkYColeccion()
    {
        // Act
        var response = await Client.GetAsync("/api/todos");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var todos = await response.Content.ReadFromJsonAsync<List<TodoItem>>(_jsonOptions);
   Assert.NotNull(todos);
    }

    [Fact]
    public async Task GetById_ConIdInexistente_DevuelveNotFound()
    {
        // Act
  var response = await Client.GetAsync("/api/todos/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
  public async Task Create_ConDatosValidos_DevuelveCreatedYNuevoItem()
    {
        // Arrange
        var newTodo = new TodoItem { Title = "Nueva tarea de integración" };

   // Act
        var response = await Client.PostAsJsonAsync("/api/todos", newTodo);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    Assert.NotNull(response.Headers.Location);

    var createdTodo = await response.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        Assert.NotNull(createdTodo);
    Assert.NotEqual(0, createdTodo.Id);
  Assert.Equal(newTodo.Title, createdTodo.Title);
      Assert.False(createdTodo.IsComplete);
    }

    [Fact]
    public async Task Create_ConTituloVacio_DevuelveBadRequest()
    {
      // Arrange
 var newTodo = new TodoItem { Title = "" };

        // Act
 var response = await Client.PostAsJsonAsync("/api/todos", newTodo);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Update_ConDatosValidos_DevuelveOkYItemActualizado()
    {
    // Arrange - Crear una tarea primero
      var newTodo = new TodoItem { Title = "Tarea para actualizar" };
    var createResponse = await Client.PostAsJsonAsync("/api/todos", newTodo);
        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        Assert.NotNull(createdTodo);

     var updatedTodo = new TodoItem 
        { 
      Title = "Tarea actualizada",
      IsComplete = true
     };

   // Act
        var updateResponse = await Client.PutAsJsonAsync($"/api/todos/{createdTodo.Id}", updatedTodo);

        // Assert
   Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var result = await updateResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
    Assert.NotNull(result);
      Assert.Equal(createdTodo.Id, result.Id);
        Assert.Equal("Tarea actualizada", result.Title);
        Assert.True(result.IsComplete);
    }

    [Fact]
    public async Task Update_ConIdInexistente_DevuelveNotFound()
    {
        // Arrange
 var updatedTodo = new TodoItem { Title = "Tarea actualizada" };

     // Act
        var response = await Client.PutAsJsonAsync("/api/todos/999", updatedTodo);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ConIdExistente_DevuelveNoContent()
  {
        // Arrange - Crear una tarea primero
        var newTodo = new TodoItem { Title = "Tarea para eliminar" };
  var createResponse = await Client.PostAsJsonAsync("/api/todos", newTodo);
        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        Assert.NotNull(createdTodo);

        // Act
        var deleteResponse = await Client.DeleteAsync($"/api/todos/{createdTodo.Id}");

   // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verificar que la tarea ya no existe
        var getResponse = await Client.GetAsync($"/api/todos/{createdTodo.Id}");
 Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_ConIdInexistente_DevuelveNotFound()
    {
        // Act
        var response = await Client.DeleteAsync("/api/todos/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
 public async Task FlujoCompleto_CrearActualizarYEliminar()
    {
        // 1. Crear una tarea
        var newTodo = new TodoItem { Title = "Tarea de flujo completo" };
      var createResponse = await Client.PostAsJsonAsync("/api/todos", newTodo);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
 Assert.NotNull(createdTodo);

      // 2. Obtener la tarea creada
 var getResponse = await Client.GetAsync($"/api/todos/{createdTodo.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var fetchedTodo = await getResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        Assert.NotNull(fetchedTodo);
    Assert.Equal(createdTodo.Id, fetchedTodo.Id);

    // 3. Actualizar la tarea
        var updatedTodo = new TodoItem 
        { 
            Title = "Tarea actualizada en flujo",
    IsComplete = true
        };
        var updateResponse = await Client.PutAsJsonAsync($"/api/todos/{createdTodo.Id}", updatedTodo);
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        // 4. Verificar la actualización
var verifyResponse = await Client.GetAsync($"/api/todos/{createdTodo.Id}");
      var verifiedTodo = await verifyResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        Assert.NotNull(verifiedTodo);
   Assert.Equal("Tarea actualizada en flujo", verifiedTodo.Title);
        Assert.True(verifiedTodo.IsComplete);

        // 5. Eliminar la tarea
        var deleteResponse = await Client.DeleteAsync($"/api/todos/{createdTodo.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

    // 6. Verificar la eliminación
        var finalGetResponse = await Client.GetAsync($"/api/todos/{createdTodo.Id}");
     Assert.Equal(HttpStatusCode.NotFound, finalGetResponse.StatusCode);
    }

    [Fact]
    public async Task GetAll_DespuesDeCrearVariasTareas_DevuelveTodasLasTareas()
    {
        // Arrange - Crear múltiples tareas
        var tareas = new[]
        {
            new TodoItem { Title = "Tarea 1" },
            new TodoItem { Title = "Tarea 2" },
            new TodoItem { Title = "Tarea 3" }
        };

        var ids = new List<int>();
  foreach (var tarea in tareas)
  {
            var response = await Client.PostAsJsonAsync("/api/todos", tarea);
   var created = await response.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
          if (created != null)
            {
        ids.Add(created.Id);
      }
        }

    // Act
        var getAllResponse = await Client.GetAsync("/api/todos");
    var allTodos = await getAllResponse.Content.ReadFromJsonAsync<List<TodoItem>>(_jsonOptions);

        // Assert
     Assert.NotNull(allTodos);
   Assert.True(allTodos.Count >= 3, "Debe haber al menos 3 tareas");

        // Cleanup - Eliminar las tareas creadas
        foreach (var id in ids)
        {
       await Client.DeleteAsync($"/api/todos/{id}");
  }
    }
}
