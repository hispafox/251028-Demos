# Product Requirements Document (PRD)
## TodoApp - API RESTful para Gestión de Tareas

---

## ?? Tabla de Contenidos
1. [Información General](#información-general)
2. [Objetivos del Proyecto](#objetivos-del-proyecto)
3. [Alcance del Proyecto](#alcance-del-proyecto)
4. [Arquitectura de la Solución](#arquitectura-de-la-solución)
5. [Especificaciones Técnicas](#especificaciones-técnicas)
6. [Funcionalidades](#funcionalidades)
7. [Modelo de Datos](#modelo-de-datos)
8. [API Endpoints](#api-endpoints)
9. [Estrategia de Pruebas](#estrategia-de-pruebas)
10. [Dependencias y Paquetes](#dependencias-y-paquetes)
11. [Configuración y Despliegue](#configuración-y-despliegue)
12. [Mejoras Futuras](#mejoras-futuras)

---

## ?? Información General

### Nombre del Proyecto
**TodoApp - API RESTful para Gestión de Tareas**

### Versión
1.0.0

### Fecha de Creación
2024

### Propósito
Desarrollar una API RESTful completa para la gestión de tareas (To-Do List) implementando las mejores prácticas de desarrollo en .NET 8, incluyendo una estrategia de pruebas exhaustiva con pruebas unitarias, de integración y end-to-end.

### Stakeholders
- Equipo de Desarrollo
- Estudiantes/Aprendices de .NET
- Arquitectos de Software

---

## ?? Objetivos del Proyecto

### Objetivos Principales
1. **Demostrar las mejores prácticas de desarrollo en .NET 8**
   - Implementación de arquitectura limpia
   - Separación de responsabilidades
   - Inyección de dependencias
 - Principios SOLID

2. **Implementar una estrategia de pruebas completa**
- Pruebas unitarias para lógica de negocio
   - Pruebas de integración para endpoints
   - Pruebas end-to-end para flujos completos

3. **Crear una API RESTful funcional**
   - CRUD completo de tareas
   - Validación de datos
   - Manejo adecuado de errores
   - Documentación con Swagger/OpenAPI

### Objetivos Secundarios
- Servir como material educativo para desarrollo en .NET
- Proporcionar una base sólida para proyectos similares
- Demostrar patrones de diseño comunes en aplicaciones web

---

## ?? Alcance del Proyecto

### Incluido en el Alcance
? API RESTful con operaciones CRUD completas  
? Validación de datos de entrada  
? Manejo de errores y excepciones  
? Documentación automática con Swagger  
? Pruebas unitarias con cobertura de servicios y controladores  
? Pruebas de integración con WebApplicationFactory  
? Pruebas end-to-end simulando escenarios reales  
? Inyección de dependencias configurada  
? Arquitectura por capas (Controllers, Services, Models)  

### Fuera del Alcance
? Persistencia en base de datos (se usa almacenamiento en memoria)  
? Autenticación y autorización  
? Frontend/Cliente web  
? Paginación avanzada  
? Filtros y búsqueda complejos  
? Logging avanzado  
? Caché distribuido  
? Internacionalización (i18n)  

---

## ??? Arquitectura de la Solución

### Estructura General
```
TodoApp/
??? src/
?   ??? TodoApp.Api/  # Proyecto principal de la API
?    ??? Controllers/      # Controladores REST
?       ??? Services/        # Lógica de negocio
?       ??? Models/     # Modelos de datos
?       ??? Program.cs            # Punto de entrada
?
??? tests/
    ??? TodoApp.UnitTests/        # Pruebas unitarias
    ??? TodoApp.IntegrationTests/ # Pruebas de integración
    ??? TodoApp.E2ETests/         # Pruebas end-to-end
```

### Patrones de Diseño Implementados

#### 1. **Dependency Injection (DI)**
- Los servicios se registran en el contenedor de DI
- Los controladores reciben dependencias a través del constructor
- Facilita el testing con mocks

#### 2. **Repository Pattern (Simplificado)**
- `ITodoService` actúa como interfaz de repositorio
- `TodoService` implementa la lógica de acceso a datos
- Desacopla la lógica de negocio del almacenamiento

#### 3. **RESTful API Pattern**
- URIs claras y semánticas
- Uso correcto de métodos HTTP (GET, POST, PUT, DELETE)
- Códigos de estado HTTP apropiados
- Content negotiation (JSON)

#### 4. **Controller-Service Pattern**
- Controladores delgados que delegan la lógica al servicio
- Servicios contienen la lógica de negocio
- Separación clara de responsabilidades

---

## ?? Especificaciones Técnicas

### Stack Tecnológico

#### Framework y Versión
- **.NET 8.0** (LTS - Long Term Support)
- **ASP.NET Core Web API**

#### Lenguaje
- **C# 12** con características modernas

#### Características Habilitadas
- Nullable reference types (`<Nullable>enable</Nullable>`)
- Implicit usings (`<ImplicitUsings>enable</ImplicitUsings>`)
- Preserve compilation context para testing

### Requisitos del Sistema
- **.NET 8 SDK** instalado
- **Visual Studio 2022** (17.8+) o **VS Code** con C# DevKit
- **Windows, macOS o Linux**

---

## ?? Funcionalidades

### Funcionalidades Principales

#### 1. **Gestión de Tareas**

##### Crear Tarea
- **Descripción**: Permite crear una nueva tarea
- **Validaciones**:
  - El título no puede estar vacío
  - El título no puede ser solo espacios en blanco
  - El objeto no puede ser nulo
- **Comportamiento**:
  - Se asigna un ID único automáticamente
  - Se inicializa con `IsComplete = false` por defecto

##### Listar Tareas
- **Descripción**: Obtiene todas las tareas existentes
- **Comportamiento**:
  - Retorna una colección vacía si no hay tareas
  - Retorna todas las tareas sin filtros

##### Obtener Tarea por ID
- **Descripción**: Obtiene una tarea específica por su ID
- **Comportamiento**:
  - Retorna la tarea si existe
  - Retorna `null` si no existe

##### Actualizar Tarea
- **Descripción**: Actualiza los datos de una tarea existente
- **Validaciones**:
  - El ID debe existir
  - El título no puede estar vacío
  - El objeto no puede ser nulo
- **Comportamiento**:
  - Actualiza título y estado de completado
  - Retorna la tarea actualizada

##### Eliminar Tarea
- **Descripción**: Elimina una tarea existente
- **Comportamiento**:
  - Retorna `true` si se eliminó correctamente
  - Retorna `false` si la tarea no existe

---

## ?? Modelo de Datos

### TodoItem

```csharp
public class TodoItem
{
    public int Id { get; set; }// Identificador único
    public string Title { get; set; }    // Título de la tarea
    public bool IsComplete { get; set; }      // Estado de completado
}
```

#### Propiedades

| Propiedad | Tipo | Descripción | Validaciones |
|-----------|------|-------------|--------------|
| `Id` | `int` | Identificador único asignado automáticamente | Generado por el sistema |
| `Title` | `string` | Título descriptivo de la tarea | No puede estar vacío o ser solo espacios |
| `IsComplete` | `bool` | Indica si la tarea está completada | Por defecto `false` |

#### Ejemplo JSON
```json
{
  "id": 1,
  "title": "Completar documentación",
  "isComplete": false
}
```

---

## ?? API Endpoints

### Base URL
```
https://localhost:5001/api/todos
```

### Endpoints Disponibles

#### 1. **GET /api/todos**
Obtiene todas las tareas

**Request:**
```http
GET /api/todos HTTP/1.1
Host: localhost:5001
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
  "title": "Completar informe",
    "isComplete": false
  },
  {
    "id": 2,
    "title": "Revisar código",
    "isComplete": true
  }
]
```

---

#### 2. **GET /api/todos/{id}**
Obtiene una tarea específica por ID

**Request:**
```http
GET /api/todos/1 HTTP/1.1
Host: localhost:5001
```

**Response (200 OK):**
```json
{
  "id": 1,
  "title": "Completar informe",
  "isComplete": false
}
```

**Response (404 Not Found):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

---

#### 3. **POST /api/todos**
Crea una nueva tarea

**Request:**
```http
POST /api/todos HTTP/1.1
Host: localhost:5001
Content-Type: application/json

{
  "title": "Nueva tarea",
  "isComplete": false
}
```

**Response (201 Created):**
```json
{
  "id": 3,
  "title": "Nueva tarea",
  "isComplete": false
}
```

**Headers:**
```
Location: https://localhost:5001/api/todos/3
```

**Response (400 Bad Request):**
```json
"El título no puede estar vacío"
```

---

#### 4. **PUT /api/todos/{id}**
Actualiza una tarea existente

**Request:**
```http
PUT /api/todos/1 HTTP/1.1
Host: localhost:5001
Content-Type: application/json

{
  "title": "Tarea actualizada",
  "isComplete": true
}
```

**Response (200 OK):**
```json
{
  "id": 1,
  "title": "Tarea actualizada",
  "isComplete": true
}
```

**Response (404 Not Found):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

**Response (400 Bad Request):**
```json
"El título no puede estar vacío"
```

---

#### 5. **DELETE /api/todos/{id}**
Elimina una tarea

**Request:**
```http
DELETE /api/todos/1 HTTP/1.1
Host: localhost:5001
```

**Response (204 No Content):**
```
[Sin contenido en el cuerpo]
```

**Response (404 Not Found):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

---

## ?? Estrategia de Pruebas

### Pirámide de Pruebas

```
        /\
  /  \    E2E Tests (2-3 pruebas)
 /    \   
     /------\  Integration Tests (5-7 pruebas)
    /    \ 
   /----------\ Unit Tests (10+ pruebas)
/____________\
```

### 1. Pruebas Unitarias (TodoApp.UnitTests)

#### Objetivo
Probar unidades individuales de código en aislamiento, sin dependencias externas.

#### Herramientas
- **xUnit**: Framework de testing
- **Moq**: Framework para crear mocks y stubs

#### Cobertura

##### **TodoServiceTests** (src/Services/TodoService.cs)
```
? GetAll_CuandoNoHayItems_DevuelveColeccionVacia()
? Add_ConItemValido_DevuelveItemConId()
? Add_ConTituloVacio_LanzaArgumentException()
? GetById_ConIdExistente_DevuelveItem()
? Delete_ConIdExistente_EliminaYDevuelveTrue()
```

**Características:**
- Prueba la lógica de negocio directamente
- No usa dependencias externas
- Instancia directa de `TodoService`
- Verifica comportamiento esperado y casos de error

##### **TodosControllerTests** (src/Controllers/TodosController.cs)
```
? GetAll_LlamaAlServicioYDevuelveOkResult()
? GetById_ConIdExistente_DevuelveOkResult()
? Create_ConDatosValidos_DevuelveCreatedAtAction()
? Delete_ConIdExistente_DevuelveNoContent()
```

**Características:**
- Usa `Mock<ITodoService>` para aislar el controlador
- Verifica que se llamen los métodos correctos del servicio
- Valida los códigos de estado HTTP
- Verifica el formato de las respuestas

#### Patrón AAA (Arrange-Act-Assert)
```csharp
[Fact]
public void Add_ConItemValido_DevuelveItemConId()
{
    // Arrange - Preparación
    var item = new TodoItem { Title = "Test Todo" };

    // Act - Acción
    var result = _todoService.Add(item);

    // Assert - Verificación
    Assert.NotEqual(0, result.Id);
    Assert.Equal(item.Title, result.Title);
}
```

---

### 2. Pruebas de Integración (TodoApp.IntegrationTests)

#### Objetivo
Probar la interacción entre múltiples componentes, incluyendo la infraestructura de ASP.NET Core.

#### Herramientas
- **xUnit**: Framework de testing
- **WebApplicationFactory**: Para levantar la aplicación en memoria
- **HttpClient**: Para hacer peticiones HTTP reales

#### Cobertura

##### **TodosControllerTests** (Integración completa)
```
? GetAll_DevuelveOkYColeccion()
? GetById_ConIdInexistente_DevuelveNotFound()
? Create_ConDatosValidos_DevuelveCreatedYNuevoItem()
? FlujoCompleto_CrearActualizarYEliminar()
```

**Características:**
- Levanta toda la aplicación con `WebApplicationFactory<Program>`
- Usa servicios reales (no mocks)
- Hace peticiones HTTP reales
- Verifica serialización/deserialización JSON
- Valida headers (ej: Location en POST)

##### **TodoE2ETests** (dentro de IntegrationTests)
```
? EscenarioCompleto_GestionDeTareas()
```

**Características:**
- Simula un flujo de usuario completo
- Múltiples operaciones en secuencia
- Verifica estado en cada paso
- Limpieza de datos al final

#### Clase Base: IntegrationTestBase
```csharp
public class IntegrationTestBase : IDisposable
{
    protected readonly WebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;

    public IntegrationTestBase()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
      builder.ConfigureServices(services =>
                {
  // Servicios limpios para cada prueba
         services.AddSingleton<ITodoService, TodoService>();
                });
        });
        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}
```

---

### 3. Pruebas End-to-End (TodoApp.E2ETests)

#### Objetivo
Probar flujos completos de usuario simulando escenarios del mundo real.

#### Herramientas
- **xUnit**: Framework de testing
- **WebApplicationFactory**: Para levantar la aplicación
- **Selenium WebDriver**: Preparado para pruebas de UI (futuro)

#### Cobertura

##### **TodoE2ETests**
```
? EscenarioCompleto_GestionDeTareas()
```

**Flujo Completo:**
1. **Verificar estado inicial** - GET todas las tareas
2. **Crear primera tarea** - POST "Completar informe mensual"
3. **Crear segunda tarea** - POST "Preparar presentación"
4. **Verificar que ambas existen** - GET todas las tareas
5. **Marcar primera como completada** - PUT con IsComplete=true
6. **Verificar actualización** - GET tarea por ID
7. **Eliminar segunda tarea** - DELETE
8. **Verificar eliminación** - GET debe retornar 404
9. **Verificar estado final** - GET todas las tareas
10. **Limpieza** - DELETE tarea restante

**Características:**
- Simula un usuario real usando la API
- Verifica consistencia de datos entre operaciones
- Valida el comportamiento completo del sistema
- Incluye limpieza de datos

---

### Matriz de Cobertura de Pruebas

| Componente | Unit Tests | Integration Tests | E2E Tests |
|------------|:----------:|:-----------------:|:---------:|
| TodoService | ? | ? | ? |
| TodosController | ? | ? | ? |
| API Endpoints | ? | ? | ? |
| Flujos Completos | ? | ? | ? |
| Validaciones | ? | ? | ? |
| Códigos HTTP | ? | ? | ? |

---

### Comandos de Ejecución

#### Ejecutar todas las pruebas
```bash
dotnet test
```

#### Ejecutar solo pruebas unitarias
```bash
dotnet test tests/TodoApp.UnitTests/TodoApp.UnitTests.csproj
```

#### Ejecutar solo pruebas de integración
```bash
dotnet test tests/TodoApp.IntegrationTests/TodoApp.IntegrationTests.csproj
```

#### Ejecutar solo pruebas E2E
```bash
dotnet test tests/TodoApp.E2ETests/TodoApp.E2ETests.csproj
```

#### Ejecutar con cobertura de código
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

---

## ?? Dependencias y Paquetes

### TodoApp.Api (Proyecto Principal)

```xml
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
```

**Propósito:**
- Genera documentación OpenAPI/Swagger
- Proporciona UI interactiva para probar la API
- Facilita la integración con clientes

---

### TodoApp.UnitTests

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="xunit" Version="2.5.3" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
<PackageReference Include="Moq" Version="4.20.72" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

**Propósito:**
- **xUnit**: Framework de pruebas moderno y extensible
- **Moq**: Creación de mocks para aislar dependencias
- **coverlet**: Medición de cobertura de código

---

### TodoApp.IntegrationTests

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="xunit" Version="2.5.3" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.16" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

**Propósito:**
- **Microsoft.AspNetCore.Mvc.Testing**: WebApplicationFactory para pruebas de integración
- Permite levantar la aplicación completa en memoria

---

### TodoApp.E2ETests

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="xunit" Version="2.5.3" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.16" />
<PackageReference Include="Selenium.WebDriver" Version="4.32.0" />
<PackageReference Include="Selenium.Support" Version="4.32.0" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

**Propósito:**
- **Selenium**: Preparado para automatización de navegador (futuro)
- Permitirá pruebas de UI cuando se agregue frontend

---

## ?? Configuración y Despliegue

### Configuración del Entorno de Desarrollo

#### 1. **Clonar el Repositorio**
```bash
git clone https://github.com/hispafox/251028-Demos
cd 251028-Demos/CursoNet/C0501/TodoApp
```

#### 2. **Restaurar Dependencias**
```bash
dotnet restore
```

#### 3. **Compilar la Solución**
```bash
dotnet build
```

#### 4. **Ejecutar la Aplicación**
```bash
cd src/TodoApp.Api
dotnet run
```

La aplicación estará disponible en:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000
- **Swagger UI**: https://localhost:5001/swagger

#### 5. **Ejecutar Pruebas**
```bash
# Desde la raíz de la solución
dotnet test
```

---

### Configuración del Proyecto

#### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
   "Default": "Information",
 "Microsoft.AspNetCore": "Warning"
  }
  },
  "AllowedHosts": "*"
}
```

#### Program.cs - Configuración de Servicios
```csharp
// Registrar servicios
builder.Services.AddControllers();
builder.Services.AddSingleton<ITodoService, TodoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
```

---

### Variables de Entorno

#### Desarrollo
```
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=https://localhost:5001;http://localhost:5000
```

#### Producción
```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=https://+:443;http://+:80
```

---

### Despliegue

#### Publicar para Producción
```bash
dotnet publish -c Release -o ./publish
```

#### Docker (Futuro)
```dockerfile
# Ejemplo de Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/TodoApp.Api/TodoApp.Api.csproj", "src/TodoApp.Api/"]
RUN dotnet restore "src/TodoApp.Api/TodoApp.Api.csproj"
COPY . .
WORKDIR "/src/src/TodoApp.Api"
RUN dotnet build "TodoApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApp.Api.dll"]
```

---

## ?? Mejoras Futuras

### Funcionalidades Pendientes

#### Prioridad Alta
1. **Persistencia en Base de Datos**
   - Implementar Entity Framework Core
   - Soporte para SQL Server / PostgreSQL
   - Migraciones de base de datos

2. **Autenticación y Autorización**
   - JWT tokens
   - Identity Server
   - Roles y permisos

3. **Validación Avanzada**
   - FluentValidation
   - Data annotations extendidas
   - Validación personalizada

#### Prioridad Media
4. **Paginación y Filtros**
   - Paginación de resultados
   - Filtros por estado (completado/pendiente)
   - Búsqueda por texto
   - Ordenamiento

5. **Logging y Monitoreo**
   - Serilog
   - Application Insights
   - Health checks

6. **Documentación Mejorada**
   - Comentarios XML para Swagger
   - Ejemplos de request/response
   - Descripciones detalladas

#### Prioridad Baja
7. **Frontend**
   - Blazor WebAssembly
   - React / Angular
   - Vue.js

8. **Características Adicionales**
   - Categorías de tareas
   - Fechas de vencimiento
   - Prioridades
   - Etiquetas/Tags
   - Asignación de usuarios

9. **Performance**
   - Caché en memoria
   - Redis
   - Rate limiting

10. **DevOps**
    - CI/CD con GitHub Actions
 - Análisis de código estático
    - Despliegue automático

---

## ?? Notas Técnicas Importantes

### Limitaciones Actuales

1. **Almacenamiento en Memoria**
   - Los datos se pierden al reiniciar la aplicación
   - No apto para producción
   - Útil solo para desarrollo y pruebas

2. **Sin Persistencia**
   - No hay base de datos configurada
   - El servicio usa una lista en memoria

3. **Thread-Safety**
   - `TodoService` registrado como Singleton
   - La lista no es thread-safe
   - Considerar `ConcurrentBag<T>` o locks

### Consideraciones de Seguridad

1. **CORS**
   - No configurado actualmente
   - Necesario para frontend desde otro dominio

2. **HTTPS**
   - Configurado para desarrollo
   - Certificado de desarrollo incluido

3. **Validación**
   - Validación básica implementada
   - Considerar validación más robusta para producción

---

## ?? Referencias y Recursos

### Documentación Oficial
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [xUnit Documentation](https://xunit.net/)
- [Moq Quickstart](https://github.com/moq/moq4)

### Patrones y Mejores Prácticas
- [Microsoft REST API Guidelines](https://github.com/Microsoft/api-guidelines)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Testing Best Practices](https://docs.microsoft.com/aspnet/core/test)

### Tutoriales
- [Web API Tutorial](https://docs.microsoft.com/aspnet/core/tutorials/first-web-api)
- [Integration Testing](https://docs.microsoft.com/aspnet/core/test/integration-tests)

---

## ?? Contacto y Soporte

### Repositorio
```
https://github.com/hispafox/251028-Demos
```

### Ruta del Proyecto
```
CursoNet/C0501/TodoApp/
```

---

## ?? Licencia

Este proyecto es material educativo y está disponible para uso en cursos y capacitaciones de .NET.

---

## ?? Conclusión

TodoApp es una implementación completa y educativa de una API RESTful en .NET 8 que demuestra:

? **Arquitectura Limpia**: Separación clara de responsabilidades  
? **Mejores Prácticas**: Implementación de patrones de diseño modernos  
? **Testing Exhaustivo**: Pirámide de pruebas completa (Unit, Integration, E2E)  
? **Código Mantenible**: Estructura clara y bien documentada  
? **Base Sólida**: Lista para extender con características empresariales  

Este PRD sirve como guía completa para entender, mantener y extender la aplicación TodoApp.

---

**Versión del Documento**: 1.0  
**Última Actualización**: 2024  
**Preparado por**: Equipo de Desarrollo
