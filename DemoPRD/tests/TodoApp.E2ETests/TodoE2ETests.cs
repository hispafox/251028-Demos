using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using TodoApp.Api.Models;
using Xunit;

namespace TodoApp.E2ETests;

/// <summary>
/// Pruebas End-to-End que simulan escenarios completos de usuario.
/// </summary>
public class TodoE2ETests : E2ETestBase
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
   PropertyNameCaseInsensitive = true
    };

    [Fact]
    public async Task EscenarioCompleto_GestionDeTareas()
    {
 var createdIds = new List<int>();

  try
        {
    // ====== PASO 1: Verificar estado inicial ======
       var initialResponse = await Client.GetAsync("/api/todos");
        Assert.Equal(HttpStatusCode.OK, initialResponse.StatusCode);
       var initialTodos = await initialResponse.Content.ReadFromJsonAsync<List<TodoItem>>(_jsonOptions);
 Assert.NotNull(initialTodos);
   var initialCount = initialTodos.Count;

          // ====== PASO 2: Crear primera tarea ======
     var tarea1 = new TodoItem 
    { 
     Title = "Completar informe mensual",
            IsComplete = false
        };

   var createResponse1 = await Client.PostAsJsonAsync("/api/todos", tarea1);
            Assert.Equal(HttpStatusCode.Created, createResponse1.StatusCode);
            Assert.NotNull(createResponse1.Headers.Location);

            var createdTarea1 = await createResponse1.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
            Assert.NotNull(createdTarea1);
Assert.NotEqual(0, createdTarea1.Id);
    Assert.Equal("Completar informe mensual", createdTarea1.Title);
     Assert.False(createdTarea1.IsComplete);
 createdIds.Add(createdTarea1.Id);

  // ====== PASO 3: Crear segunda tarea ======
            var tarea2 = new TodoItem 
    { 
       Title = "Preparar presentación",
    IsComplete = false
            };

 var createResponse2 = await Client.PostAsJsonAsync("/api/todos", tarea2);
       Assert.Equal(HttpStatusCode.Created, createResponse2.StatusCode);

  var createdTarea2 = await createResponse2.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
     Assert.NotNull(createdTarea2);
            Assert.NotEqual(0, createdTarea2.Id);
 Assert.NotEqual(createdTarea1.Id, createdTarea2.Id);
    createdIds.Add(createdTarea2.Id);

// ====== PASO 4: Verificar que ambas tareas existen ======
        var allTodosResponse = await Client.GetAsync("/api/todos");
        var allTodos = await allTodosResponse.Content.ReadFromJsonAsync<List<TodoItem>>(_jsonOptions);
            Assert.NotNull(allTodos);
  Assert.Equal(initialCount + 2, allTodos.Count);

   // ====== PASO 5: Marcar primera tarea como completada ======
          var updateTarea1 = new TodoItem 
         { 
        Title = "Completar informe mensual",
        IsComplete = true
 };

            var updateResponse = await Client.PutAsJsonAsync($"/api/todos/{createdTarea1.Id}", updateTarea1);
     Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

       var updatedTarea1 = await updateResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
            Assert.NotNull(updatedTarea1);
   Assert.True(updatedTarea1.IsComplete);

    // ====== PASO 6: Verificar actualización mediante GET ======
     var getTarea1Response = await Client.GetAsync($"/api/todos/{createdTarea1.Id}");
            Assert.Equal(HttpStatusCode.OK, getTarea1Response.StatusCode);

        var fetchedTarea1 = await getTarea1Response.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        Assert.NotNull(fetchedTarea1);
    Assert.True(fetchedTarea1.IsComplete);
            Assert.Equal("Completar informe mensual", fetchedTarea1.Title);

    // ====== PASO 7: Eliminar segunda tarea ======
     var deleteResponse = await Client.DeleteAsync($"/api/todos/{createdTarea2.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
      createdIds.Remove(createdTarea2.Id);

            // ====== PASO 8: Verificar que la tarea eliminada no existe ======
var getTarea2Response = await Client.GetAsync($"/api/todos/{createdTarea2.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getTarea2Response.StatusCode);

       // ====== PASO 9: Verificar estado final ======
            var finalResponse = await Client.GetAsync("/api/todos");
 var finalTodos = await finalResponse.Content.ReadFromJsonAsync<List<TodoItem>>(_jsonOptions);
            Assert.NotNull(finalTodos);
   Assert.Equal(initialCount + 1, finalTodos.Count);

// Verificar que la primera tarea aún existe y está completada
 var remainingTarea = finalTodos.FirstOrDefault(t => t.Id == createdTarea1.Id);
          Assert.NotNull(remainingTarea);
    Assert.True(remainingTarea.IsComplete);
    Assert.Equal("Completar informe mensual", remainingTarea.Title);

            // ====== PASO 10: Limpiar - Eliminar tarea restante ======
      foreach (var id in createdIds.ToList())
     {
       await Client.DeleteAsync($"/api/todos/{id}");
         }
        createdIds.Clear();
        }
        finally
        {
        // Cleanup en caso de fallo
foreach (var id in createdIds)
       {
try
   {
       await Client.DeleteAsync($"/api/todos/{id}");
   }
    catch
         {
               // Ignorar errores de limpieza
     }
     }
     }
    }

    [Fact]
    public async Task EscenarioValidacion_IntentarCrearTareaInvalida()
    {
   // ====== PASO 1: Intentar crear tarea sin título ======
     var tareaInvalida1 = new TodoItem { Title = "" };
  var response1 = await Client.PostAsJsonAsync("/api/todos", tareaInvalida1);
 Assert.Equal(HttpStatusCode.BadRequest, response1.StatusCode);

        // ====== PASO 2: Intentar crear tarea con título solo espacios ======
        var tareaInvalida2 = new TodoItem { Title = "   " };
var response2 = await Client.PostAsJsonAsync("/api/todos", tareaInvalida2);
     Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);

     // ====== PASO 3: Verificar que no se crearon tareas inválidas ======
        var allTodosResponse = await Client.GetAsync("/api/todos");
        var allTodos = await allTodosResponse.Content.ReadFromJsonAsync<List<TodoItem>>(_jsonOptions);
        Assert.NotNull(allTodos);
        Assert.DoesNotContain(allTodos, t => string.IsNullOrWhiteSpace(t.Title));
    }

    [Fact]
    public async Task EscenarioActualizacion_ModificarMultiplesPropiedades()
    {
        int createdId = 0;

        try
 {
            // ====== PASO 1: Crear tarea inicial ======
var tareaInicial = new TodoItem 
 { 
 Title = "Tarea inicial",
            IsComplete = false
   };

    var createResponse = await Client.PostAsJsonAsync("/api/todos", tareaInicial);
var createdTarea = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
            Assert.NotNull(createdTarea);
            createdId = createdTarea.Id;

            // ====== PASO 2: Actualizar solo el título ======
       var update1 = new TodoItem 
    { 
     Title = "Título actualizado",
       IsComplete = false
  };

  var updateResponse1 = await Client.PutAsJsonAsync($"/api/todos/{createdId}", update1);
            Assert.Equal(HttpStatusCode.OK, updateResponse1.StatusCode);

         // ====== PASO 3: Verificar cambio de título ======
     var getResponse1 = await Client.GetAsync($"/api/todos/{createdId}");
            var fetchedTarea1 = await getResponse1.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
            Assert.NotNull(fetchedTarea1);
          Assert.Equal("Título actualizado", fetchedTarea1.Title);
  Assert.False(fetchedTarea1.IsComplete);

      // ====== PASO 4: Actualizar solo el estado ======
            var update2 = new TodoItem 
      { 
         Title = "Título actualizado",
                IsComplete = true
      };

        var updateResponse2 = await Client.PutAsJsonAsync($"/api/todos/{createdId}", update2);
       Assert.Equal(HttpStatusCode.OK, updateResponse2.StatusCode);

      // ====== PASO 5: Verificar cambio de estado ======
      var getResponse2 = await Client.GetAsync($"/api/todos/{createdId}");
  var fetchedTarea2 = await getResponse2.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
            Assert.NotNull(fetchedTarea2);
    Assert.Equal("Título actualizado", fetchedTarea2.Title);
     Assert.True(fetchedTarea2.IsComplete);

            // ====== PASO 6: Actualizar ambas propiedades ======
            var update3 = new TodoItem 
        { 
       Title = "Título final",
       IsComplete = false
     };

    var updateResponse3 = await Client.PutAsJsonAsync($"/api/todos/{createdId}", update3);
            Assert.Equal(HttpStatusCode.OK, updateResponse3.StatusCode);

   // ====== PASO 7: Verificar cambios finales ======
        var getResponse3 = await Client.GetAsync($"/api/todos/{createdId}");
            var fetchedTarea3 = await getResponse3.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
  Assert.NotNull(fetchedTarea3);
            Assert.Equal("Título final", fetchedTarea3.Title);
            Assert.False(fetchedTarea3.IsComplete);
        }
        finally
      {
      // Cleanup
            if (createdId != 0)
  {
         await Client.DeleteAsync($"/api/todos/{createdId}");
            }
      }
 }
}
