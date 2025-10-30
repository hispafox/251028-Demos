# Instrucciones para Pruebas End-to-End (E2E) - TodoApp

## ?? Descripción del Proyecto

Este proyecto contiene pruebas End-to-End (E2E) para la aplicación TodoApp, utilizando **xUnit**, **Microsoft.AspNetCore.Mvc.Testing** y opcionalmente **Selenium WebDriver** para pruebas de interfaz de usuario completas.

---

## ??? Convenciones de Nomenclatura

### Nombres de Métodos de Prueba

```
Escenario[Completo/Especifico]_[DescripcionDelFlujo]
```

**Ejemplos:**
- `EscenarioCompleto_GestionDeTareas`
- `EscenarioCompleto_UsuarioCreaTareasYLasCompleta`
- `EscenarioCompleto_FlujoHappyPathCRUD`
- `Escenario_CreacionYEliminacionMultipleTareas`

### Nombres de Clases de Prueba

```
[Componente]E2ETests
```

**Ejemplos:**
- `TodoE2ETests` - Pruebas E2E para la funcionalidad Todo completa

---

## ?? Estructura de Escenarios E2E

Las pruebas E2E deben simular flujos de usuario completos y realistas:

```csharp
[Fact]
public async Task EscenarioCompleto_DescripcionDelFlujo()
{
    // PASO 1: Estado inicial - Verificar precondiciones
    // ...

    // PASO 2: Acción del usuario 1
    // ...

    // PASO 3: Verificación intermedia
    // ...

    // PASO 4: Acción del usuario 2
    // ...

    // PASO 5: Verificación final
    // ...

    // PASO N: Cleanup (limpieza)
    // ...
}
```

---

## ?? Configuración del Entorno E2E

### Configuración Básica (API Testing)

```csharp
public class TodoE2ETests : IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

  public TodoE2ETests()
    {
        // Arrange - Configurar factory y cliente para todas las pruebas
  _factory = new WebApplicationFactory<Program>();
  _client = _factory.CreateClient();
    }

    public void Dispose()
    {
        // Cleanup - Liberar recursos
        _client.Dispose();
        _factory.Dispose();
    }
}
```

### Configuración con Selenium (UI Testing)

```csharp
public class TodoUIE2ETests : IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly IWebDriver _driver;
    private readonly string _baseUrl;

    public TodoUIE2ETests()
    {
  // Configurar servidor de pruebas
     _factory = new WebApplicationFactory<Program>()
          .WithWebHostBuilder(builder =>
    {
             builder.UseUrls("http://localhost:5555");
 });

        // Iniciar aplicación
     var server = _factory.Server;
        _baseUrl = "http://localhost:5555";

    // Configurar Selenium WebDriver
        var options = new ChromeOptions();
        options.AddArgument("--headless"); // Ejecutar sin UI
        options.AddArgument("--no-sandbox");
        _driver = new ChromeDriver(options);
    }

    public void Dispose()
    {
  _driver.Quit();
        _driver.Dispose();
        _factory.Dispose();
    }
}
```

---

## ?? Tipos de Pruebas E2E

### 1. Pruebas de Escenarios Completos (API)

**Objetivo:** Validar flujos de usuario completos desde el inicio hasta el fin.

**Características:**
- Simular secuencias realistas de operaciones
- Verificar estado en múltiples puntos del flujo
- Validar consistencia de datos a lo largo del tiempo
- Incluir limpieza de datos al final

**Ejemplo:**

```csharp
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
```

### 2. Pruebas de Flujos de Usuario (UI con Selenium)

**Objetivo:** Validar la interfaz de usuario completa incluyendo navegación e interacciones.

**Características:**
- Automatizar clicks, inputs y navegación
- Verificar elementos visibles en la página
- Validar mensajes al usuario
- Probar flujos completos desde la UI

**Ejemplo:**

```csharp
[Fact]
public void EscenarioCompleto_UsuarioCreaYCompletaTarea()
{
    // PASO 1: Navegar a la página principal
    _driver.Navigate().GoToUrl($"{_baseUrl}/todos");

    // PASO 2: Verificar que la página cargó correctamente
    Assert.Contains("Todo App", _driver.Title);

    // PASO 3: Crear nueva tarea
    var inputField = _driver.FindElement(By.Id("new-todo-input"));
    inputField.SendKeys("Tarea de prueba E2E");
    var submitButton = _driver.FindElement(By.Id("submit-todo"));
    submitButton.Click();

    // PASO 4: Esperar y verificar que la tarea aparece en la lista
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
    wait.Until(d => d.FindElements(By.ClassName("todo-item")).Count > 0);

    var todoItems = _driver.FindElements(By.ClassName("todo-item"));
    Assert.Contains(todoItems, item => item.Text.Contains("Tarea de prueba E2E"));

    // PASO 5: Marcar tarea como completada
    var checkbox = _driver.FindElement(By.CssSelector(".todo-item input[type='checkbox']"));
    checkbox.Click();

  // PASO 6: Verificar que la tarea está marcada como completada
    wait.Until(d => d.FindElement(By.CssSelector(".todo-item.completed")) != null);
    var completedTask = _driver.FindElement(By.CssSelector(".todo-item.completed"));
    Assert.NotNull(completedTask);

    // PASO 7: Eliminar la tarea
    var deleteButton = _driver.FindElement(By.CssSelector(".todo-item .delete-button"));
    deleteButton.Click();

    // PASO 8: Verificar que la tarea fue eliminada
    wait.Until(d => d.FindElements(By.ClassName("todo-item")).Count == 0);
}
```

### 3. Pruebas de Casos de Error en Escenarios

**Objetivo:** Validar que el sistema maneja errores correctamente en flujos completos.

**Características:**
- Simular errores en medio de un flujo
- Verificar recuperación del sistema
- Validar mensajes de error al usuario

**Ejemplo:**

```csharp
[Fact]
public async Task Escenario_ErrorEnMedioDeFlujo_SistemaSeRecupera()
{
    // PASO 1: Crear tarea válida
    var task = new TodoItem { Title = "Tarea válida" };
    var createResponse = await _client.PostAsJsonAsync("/api/todos", task);
    createResponse.EnsureSuccessStatusCode();
    var createdTask = await createResponse.Content.ReadFromJsonAsync<TodoItem>();

    // PASO 2: Intentar actualizar con datos inválidos
    var invalidUpdate = new TodoItem { Title = "" }; // Título vacío
    var updateResponse = await _client.PutAsJsonAsync($"/api/todos/{createdTask.Id}", invalidUpdate);
    Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);

    // PASO 3: Verificar que la tarea original no cambió
    var getResponse = await _client.GetAsync($"/api/todos/{createdTask.Id}");
    getResponse.EnsureSuccessStatusCode();
    var unchangedTask = await getResponse.Content.ReadFromJsonAsync<TodoItem>();
    Assert.Equal("Tarea válida", unchangedTask.Title);

    // PASO 4: Actualizar con datos válidos debe funcionar
    var validUpdate = new TodoItem { Title = "Tarea actualizada correctamente" };
    var validUpdateResponse = await _client.PutAsJsonAsync($"/api/todos/{createdTask.Id}", validUpdate);
    validUpdateResponse.EnsureSuccessStatusCode();

    // PASO 5: Cleanup
    await _client.DeleteAsync($"/api/todos/{createdTask.Id}");
}
```

### 4. Pruebas de Rendimiento y Carga (Load Testing)

**Objetivo:** Validar el comportamiento del sistema bajo carga.

**Características:**
- Crear múltiples items simultáneamente
- Validar tiempos de respuesta
- Verificar que no hay pérdida de datos

**Ejemplo:**

```csharp
[Fact]
public async Task Escenario_CargaMultipleTareas_SistemaMantieneDatos()
{
    // PASO 1: Crear múltiples tareas en paralelo
    var tasks = Enumerable.Range(1, 50)
        .Select(i => new TodoItem { Title = $"Tarea {i}" })
     .Select(item => _client.PostAsJsonAsync("/api/todos", item))
        .ToArray();

    var responses = await Task.WhenAll(tasks);

    // PASO 2: Verificar que todas las creaciones fueron exitosas
    Assert.All(responses, response =>
    {
        Assert.True(response.IsSuccessStatusCode);
    });

    var createdItems = await Task.WhenAll(
        responses.Select(r => r.Content.ReadFromJsonAsync<TodoItem>())
    );

    // PASO 3: Verificar que todas las tareas están en el sistema
    var listResponse = await _client.GetAsync("/api/todos");
    listResponse.EnsureSuccessStatusCode();
    var allItems = await listResponse.Content.ReadFromJsonAsync<TodoItem[]>();

    Assert.True(allItems.Length >= 50);

    // PASO 4: Cleanup - Eliminar todas las tareas creadas
    var deleteTasks = createdItems
        .Select(item => _client.DeleteAsync($"/api/todos/{item.Id}"))
     .ToArray();

    await Task.WhenAll(deleteTasks);
}
```

---

## ?? Plan de Generación de Tests E2E Completos

### Paso 1: Identificar Escenarios de Usuario

**Escenarios Principales:**
1. Usuario nuevo crea su primera tarea
2. Usuario crea múltiples tareas y las gestiona
3. Usuario completa tareas en orden
4. Usuario elimina tareas completadas
5. Usuario actualiza tareas existentes

**Escenarios de Error:**
1. Usuario intenta crear tarea sin título
2. Usuario intenta acceder a tarea inexistente
3. Sistema se recupera de errores de validación

### Paso 2: Diseñar Flujos Completos

Cada escenario debe incluir:
- **Estado inicial**: Verificar precondiciones
- **Acciones del usuario**: Secuencia de operaciones
- **Verificaciones intermedias**: Validar estado después de cada acción
- **Estado final**: Verificar postcondiciones
- **Cleanup**: Limpiar datos de prueba

### Paso 3: Implementar con Pasos Numerados

```csharp
[Fact]
public async Task EscenarioCompleto_NombreDescriptivo()
{
    // PASO 1: [Descripción]
    // ...código...

    // PASO 2: [Descripción]
    // ...código...

    // PASO N: Cleanup
    // ...código de limpieza...
}
```

### Paso 4: Agregar Verificaciones en Cada Paso

```csharp
// PASO 3: Verificar que ambas tareas existen
var listResponse = await _client.GetAsync("/api/todos");
listResponse.EnsureSuccessStatusCode();
var allTasks = await listResponse.Content.ReadFromJsonAsync<TodoItem[]>();

// Verificaciones múltiples
Assert.NotNull(allTasks);
Assert.Equal(expectedCount, allTasks.Length);
Assert.Contains(allTasks, t => t.Title == "Tarea 1");
Assert.Contains(allTasks, t => t.Title == "Tarea 2");
```

### Paso 5: Incluir Cleanup Obligatorio

```csharp
// PASO FINAL: Limpiar - eliminar todas las tareas creadas
foreach (var taskId in createdTaskIds)
{
    await _client.DeleteAsync($"/api/todos/{taskId}");
}

// O usando try-finally
try
{
    // ...pruebas...
}
finally
{
    // Cleanup garantizado
    foreach (var taskId in createdTaskIds)
    {
        await _client.DeleteAsync($"/api/todos/{taskId}");
    }
}
```

---

## ? Checklist de Escenarios E2E

### Escenarios Básicos
- [x] Crear ? Listar ? Verificar existencia
- [x] Crear ? Actualizar ? Verificar cambio
- [x] Crear ? Eliminar ? Verificar eliminación
- [x] Crear múltiples ? Listar ? Verificar cantidad

### Escenarios Completos
- [x] CRUD completo: Crear ? Actualizar ? Listar ? Eliminar ? Verificar
- [ ] Gestión de tareas: Crear varias ? Completar algunas ? Eliminar completadas
- [ ] Flujo de usuario real: Estado inicial ? Múltiples acciones ? Estado final

### Escenarios de Error
- [ ] Crear tarea inválida ? Verificar error ? Recuperarse
- [ ] Actualizar tarea inexistente ? Verificar error
- [ ] Error en medio de flujo ? Sistema mantiene consistencia

### Escenarios de Rendimiento
- [ ] Crear múltiples tareas simultáneamente
- [ ] Operaciones concurrentes
- [ ] Sistema bajo carga mantiene datos

---

## ?? Plantilla para Nuevos Tests E2E

### Test E2E Completo (API)

```csharp
[Fact]
public async Task EscenarioCompleto_DescripcionDelFlujo()
{
    var createdIds = new List<int>();

    try
    {
        // PASO 1: Verificar estado inicial
        var initialResponse = await _client.GetAsync("/api/todos");
    initialResponse.EnsureSuccessStatusCode();
        var initialItems = await initialResponse.Content.ReadFromJsonAsync<TodoItem[]>();
 int initialCount = initialItems.Length;

 // PASO 2: Primera acción del usuario
        var item1 = new TodoItem { Title = "Primera tarea" };
        var createResponse1 = await _client.PostAsJsonAsync("/api/todos", item1);
        createResponse1.EnsureSuccessStatusCode();
 var created1 = await createResponse1.Content.ReadFromJsonAsync<TodoItem>();
        createdIds.Add(created1.Id);

        // PASO 3: Verificación intermedia
        var listResponse = await _client.GetAsync("/api/todos");
        var items = await listResponse.Content.ReadFromJsonAsync<TodoItem[]>();
        Assert.Equal(initialCount + 1, items.Length);

        // PASO 4: Segunda acción del usuario
        // ...

    // PASO N: Verificación final
   // ...
    }
    finally
    {
  // CLEANUP: Eliminar todos los items creados
        foreach (var id in createdIds)
        {
            await _client.DeleteAsync($"/api/todos/{id}");
        }
    }
}
```

### Test E2E con Selenium (UI)

```csharp
[Fact]
public void EscenarioCompleto_InteraccionUI()
{
    // PASO 1: Navegar a la página
    _driver.Navigate().GoToUrl($"{_baseUrl}/");

    // PASO 2: Verificar elementos visibles
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    wait.Until(d => d.FindElement(By.Id("main-container")));

    // PASO 3: Interactuar con elementos
    var inputField = _driver.FindElement(By.Id("todo-input"));
    inputField.SendKeys("Nueva tarea");

    var submitButton = _driver.FindElement(By.Id("submit-button"));
    submitButton.Click();

    // PASO 4: Verificar resultado en UI
    wait.Until(d => d.FindElements(By.ClassName("todo-item")).Count > 0);

    var todoItems = _driver.FindElements(By.ClassName("todo-item"));
    Assert.Contains(todoItems, item => item.Text.Contains("Nueva tarea"));

    // PASO 5: Cleanup en UI
    var deleteButton = _driver.FindElement(By.CssSelector(".todo-item .delete-button"));
    deleteButton.Click();
}
```

---

## ??? Herramientas y Comandos

### Ejecutar Tests E2E
- **Todos los tests**: `Ctrl+R, A`
- **Tests del archivo actual**: `Ctrl+R, T`
- **Test específico**: `Ctrl+R, Ctrl+T` (cursor en el test)

### Ver Resultados
- Abrir **Test Explorer**: `Ctrl+E, T`
- Ver salida detallada en **Output** ? **Tests**

### Ejecutar con Selenium
- Instalar ChromeDriver: `dotnet add package Selenium.WebDriver.ChromeDriver`
- Ejecutar en modo headless (sin ventana): `options.AddArgument("--headless")`
- Ejecutar con ventana visible: Comentar la línea de `--headless`

---

## ?? Configuración de Dependencias

### Paquetes Necesarios

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.5.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.16" />
    
    <!-- Opcional: Para pruebas de UI -->
  <PackageReference Include="Selenium.WebDriver" Version="4.32.0" />
    <PackageReference Include="Selenium.Support" Version="4.32.0" />
</ItemGroup>
```

---

## ?? Recursos Adicionales

- [End-to-End Testing in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests)
- [Selenium WebDriver Documentation](https://www.selenium.dev/documentation/webdriver/)
- [WebApplicationFactory Documentation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1)
- [xUnit Documentation](https://xunit.net/)

---

## ?? Estándares del Proyecto

- **Framework de pruebas**: xUnit 2.5.3
- **Framework de testing web**: Microsoft.AspNetCore.Mvc.Testing 8.0.16
- **Framework de UI testing**: Selenium WebDriver 4.32.0 (opcional)
- **Target Framework**: .NET 8.0
- **Lenguaje**: C# 12.0
- **Convención de nombres**: `EscenarioCompleto_DescripcionDelFlujo`
- **Cobertura objetivo**: Escenarios críticos de usuario (>5 escenarios principales)
- **Estructura obligatoria**: Pasos numerados con descripciones claras
- **Async/Await**: Obligatorio para operaciones HTTP
- **Cleanup**: Obligatorio al final de cada escenario

---

## ?? Notas Importantes

1. **Diferencia con Integration Tests**:
   - **Integration**: Prueban componentes específicos (controladores, servicios)
   - **E2E**: Prueban flujos completos de usuario desde inicio a fin

2. **Estado del Sistema**:
   - Verificar estado inicial antes de cada escenario
- Limpiar datos al final (cleanup obligatorio)
   - Cada escenario debe ser independiente

3. **Datos de Prueba**:
   - Usar datos realistas que simulen usuarios reales
   - Evitar hardcodear IDs o valores específicos
   - Generar datos dinámicamente cuando sea posible

4. **Performance**:
   - Las pruebas E2E son las más lentas
   - Priorizar escenarios críticos de negocio
   - Considerar ejecutar E2E en pipeline de CI/CD separado

5. **Debugging**:
   - Usar pasos numerados facilita identificar dónde falla
   - Agregar logs o screenshots en caso de fallos (con Selenium)
   - Verificar estado en cada paso para localizar problemas

6. **Selenium Considerations** (si se usa):
   - Configurar WebDriver correctamente (ChromeDriver, GeckoDriver, etc.)
   - Usar esperas explícitas (`WebDriverWait`) en lugar de `Thread.Sleep`
   - Ejecutar en modo headless para CI/CD
   - Cerrar y liberar recursos con `Dispose`

7. **Manejo de Tiempo**:
   - Agregar timeouts apropiados
   - Usar `WebDriverWait` para esperar elementos
   - No usar `Thread.Sleep` (puede causar flakiness)
