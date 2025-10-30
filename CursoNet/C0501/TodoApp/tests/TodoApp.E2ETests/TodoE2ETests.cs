// TodoApp.E2ETests/TodoE2ETests.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoApp.Api;
using TodoApp.Api.Models;
using Xunit;

namespace TodoApp.E2ETests
{
    public class TodoE2ETests : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public TodoE2ETests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task EscenarioCompleto_GestionDeTareas()
        {
            // PASO 1: Verificar estado inicial (no hay tareas o hay un número específico)
            var initialResponse = await _client.GetAsync("/api/todos");
            initialResponse.EnsureSuccessStatusCode();
            var initialItems = await initialResponse.Content.ReadFromJsonAsync<TodoItem[]>();
            int initialCount = initialItems.Length;

            // PASO 2: Crear primera tarea
            var task1 = new TodoItem { Title = "Completar informe mensual" };
            var createResponse1 = await _client.PostAsJsonAsync("/api/todos", task1);
            createResponse1.EnsureSuccessStatusCode();
            var createdTask1 = await createResponse1.Content.ReadFromJsonAsync<TodoItem>();

            // PASO 3: Crear segunda tarea
            var task2 = new TodoItem { Title = "Preparar presentación" };
            var createResponse2 = await _client.PostAsJsonAsync("/api/todos", task2);
            createResponse2.EnsureSuccessStatusCode();
            var createdTask2 = await createResponse2.Content.ReadFromJsonAsync<TodoItem>();

            // PASO 4: Verificar que ambas tareas existen
            var listResponse = await _client.GetAsync("/api/todos");
            listResponse.EnsureSuccessStatusCode();
            var allTasks = await listResponse.Content.ReadFromJsonAsync<TodoItem[]>();
            Assert.Equal(initialCount + 2, allTasks.Length);

            // PASO 5: Marcar primera tarea como completada
            var updateTask = new TodoItem { Title = createdTask1.Title, IsComplete = true };
            var updateResponse = await _client.PutAsJsonAsync($"/api/todos/{createdTask1.Id}", updateTask);
            updateResponse.EnsureSuccessStatusCode();

            // PASO 6: Verificar que se actualizó correctamente
            var getUpdatedResponse = await _client.GetAsync($"/api/todos/{createdTask1.Id}");
            getUpdatedResponse.EnsureSuccessStatusCode();
            var updatedTask = await getUpdatedResponse.Content.ReadFromJsonAsync<TodoItem>();
            Assert.True(updatedTask.IsComplete);

            // PASO 7: Eliminar la segunda tarea
            var deleteResponse = await _client.DeleteAsync($"/api/todos/{createdTask2.Id}");
            deleteResponse.EnsureSuccessStatusCode();

            // PASO 8: Verificar que la segunda tarea ya no existe
            var getDeletedResponse = await _client.GetAsync($"/api/todos/{createdTask2.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getDeletedResponse.StatusCode);

            // PASO 9: Verificar estado final (debe haber una tarea más que al principio)
            var finalResponse = await _client.GetAsync("/api/todos");
            finalResponse.EnsureSuccessStatusCode();
            var finalItems = await finalResponse.Content.ReadFromJsonAsync<TodoItem[]>();
            Assert.Equal(initialCount + 1, finalItems.Length);

            // PASO 10: Limpiar - eliminar la tarea restante
            await _client.DeleteAsync($"/api/todos/{createdTask1.Id}");
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}