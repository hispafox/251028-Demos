using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Api.Models;
using TodoApp.Api.Services;

namespace TodoApp.UnitTests.Services
{
    public class TodoServiceTests
    {
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            // Arrange - Inicializamos el servicio para todas las pruebas
            _todoService = new TodoService();
        }

        [Fact]
        public void GetAll_CuandoNoHayItems_DevuelveColeccionVacia()
        {
            // Act - Obtenemos todos los items
            var result = _todoService.GetAll();

            // Assert - Verificamos que la colección está vacía
            Assert.Empty(result);
        }

        [Fact]
        public void Add_ConItemValido_DevuelveItemConId()
        {
            // Arrange - Creamos un item de prueba
            var item = new TodoItem { Title = "Test Todo" };

            // Act - Agregamos el item
            var result = _todoService.Add(item);

            // Assert - Verificamos que el item se agregó correctamente
            Assert.NotEqual(0, result.Id);
            Assert.Equal(item.Title, result.Title);
        }

        [Fact]
        public void Add_ConTituloVacio_LanzaArgumentException()
        {
            // Arrange - Creamos un item con título vacío
            var item = new TodoItem { Title = "" };

            // Act & Assert - Verificamos que se lance la excepción esperada
            var exception = Assert.Throws<ArgumentException>(() => _todoService.Add(item));
            Assert.Contains("título no puede estar vacío", exception.Message);
        }

        [Fact]
        public void GetById_ConIdExistente_DevuelveItem()
        {
            // Arrange - Agregamos un item para luego buscarlo
            var item = new TodoItem { Title = "Test Todo" };
            var addedItem = _todoService.Add(item);

            // Act - Buscamos el item por su ID
            var result = _todoService.GetById(addedItem.Id);

            // Assert - Verificamos que se encontró el item correcto
            Assert.NotNull(result);
            Assert.Equal(addedItem.Id, result.Id);
            Assert.Equal("Test Todo", result.Title);
        }

        [Fact]
        public void Delete_ConIdExistente_EliminaYDevuelveTrue()
        {
            // Arrange - Agregamos un item para luego eliminarlo
            var item = _todoService.Add(new TodoItem { Title = "To Delete" });

            // Act - Eliminamos el item
            var result = _todoService.Delete(item.Id);

            // Assert - Verificamos que se eliminó correctamente
            Assert.True(result);
            Assert.Null(_todoService.GetById(item.Id));
        }
    }
}
