# Instrucciones para Pruebas de Integraci�n - TodoApp

## ?? Descripci�n del Proyecto

Este proyecto contiene pruebas de integraci�n para la aplicaci�n TodoApp, utilizando **xUnit** y **Microsoft.AspNetCore.Mvc.Testing** para probar la API completa en un entorno controlado.

---

## ??? Convenciones de Nomenclatura

### Nombres de M�todos de Prueba

```
[M�todo]_[Condici�n]_[ResultadoEsperado]
```

**Ejemplos:**
- `GetAll_DevuelveOkYColeccion`
- `GetById_ConIdInexistente_DevuelveNotFound`
- `Create_ConDatosValidos_DevuelveCreatedYNuevoItem`
- `FlujoCompleto_CrearActualizarYEliminar`

### Nombres de Clases de Prueba

```
[ControladorObjetivo]Tests
```

**Ejemplos:**
- `TodosControllerTests` - Pruebas de integraci�n para el controlador Todos

---

## ?? Estructura AAA (Arrange-Act-Assert)

Todas las pruebas deben seguir el patr�n AAA con comentarios expl�citos:

```csharp
[Fact]
public async Task NombreDelTest()
{
    // Arrange - Preparar datos de prueba
    var newItem = new TodoItem { Title = "Test" };

    // Act - Realizar petici�n HTTP
    var response = await Client.PostAsJsonAsync("/api/todos", newItem);

    // Assert - Verificar respuesta HTTP y datos
    response.EnsureSuccessStatusCode();
Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}
```

---

## ?? Clase Base de Integraci�n

```csharp
/// <summary>
/// Clase base que configura WebApplicationFactory para todas las pruebas
/// </summary>
public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient Client;

    public IntegrationTestBase(WebApplicationFactory<Program> factory)
    {
    // Arrange - Cliente HTTP reutilizable para todas las pruebas
        Client = factory.CreateClient();
    }
}
```

**Ventajas:**
- **Reutilizaci�n**: Un solo servidor de pruebas para todas las pruebas de la clase
- **Rendimiento**: Evita crear y destruir el servidor en cada test
- **Realismo**: Usa la configuraci�n real de `Program.cs`

---

## ?? Tipos de Pruebas de Integraci�n

### 1. Pruebas de Endpoints HTTP

**Objetivo:** Validar que los endpoints de la API funcionan correctamente con HTTP real.

**Caracter�sticas:**
- Realizar llamadas HTTP reales al servidor de pruebas
- Validar c�digos de estado HTTP
- Verificar serializaci�n/deserializaci�n JSON
- Probar headers (Location, Content-Type, etc.)

**Ejemplo:**

```csharp
public class TodosControllerTests : IntegrationTestBase
{
    [Fact]
    public async Task GetAll_DevuelveOkYColeccion()
    {
        // Arrange - No se necesita, el cliente ya est� configurado

     // Act - Hacer petici�n GET
    var response = await Client.GetAsync("/api/todos");

        // Assert - Verificar respuesta
    response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Verificar deserializaci�n
   var items = await response.Content.ReadFromJsonAsync<TodoItem[]>();
        Assert.NotNull(items);
    }

    [Fact]
    public async Task Create_ConDatosValidos_DevuelveCreatedYNuevoItem()
    {
     // Arrange
        var newItem = new TodoItem { Title = "Integration Test Todo" };

        // Act
        var response = await Client.PostAsJsonAsync("/api/todos", newItem);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

   var createdItem = await response.Content.ReadFromJsonAsync<TodoItem>();
        Assert.NotNull(createdItem);
        Assert.Equal(newItem.Title, createdItem.Title);
        Assert.NotEqual(0, createdItem.Id);

        // Verificar header Location
        Assert.NotNull(response.Headers.Location);
    }
}
```

### 2. Pruebas de Flujos Completos

**Objetivo:** Validar escenarios realistas que involucran m�ltiples operaciones.

**Caracter�sticas:**
- Simular flujos de usuario completos
- Combinar operaciones CRUD
- Verificar consistencia de datos entre operaciones
- Validar efectos secundarios

**Ejemplo:**

```csharp
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
```

### 3. Pruebas de Validaci�n

**Objetivo:** Verificar que la API valida correctamente los datos de entrada.

**Caracter�sticas:**
- Enviar datos inv�lidos
- Verificar c�digos de error apropiados (400 Bad Request)
- Validar mensajes de error

**Ejemplo:**

```csharp
[Fact]
public async Task Create_ConTituloVacio_DevuelveBadRequest()
{
// Arrange
    var invalidItem = new TodoItem { Title = "" };

    // Act
    var response = await Client.PostAsJsonAsync("/api/todos", invalidItem);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
}

[Theory]
[InlineData("")]
[InlineData(null)]
[InlineData("   ")]
public async Task Create_ConTituloInvalido_DevuelveBadRequest(string titulo)
{
    // Arrange
 var invalidItem = new TodoItem { Title = titulo };

    // Act
var response = await Client.PostAsJsonAsync("/api/todos", invalidItem);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
}
```

---

## ?? Plan de Generaci�n de Tests Completos

### Paso 1: Identificar Endpoints a Probar

- **GET** `/api/todos` - Listar todos los items
- **GET** `/api/todos/{id}` - Obtener un item espec�fico
- **POST** `/api/todos` - Crear nuevo item
- **PUT** `/api/todos/{id}` - Actualizar item existente
- **DELETE** `/api/todos/{id}` - Eliminar item

### Paso 2: Dise�ar Casos de Prueba

#### 2.1 Casos Happy Path (Exitosos)
- Listar items cuando hay datos
- Crear item con datos v�lidos
- Actualizar item existente
- Eliminar item existente

#### 2.2 Casos con Colecciones Vac�as
- Listar items cuando no hay datos
- Verificar respuesta OK con array vac�o

#### 2.3 Casos de Error
- Obtener item inexistente (404)
- Crear item con datos inv�lidos (400)
- Actualizar item inexistente (404)
- Eliminar item inexistente (404)

#### 2.4 Casos de Flujos Completos
- CRUD completo en una sola prueba
- M�ltiples operaciones en secuencia
- Verificar consistencia entre operaciones

### Paso 3: Implementar Tests con Async/Await

```csharp
[Fact]
public async Task GetById_ConIdInexistente_DevuelveNotFound()
{
    // Act - Intentar obtener item que no existe
    var response = await Client.GetAsync("/api/todos/999");

    // Assert - Verificar c�digo 404
  Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

### Paso 4: Verificar Respuestas HTTP Completas

```csharp
[Fact]
public async Task Create_ConDatosValidos_VerificaRespuestaCompleta()
{
    // Arrange
    var newItem = new TodoItem { Title = "Test Todo" };

    // Act
    var response = await Client.PostAsJsonAsync("/api/todos", newItem);

    // Assert - Verificar c�digo de estado
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

    // Verificar header Location
 Assert.NotNull(response.Headers.Location);
    Assert.Contains("/api/todos/", response.Headers.Location.ToString());

    // Verificar Content-Type
    Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

    // Verificar cuerpo de respuesta
    var createdItem = await response.Content.ReadFromJsonAsync<TodoItem>();
    Assert.NotNull(createdItem);
    Assert.Equal(newItem.Title, createdItem.Title);
    Assert.NotEqual(0, createdItem.Id);
    Assert.False(createdItem.IsComplete);
}
```

### Paso 5: Limpiar Datos de Prueba (Opcional)

```csharp
[Fact]
public async Task Test_ConLimpieza()
{
  // Arrange & Act - Crear item de prueba
    var newItem = new TodoItem { Title = "Test" };
    var createResponse = await Client.PostAsJsonAsync("/api/todos", newItem);
    var createdItem = await createResponse.Content.ReadFromJsonAsync<TodoItem>();

    try
    {
        // Realizar pruebas...
        Assert.NotNull(createdItem);
    }
finally
    {
        // Cleanup - Eliminar item de prueba
        await Client.DeleteAsync($"/api/todos/{createdItem.Id}");
    }
}
```

---

## ? Checklist de Cobertura

### Endpoint: GET /api/todos
- [x] Devuelve OK con colecci�n (vac�a o con datos)
- [ ] Devuelve Content-Type correcto
- [ ] Respeta paginaci�n (si aplica)

### Endpoint: GET /api/todos/{id}
- [x] ID existente devuelve OK con item
- [x] ID inexistente devuelve NotFound (404)
- [ ] ID inv�lido devuelve BadRequest (si aplica)

### Endpoint: POST /api/todos
- [x] Datos v�lidos devuelve Created (201) con item y Location header
- [ ] T�tulo vac�o devuelve BadRequest (400)
- [ ] T�tulo null devuelve BadRequest (400)
- [ ] Datos duplicados (si aplica validaci�n)

### Endpoint: PUT /api/todos/{id}
- [x] ID existente actualiza y devuelve OK o NoContent
- [x] ID inexistente devuelve NotFound (404)
- [ ] Datos inv�lidos devuelve BadRequest (400)
- [ ] ID en URL diferente al ID en body (si aplica)

### Endpoint: DELETE /api/todos/{id}
- [x] ID existente elimina y devuelve NoContent (204)
- [x] ID inexistente devuelve NotFound (404)
- [x] Verificar que item eliminado ya no existe

### Flujos Completos
- [x] CRUD completo: Crear ? Actualizar ? Eliminar ? Verificar
- [ ] M�ltiples items: Crear varios ? Listar ? Eliminar todos
- [ ] Operaciones concurrentes (si aplica)

---

## ?? Plantilla para Nuevos Tests

### Test de Endpoint Individual

```csharp
[Fact]
public async Task Metodo_Condicion_ResultadoEsperado()
{
    // Arrange - Preparar datos si es necesario
    var data = new TodoItem { Title = "Test" };

    // Act - Realizar petici�n HTTP
    var response = await Client.PostAsJsonAsync("/api/endpoint", data);

    // Assert - Verificar respuesta
    response.EnsureSuccessStatusCode();
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

    var result = await response.Content.ReadFromJsonAsync<TodoItem>();
    Assert.NotNull(result);
}
```

### Test de Flujo Completo

```csharp
[Fact]
public async Task FlujoCompleto_Descripcion()
{
    // Paso 1: Operaci�n inicial
    var step1Response = await Client.PostAsJsonAsync("/api/todos", new TodoItem { Title = "Step 1" });
    step1Response.EnsureSuccessStatusCode();
    var item = await step1Response.Content.ReadFromJsonAsync<TodoItem>();

    // Paso 2: Operaci�n intermedia
    var step2Response = await Client.PutAsJsonAsync($"/api/todos/{item.Id}", new TodoItem { Title = "Updated" });
    step2Response.EnsureSuccessStatusCode();

    // Paso 3: Verificaci�n final
    var step3Response = await Client.GetAsync($"/api/todos/{item.Id}");
    step3Response.EnsureSuccessStatusCode();
    var updatedItem = await step3Response.Content.ReadFromJsonAsync<TodoItem>();
    Assert.Equal("Updated", updatedItem.Title);

    // Cleanup
    await Client.DeleteAsync($"/api/todos/{item.Id}");
}
```

---

## ??? Herramientas y Comandos

### Ejecutar Tests
- **Todos los tests**: `Ctrl+R, A`
- **Tests del archivo actual**: `Ctrl+R, T`
- **Test bajo el cursor**: `Ctrl+R, Ctrl+T`
- **Ejecutar tests fallidos**: `Ctrl+R, F`

### Ver Resultados
- Abrir **Test Explorer**: `Ctrl+E, T`
- Ver detalles de fallos en **Output** ? **Tests**

### Depurar Tests de Integraci�n
- Poner breakpoint en el test o en el c�digo de la API
- Click derecho en el test ? **Debug Test**
- El servidor de pruebas se levanta autom�ticamente

---

## ?? Configuraci�n del Proyecto

### WebApplicationFactory

```csharp
public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient Client;

    public IntegrationTestBase(WebApplicationFactory<Program> factory)
    {
        Client = factory.CreateClient();
    }
}
```

### Archivo testhost.deps.json

El proyecto incluye una configuraci�n especial en el `.csproj` para copiar el archivo `deps.json`:

```xml
<ItemGroup>
    <None Include="..\..\src\TodoApp.Api\bin\Debug\net8.0\TodoApp.Api.deps.json">
        <Link>testhost.deps.json</Link>
     <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
</ItemGroup>
```

**?? Importante**: Si cambias la ruta del proyecto API, actualiza esta ruta en el `.csproj`.

---

## ?? Recursos Adicionales

- [Integration Tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests)
- [WebApplicationFactory Documentation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1)
- [xUnit Documentation](https://xunit.net/)
- [HttpClient Best Practices](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines)

---

## ?? Est�ndares del Proyecto

- **Framework de pruebas**: xUnit 2.5.3
- **Framework de testing web**: Microsoft.AspNetCore.Mvc.Testing 8.0.16
- **Target Framework**: .NET 8.0
- **Lenguaje**: C# 12.0
- **Convenci�n de nombres**: PascalCase con guiones bajos para separar secciones
- **Cobertura objetivo**: >70% de endpoints
- **Patr�n obligatorio**: AAA (Arrange-Act-Assert) con comentarios
- **Async/Await**: Obligatorio para todas las operaciones HTTP

---

## ?? Notas Importantes

1. **Base de Datos**: Las pruebas de integraci�n pueden usar:
   - Base de datos en memoria (SQLite in-memory, EF Core InMemory)
   - Base de datos de pruebas separada
   - Contenedores Docker (Testcontainers)

2. **Estado Compartido**: Usar `IClassFixture` para compartir el servidor entre tests de la misma clase, pero cada test debe ser independiente en cuanto a datos.

3. **Limpieza de Datos**: Considerar:
   - Limpiar datos al final de cada test
   - Usar transacciones que se revierten
   - Resetear la base de datos entre ejecuciones

4. **Performance**: Las pruebas de integraci�n son m�s lentas que las unitarias. Priorizar:
   - Flujos cr�ticos de negocio
   - Validaciones importantes
   - Casos de error comunes

5. **Aislamiento vs. Realismo**: Las pruebas de integraci�n buscan un balance entre:
   - **Realismo**: Usar componentes reales (controladores, servicios, etc.)
   - **Control**: Mantener el entorno de pruebas predecible
