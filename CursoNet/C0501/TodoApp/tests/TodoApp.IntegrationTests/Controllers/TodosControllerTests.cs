using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Api.Models;

namespace TodoApp.IntegrationTests.Controllers
{
    public class TodosControllerTests : IntegrationTestBase
    {



        [Fact]
        public async Task GetAll_DevuelveOkYColeccion()
        {
            // Arrange 
            // Ocurre en el constructor de la clase base (IntegrationTestBase)

            // Act - Hacemos una solicitud GET a la API
            var response = await Client.GetAsync("/api/todos");

            // Assert - Verificamos que la respuesta sea correcta
            response.EnsureSuccessStatusCode(); // Status 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verificamos que podemos deserializar la respuesta
            var items = await response.Content.ReadFromJsonAsync<TodoItem[]>();
            Assert.NotNull(items);


        }

        [Fact]
        public async Task GetById_ConIdInexistente_DevuelveNotFound()
        {
            // Act - Intentamos obtener un item que no existe
            var response = await Client.GetAsync("/api/todos/999");

            // Assert - Verificamos que recibimos un 404
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Create_ConDatosValidos_DevuelveCreatedYNuevoItem()
        {
            // Arrange - Preparamos un nuevo item
            var newItem = new TodoItem { Title = "Integration Test Todo" };

            // Act - Enviamos una solicitud POST para crear el item
            var response = await Client.PostAsJsonAsync("/api/todos", newItem);

            // Assert - Verificamos que la respuesta sea correcta
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Verificamos que podemos deserializar la respuesta y que contiene los datos correctos
            var createdItem = await response.Content.ReadFromJsonAsync<TodoItem>();
            Assert.NotNull(createdItem);
            Assert.Equal(newItem.Title, createdItem.Title);
            Assert.NotEqual(0, createdItem.Id); // Se debe haber asignado un ID

            // Verificamos que la ubicación en el encabezado es correcta
            Assert.NotNull(response.Headers.Location);
        }

        [Fact]
        public async Task FlujoCompleto_CrearActualizarYEliminar()
        {
            // Paso 1: Crear un item
            var newItem = new TodoItem { Title = "Todo para flujo completo" };
            var createResponse = await Client.PostAsJsonAsync("/api/todos", newItem);
            createResponse.EnsureSuccessStatusCode();
            var createdItem = await createResponse.Content.ReadFromJsonAsync<TodoItem>();

            // Paso 2: Actualizar el item
            var updateItem = new TodoItem { Title = "Todo actualizado", IsComplete = true };
            var updateResponse = await Client.PutAsJsonAsync($"/api/todos/{createdItem.Id}", updateItem);
            updateResponse.EnsureSuccessStatusCode();
            var updatedItem = await updateResponse.Content.ReadFromJsonAsync<TodoItem>();
            Assert.Equal("Todo actualizado", updatedItem.Title);
            Assert.True(updatedItem.IsComplete);

            // Paso 3: Eliminar el item
            var deleteResponse = await Client.DeleteAsync($"/api/todos/{createdItem.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Paso 4: Verificar que el item ya no existe
            var getResponse = await Client.GetAsync($"/api/todos/{createdItem.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }

}

