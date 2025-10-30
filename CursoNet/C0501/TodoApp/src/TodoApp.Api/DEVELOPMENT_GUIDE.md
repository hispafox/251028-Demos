# Guía de Desarrollo - TodoApp.Api

## ?? Descripción del Proyecto

**TodoApp.Api** es una API REST construida con **ASP.NET Core 8.0** que proporciona operaciones CRUD para gestionar tareas (todos). La API sigue principios RESTful y está diseñada para ser simple, mantenible y extensible.

---

## ??? Arquitectura del Proyecto

```
TodoApp.Api/
??? Controllers/        # Endpoints HTTP (API)
?   ??? TodosController.cs
?   ??? WeatherForecastController.cs
??? Models/       # Entidades de dominio
?   ??? TodoItem.cs
??? Services/          # Lógica de negocio
?   ??? ITodoService.cs
?   ??? TodoService.cs
??? Program.cs            # Configuración y startup
??? appsettings.json      # Configuración de la aplicación
```

### Patrón de Arquitectura

Este proyecto sigue una **arquitectura en capas simplificada**:

1. **Controllers** (Capa de Presentación): Maneja las peticiones HTTP
2. **Services** (Capa de Lógica de Negocio): Implementa las reglas de negocio
3. **Models** (Capa de Dominio): Define las entidades del dominio

---

## ?? Convenciones de Código

### 1. Nomenclatura de Archivos y Clases

#### Controladores
```csharp
// Nombre: [Entidad]Controller.cs
// Ubicación: Controllers/
// Ejemplo:
TodosController.cs
UsersController.cs
```

#### Servicios
```csharp
// Interfaz: I[Nombre]Service.cs
// Ubicación: Services/
ITodoService.cs

// Implementación: [Nombre]Service.cs
TodoService.cs
```

#### Modelos
```csharp
// Nombre: [Entidad].cs
// Ubicación: Models/
// Ejemplo:
TodoItem.cs
User.cs
```

### 2. Convenciones de Rutas (Routing)

#### Estructura de Rutas REST

```
[HttpMethod] /api/[controller]/{id}
```

**Ejemplos:**
```csharp
GET    /api/todos          // Obtener todos los items
GET    /api/todos/1        // Obtener item por ID
POST   /api/todos          // Crear nuevo item
PUT    /api/todos/1        // Actualizar item existente
DELETE /api/todos/1        // Eliminar item
```

#### Configuración en Controladores

```csharp
[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    [HttpGet]         // GET /api/todos
    public ActionResult<IEnumerable<TodoItem>> GetAll() { }

    [HttpGet("{id}")]       // GET /api/todos/5
    public ActionResult<TodoItem> GetById(int id) { }

    [HttpPost]       // POST /api/todos
    public ActionResult<TodoItem> Create(TodoItem item) { }

    [HttpPut("{id}")]            // PUT /api/todos/5
    public ActionResult<TodoItem> Update(int id, TodoItem item) { }

    [HttpDelete("{id}")]         // DELETE /api/todos/5
    public ActionResult Delete(int id) { }
}
```

### 3. Códigos de Estado HTTP

Usar códigos de estado HTTP apropiados:

| Código | Método | Situación | Método ASP.NET Core |
|--------|--------|-----------|---------------------|
| 200 OK | GET, PUT | Éxito con contenido | `Ok(data)` |
| 201 Created | POST | Recurso creado | `CreatedAtAction()` |
| 204 No Content | DELETE | Éxito sin contenido | `NoContent()` |
| 400 Bad Request | POST, PUT | Datos inválidos | `BadRequest(message)` |
| 404 Not Found | GET, PUT, DELETE | Recurso no encontrado | `NotFound()` |
| 500 Internal Server Error | ANY | Error del servidor | (automático) |

**Ejemplo de implementación:**

```csharp
[HttpGet("{id}")]
public ActionResult<TodoItem> GetById(int id)
{
    var item = _todoService.GetById(id);
    
    if (item == null)
     return NotFound();  // 404

    return Ok(item);      // 200
}

[HttpPost]
public ActionResult<TodoItem> Create(TodoItem item)
{
    try
    {
        var newItem = _todoService.Add(item);
      return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);  // 201
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);  // 400
    }
}
```

### 4. Inyección de Dependencias

#### Registro de Servicios en `Program.cs`

```csharp
// Singleton - Una instancia para toda la aplicación
builder.Services.AddSingleton<ITodoService, TodoService>();

// Scoped - Una instancia por request HTTP
builder.Services.AddScoped<ITodoService, TodoService>();

// Transient - Nueva instancia cada vez
builder.Services.AddTransient<ITodoService, TodoService>();
```

**¿Cuándo usar cada uno?**

- **Singleton**: Servicios sin estado, cachés, configuración
- **Scoped**: Servicios con contexto de request (EF Core DbContext)
- **Transient**: Servicios ligeros y sin estado compartido

**En TodoApp:**
```csharp
// TodoService usa Singleton porque mantiene estado en memoria
builder.Services.AddSingleton<ITodoService, TodoService>();
```

#### Inyección en Controladores

```csharp
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

 // Constructor injection
    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }
}
```

---

## ?? Patrones de Diseño Implementados

### 1. Dependency Injection (Inyección de Dependencias)

**Propósito**: Desacoplar componentes y facilitar testing.

```csharp
// ? Acoplamiento directo (malo)
public class TodosController
{
    private TodoService _service = new TodoService();  // Acoplado
}

// ? Dependency Injection (bueno)
public class TodosController
{
    private readonly ITodoService _service;
    
    public TodosController(ITodoService service)
    {
 _service = service;  // Inyectado - fácil de testear
    }
}
```

### 2. Repository Pattern (Implícito en Services)

**Propósito**: Abstraer la lógica de acceso a datos.

```csharp
// Service actúa como repository
public interface ITodoService
{
    IEnumerable<TodoItem> GetAll();
    TodoItem? GetById(int id);
    TodoItem Add(TodoItem item);
    TodoItem? Update(int id, TodoItem item);
    bool Delete(int id);
}
```

### 3. Controller Pattern (MVC)

**Propósito**: Separar lógica de presentación de lógica de negocio.

```csharp
// Controller maneja HTTP, Service maneja lógica
[HttpPost]
public ActionResult<TodoItem> Create(TodoItem item)
{
    try
    {
        // Delegamos la lógica al servicio
     var newItem = _todoService.Add(item);
      return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }
    catch (ArgumentException ex)
    {
return BadRequest(ex.Message);
 }
}
```

---

## ?? Guía de Implementación

### Agregar un Nuevo Endpoint

**Paso 1: Definir el modelo (si es necesario)**

```csharp
// Models/NewModel.cs
namespace TodoApp.Api.Models
{
    public class NewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
```

**Paso 2: Crear la interfaz del servicio**

```csharp
// Services/INewService.cs
namespace TodoApp.Api.Services
{
    public interface INewService
    {
        IEnumerable<NewModel> GetAll();
        NewModel? GetById(int id);
NewModel Add(NewModel item);
        // ... otros métodos
    }
}
```

**Paso 3: Implementar el servicio**

```csharp
// Services/NewService.cs
namespace TodoApp.Api.Services
{
    public class NewService : INewService
    {
    private readonly List<NewModel> _items = new();
        private int _nextId = 1;

        public IEnumerable<NewModel> GetAll() => _items;

     public NewModel? GetById(int id)
        {
  return _items.FirstOrDefault(x => x.Id == id);
        }

        public NewModel Add(NewModel item)
        {
    if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name cannot be empty");

item.Id = _nextId++;
            _items.Add(item);
            return item;
        }
 }
}
```

**Paso 4: Crear el controlador**

```csharp
// Controllers/NewController.cs
[ApiController]
[Route("api/[controller]")]
public class NewController : ControllerBase
{
    private readonly INewService _service;

  public NewController(INewService service)
    {
      _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<NewModel>> GetAll()
    {
    return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<NewModel> GetById(int id)
    {
   var item = _service.GetById(id);
        return item == null ? NotFound() : Ok(item);
    }

 [HttpPost]
    public ActionResult<NewModel> Create(NewModel item)
    {
        try
        {
            var newItem = _service.Add(item);
   return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
   }
        catch (ArgumentException ex)
        {
  return BadRequest(ex.Message);
      }
    }
}
```

**Paso 5: Registrar el servicio en `Program.cs`**

```csharp
// Program.cs
builder.Services.AddSingleton<INewService, NewService>();
```

---

## ? Mejores Prácticas

### 1. Manejo de Errores

#### ? Validación en Servicios

```csharp
public TodoItem Add(TodoItem item)
{
    // Validar argumentos
    if (item == null)
     throw new ArgumentNullException(nameof(item));

    if (string.IsNullOrWhiteSpace(item.Title))
        throw new ArgumentException("El título no puede estar vacío", nameof(item.Title));

    // Lógica de negocio...
}
```

#### ? Manejo en Controladores

```csharp
[HttpPost]
public ActionResult<TodoItem> Create(TodoItem item)
{
    try
    {
        var newItem = _todoService.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }
 catch (ArgumentNullException)
    {
        return BadRequest("Item cannot be null");
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
  catch (Exception ex)
    {
        // Log el error
        return StatusCode(500, "Internal server error");
    }
}
```

### 2. Uso de ActionResult

```csharp
// ? Usar ActionResult<T> para documentación automática
[HttpGet("{id}")]
public ActionResult<TodoItem> GetById(int id)
{
  var item = _todoService.GetById(id);
  return item == null ? NotFound() : Ok(item);
}

// ? Evitar IActionResult sin tipo
[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var item = _todoService.GetById(id);
    return item == null ? NotFound() : Ok(item);
}
```

### 3. Validación de Modelos

#### Usar Data Annotations

```csharp
using System.ComponentModel.DataAnnotations;

public class TodoItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters")]
    public string Title { get; set; } = string.Empty;

    public bool IsComplete { get; set; }
}
```

#### Validación Automática en Controladores

```csharp
[HttpPost]
public ActionResult<TodoItem> Create([FromBody] TodoItem item)
{
    // [ApiController] valida automáticamente ModelState
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // ...
}
```

### 4. Documentación con XML Comments

```csharp
/// <summary>
/// Obtiene todos los items de tareas.
/// </summary>
/// <returns>Una colección de items de tareas.</returns>
/// <response code="200">Devuelve la lista de items</response>
[HttpGet]
[ProducesResponseType(typeof(IEnumerable<TodoItem>), StatusCodes.Status200OK)]
public ActionResult<IEnumerable<TodoItem>> GetAll()
{
    return Ok(_todoService.GetAll());
}

/// <summary>
/// Obtiene un item específico por ID.
/// </summary>
/// <param name="id">ID del item a buscar</param>
/// <returns>El item solicitado</returns>
/// <response code="200">Item encontrado</response>
/// <response code="404">Item no encontrado</response>
[HttpGet("{id}")]
[ProducesResponseType(typeof(TodoItem), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public ActionResult<TodoItem> GetById(int id)
{
    var item = _todoService.GetById(id);
    return item == null ? NotFound() : Ok(item);
}
```

### 5. Asynchronous Programming

Para operaciones que involucran I/O (base de datos, APIs externas):

```csharp
// Interfaz asíncrona
public interface ITodoService
{
  Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
 Task<TodoItem> AddAsync(TodoItem item);
    Task<TodoItem?> UpdateAsync(int id, TodoItem item);
    Task<bool> DeleteAsync(int id);
}

// Implementación asíncrona
public class TodoService : ITodoService
{
    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
     // Simulación de operación asíncrona
   await Task.Delay(10);
      return _todos;
    }
}

// Controlador asíncrono
[HttpGet]
public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
{
    var items = await _todoService.GetAllAsync();
  return Ok(items);
}
```

---

## ?? Configuración y Middleware

### Program.cs - Pipeline de Middleware

```csharp
var builder = WebApplication.CreateBuilder(args);

// ===== CONFIGURACIÓN DE SERVICIOS =====
builder.Services.AddControllers();
builder.Services.AddSingleton<ITodoService, TodoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (si es necesario)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
  {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
             .AllowAnyHeader();
    });
});

var app = builder.Build();

// ===== CONFIGURACIÓN DEL PIPELINE HTTP =====
// Orden importante: los middleware se ejecutan en orden

// 1. Swagger (solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. HTTPS Redirection
app.UseHttpsRedirection();

// 3. CORS (si se configuró)
// app.UseCors("AllowAll");

// 4. Authentication (si existe)
// app.UseAuthentication();

// 5. Authorization
app.UseAuthorization();

// 6. Mapear controladores
app.MapControllers();

// 7. Ejecutar la aplicación
app.Run();
```

### Orden del Pipeline (Importante)

```
Request
   ?
1. Swagger
   ?
2. HTTPS Redirection
   ?
3. CORS
   ?
4. Authentication
   ?
5. Authorization
   ?
6. Controllers
   ?
Response
```

---

## ?? Comandos Esenciales

### Desarrollo

```bash
# Restaurar dependencias
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar la aplicación
dotnet run

# Ejecutar con hot reload (recarga automática)
dotnet watch run

# Ejecutar en un puerto específico
dotnet run --urls "https://localhost:5001;http://localhost:5000"
```

### Producción

```bash
# Build de release
dotnet build -c Release

# Publicar la aplicación
dotnet publish -c Release -o ./publish

# Ejecutar la versión publicada
dotnet ./publish/TodoApp.Api.dll
```

### Gestión de Paquetes

```bash
# Agregar un paquete NuGet
dotnet add package [PackageName]

# Ejemplos:
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Serilog.AspNetCore
dotnet add package FluentValidation.AspNetCore

# Listar paquetes instalados
dotnet list package

# Actualizar paquetes
dotnet add package [PackageName] --version [NewVersion]
```

---

## ?? Estructura de Respuestas

### Respuestas Exitosas

#### GET - Lista de items
```json
// GET /api/todos
// Status: 200 OK
[
  {
    "id": 1,
    "title": "Completar informe",
    "isComplete": false
  },
  {
    "id": 2,
    "title": "Revisar emails",
    "isComplete": true
  }
]
```

#### GET - Item individual
```json
// GET /api/todos/1
// Status: 200 OK
{
  "id": 1,
  "title": "Completar informe",
  "isComplete": false
}
```

#### POST - Crear item
```json
// Request:
// POST /api/todos
{
  "title": "Nueva tarea",
  "isComplete": false
}

// Response:
// Status: 201 Created
// Location: /api/todos/3
{
  "id": 3,
  "title": "Nueva tarea",
  "isComplete": false
}
```

#### PUT - Actualizar item
```json
// Request:
// PUT /api/todos/1
{
  "title": "Tarea actualizada",
  "isComplete": true
}

// Response:
// Status: 200 OK
{
  "id": 1,
  "title": "Tarea actualizada",
  "isComplete": true
}
```

#### DELETE - Eliminar item
```
// DELETE /api/todos/1
// Status: 204 No Content
(sin contenido en el cuerpo)
```

### Respuestas de Error

#### 400 Bad Request
```json
{
  "message": "El título no puede estar vacío"
}
```

#### 404 Not Found
```
(sin contenido en el cuerpo)
```

---

## ?? Testing de la API

### Usar Swagger UI

1. Ejecutar la aplicación: `dotnet run`
2. Navegar a: `https://localhost:[puerto]/swagger`
3. Probar endpoints directamente desde la interfaz

### Usar curl

```bash
# GET - Listar todos
curl -X GET https://localhost:5001/api/todos

# GET - Obtener por ID
curl -X GET https://localhost:5001/api/todos/1

# POST - Crear
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d '{"title":"Nueva tarea","isComplete":false}'

# PUT - Actualizar
curl -X PUT https://localhost:5001/api/todos/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"Tarea actualizada","isComplete":true}'

# DELETE - Eliminar
curl -X DELETE https://localhost:5001/api/todos/1
```

### Usar Postman / Insomnia

Importar colección con los siguientes endpoints:

```
GET https://localhost:5001/api/todos
GET    https://localhost:5001/api/todos/{id}
POST   https://localhost:5001/api/todos
PUT    https://localhost:5001/api/todos/{id}
DELETE https://localhost:5001/api/todos/{id}
```

---

## ?? Checklist de Feature Completa

Al implementar una nueva feature, verificar:

- [ ] **Modelo**
  - [ ] Propiedades con tipos apropiados
  - [ ] Data Annotations para validación
  - [ ] Nullable reference types configurados
  - [ ] XML comments para documentación

- [ ] **Servicio**
  - [ ] Interfaz creada (`IXxxService`)
  - [ ] Implementación completa
  - [ ] Validaciones de negocio
  - [ ] Manejo de excepciones apropiado
  - [ ] XML comments en interfaz

- [ ] **Controlador**
  - [ ] Rutas RESTful configuradas
  - [ ] Métodos HTTP apropiados
- [ ] Códigos de estado correctos
  - [ ] Manejo de errores (try-catch)
  - [ ] XML comments y ProducesResponseType
  - [ ] Dependency injection configurada

- [ ] **Registro**
  - [ ] Servicio registrado en `Program.cs`
  - [ ] Lifetime apropiado (Singleton/Scoped/Transient)

- [ ] **Testing**
  - [ ] Unit tests para el servicio
  - [ ] Integration tests para el controlador
  - [ ] Casos de error cubiertos

- [ ] **Documentación**
  - [ ] Swagger actualizado
  - [ ] README actualizado (si aplica)
  - [ ] Ejemplos de uso documentados

---

## ?? Recursos Adicionales

### Documentación Oficial

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [ASP.NET Core Web API Tutorial](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)
- [Dependency Injection in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [Routing in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing)
- [Model Validation in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation)

### Mejores Prácticas

- [REST API Best Practices](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design)
- [ASP.NET Core Performance Best Practices](https://learn.microsoft.com/en-us/aspnet/core/performance/performance-best-practices)
- [Security in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/)

---

## ?? Problemas Comunes y Soluciones

### Error 1: "No se puede acceder a la API desde el navegador"

**Problema**: CORS no configurado

**Solución**:
```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
             .AllowAnyMethod()
          .AllowAnyHeader();
    });
});

// ...

app.UseCors("AllowAll");
```

### Error 2: "Servicio no resuelto"

**Problema**: Servicio no registrado en DI

**Solución**:
```csharp
// Verificar en Program.cs
builder.Services.AddSingleton<ITodoService, TodoService>();
```

### Error 3: "Swagger no muestra documentación"

**Problema**: XML comments no habilitados

**Solución**:
```xml
<!-- TodoApp.Api.csproj -->
<PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

### Error 4: "Validación de modelo no funciona"

**Problema**: Falta `[ApiController]` attribute

**Solución**:
```csharp
[ApiController]  // ? Necesario para validación automática
[Route("api/[controller]")]
public class TodosController : ControllerBase
```

---

## ?? Estructura del Proyecto TodoApp

```
TodoApp/
??? src/
?   ??? TodoApp.Api/              ? Este proyecto
?   ??? Controllers/
?   ??? Models/
?       ??? Services/
?       ??? Program.cs
?       ??? DEVELOPMENT_GUIDE.md  ? Esta guía
??? tests/
    ??? TodoApp.UnitTests/        ? Unit Tests
    ??? TodoApp.IntegrationTests/ ? Integration Tests
    ??? TodoApp.E2ETests/     ? E2E Tests
```

---

**Versión:** 1.0  
**Última actualización:** 2024  
**Target Framework:** .NET 8.0  
**Tipo de Proyecto:** ASP.NET Core Web API
