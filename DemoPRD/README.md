# TodoApp - API RESTful para Gestión de Tareas

API RESTful completa desarrollada con .NET 8 para la gestión de tareas (To-Do List), implementando las mejores prácticas de desarrollo, **persistencia con Entity Framework Core**, **generación de datos con Bogus**, y una estrategia de pruebas exhaustiva.

## ?? Características

- ? **API RESTful completa** con operaciones CRUD
- ? **Persistencia de datos** con Entity Framework Core
- ? **Patrón Repository** para abstracción de datos
- ? **DTOs y AutoMapper** para separación de capas
- ? **?? Seeding con Bogus** para datos de prueba realistas
- ? **Arquitectura limpia** con separación de responsabilidades
- ? **Pruebas exhaustivas** (Unitarias, Integración y E2E)
- ? **Documentación automática** con Swagger/OpenAPI
- ? **Inyección de dependencias** configurada
- ? **.NET 8** con las últimas características
- ? **Multi-base de datos** (SQLite, SQL Server, PostgreSQL)

## ?? Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (17.8+) o VS Code con C# DevKit
- Git (opcional)

## ?? Instalación y Configuración

> ?? **¿Primera vez?** Sigue la **[Guía de Inicio Rápido](INICIO-RAPIDO.md)** para estar listo en 5 minutos.

### 1. Clonar el Repositorio

```bash
git clone https://github.com/hispafox/251028-Demos.git
cd 251028-Demos/DemoPRD
```

### 2. Restaurar Dependencias

```bash
dotnet restore
```

### 3. Compilar la Solución

```bash
dotnet build
```

### 4. Ejecutar la Aplicación

```bash
cd src/TodoApp.Api
dotnet run
```

La aplicación estará disponible en:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000
- **Swagger UI**: https://localhost:5001/swagger

## ?? Ejecutar Pruebas

### Todas las pruebas
```bash
dotnet test
```

### Solo pruebas unitarias
```bash
dotnet test tests/TodoApp.UnitTests/TodoApp.UnitTests.csproj
```

### Solo pruebas de integración
```bash
dotnet test tests/TodoApp.IntegrationTests/TodoApp.IntegrationTests.csproj
```

### Solo pruebas E2E
```bash
dotnet test tests/TodoApp.E2ETests/TodoApp.E2ETests.csproj
```

### Con cobertura de código
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## ?? Estructura del Proyecto

```
TodoApp/
??? src/
?   ??? TodoApp.Api/    # Proyecto principal de la API
?       ??? Controllers/    # Controladores REST
?       ??? Services/      # Lógica de negocio
?       ??? Data/         # ? Capa de persistencia
?       ?   ??? TodoDbContext.cs
?       ?   ??? Repositories/ # Repositorios
?  ?   ??? Entities/     # Entidades de BD
?       ?   ??? Configurations/ # Configuraciones EF
?       ?   ??? Seeders/      # ?? Generación de datos con Bogus
?     ?   ??? Extensions/   # Extension methods
?       ??? DTOs/        # ? Objetos de transferencia
?       ??? Mappings/       # ? Configuración AutoMapper
?       ??? Models/      # Modelos (legacy)
?       ??? Migrations/     # ? Migraciones EF Core
?       ??? Program.cs        # Punto de entrada
?
??? tests/
?   ??? TodoApp.UnitTests/        # Pruebas unitarias
?   ??? TodoApp.IntegrationTests/ # Pruebas de integración
?   ??? TodoApp.E2ETests/     # Pruebas end-to-end
?
??? TodoApp.sln         # Archivo de solución
??? README.md        # Este archivo
??? PRD-TodoApp.md         # PRD original
??? PRD-Persistencia-TodoApp.md# PRD de persistencia con Bogus
??? INDICE-DOCUMENTACION.md      # Índice maestro
??? RESUMEN-BOGUS-SEEDING.md     # Guía rápida de Bogus
```

**Leyenda:**
- ? Agregado en v2.0 (Persistencia)
- ?? Agregado en v2.1 (Bogus Seeding)

## ?? API Endpoints

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/todos` | Obtiene todas las tareas |
| GET | `/api/todos/{id}` | Obtiene una tarea por ID |
| POST | `/api/todos` | Crea una nueva tarea |
| PUT | `/api/todos/{id}` | Actualiza una tarea existente |
| DELETE | `/api/todos/{id}` | Elimina una tarea |

### Ejemplos de Uso

#### Crear una tarea
```bash
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d '{"title":"Mi nueva tarea","isComplete":false}'
```

#### Obtener todas las tareas
```bash
curl https://localhost:5001/api/todos
```

#### Actualizar una tarea
```bash
curl -X PUT https://localhost:5001/api/todos/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"Tarea actualizada","isComplete":true}'
```

#### Eliminar una tarea
```bash
curl -X DELETE https://localhost:5001/api/todos/1
```

## ?? Estrategia de Pruebas

### Pirámide de Pruebas

```
        /\
       /E2E\      (2-3 pruebas)
      /------\
     /  Int   \   (5-7 pruebas)
    /----------\
 /   Unit     \ (10+ pruebas)
  /______________\
```

### Tipos de Pruebas

1. **Pruebas Unitarias** (`TodoApp.UnitTests`)
   - Prueban servicios y controladores en aislamiento
   - Usan Moq para crear mocks
   - Cobertura: TodoService y TodosController

2. **Pruebas de Integración** (`TodoApp.IntegrationTests`)
   - Prueban la interacción entre componentes
   - Usan WebApplicationFactory
   - Peticiones HTTP reales

3. **Pruebas E2E** (`TodoApp.E2ETests`)
   - Prueban flujos completos de usuario
   - Simulan escenarios del mundo real
   - Verifican consistencia de datos

## ??? Arquitectura

### Patrones Implementados

- **Dependency Injection**: Inyección de dependencias
- **Repository Pattern**: Interfaz ITodoService
- **RESTful API**: Endpoints REST estándar
- **Controller-Service Pattern**: Separación de responsabilidades

### Tecnologías

- **Framework**: ASP.NET Core 8.0
- **Lenguaje**: C# 12
- **Testing**: xUnit, Moq
- **Documentación**: Swagger/OpenAPI

## ?? Modelo de Datos

### TodoItem

```json
{
  "id": 1,
  "title": "Completar documentación",
  "isComplete": false
}
```

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `id` | `int` | Identificador único |
| `title` | `string` | Título de la tarea (requerido) |
| `isComplete` | `bool` | Estado de completado |

## ?? Limitaciones Actuales

### ? Fuera del Alcance (Versión Actual)

- Unit of Work complejo
- Auditoría automática de cambios
- Soft delete (eliminación lógica)
- Autenticación y autorización
- Paginación avanzada
- Caché distribuido (Redis)
- Frontend/Cliente web

## ?? Mejoras Futuras

### ? Implementado en v2.1
- [x] Persistencia con Entity Framework Core
- [x] Patrón Repository genérico y específico
- [x] DTOs y AutoMapper
- [x] ?? Seeding de datos con Bogus
- [x] Migraciones de base de datos
- [x] Soporte multi-base de datos

### ?? Próximas Versiones
- [ ] Autenticación y autorización JWT
- [ ] Paginación y filtros avanzados
- [ ] Logging con Serilog
- [ ] Health checks
- [ ] Frontend (Blazor/React/Angular)
- [ ] Docker y contenedores
- [ ] CI/CD con GitHub Actions
- [ ] Caché con Redis
- [ ] API versionada

## ?? Documentación

### ?? Documentos Principales

1. **[PRD Original](PRD-TodoApp.md)** (v1.0) - Diseño original de la API
   - Arquitectura base sin persistencia
   - Estrategia de pruebas
   - Almacenamiento en memoria

2. **[PRD de Persistencia](PRD-Persistencia-TodoApp.md)** (v2.1) ? **RECOMENDADO**
   - Implementación completa de Entity Framework Core
   - Patrón Repository y DTOs
   - **?? Seeding de datos con Bogus** (NUEVO)
   - Migraciones y configuración de BD
 - Plan de implementación detallado

3. **[Índice de Documentación](INDICE-DOCUMENTACION.md)** - Guía completa de todos los documentos

### ?? Guías Rápidas

- **[Resumen de Implementación](RESUMEN-IMPLEMENTACION.md)** - Vista rápida del proyecto
- **[Resumen de Bogus](RESUMEN-BOGUS-SEEDING.md)** ?? - Guía rápida de seeding de datos
- **[Ejemplos de Uso](EJEMPLOS-USO.md)** - Ejemplos de la API
- **[Comandos Útiles](COMANDOS.md)** - Referencia de comandos

### ?? Código de Ejemplo

- **[Ejemplo de Bogus](EJEMPLO-CODIGO-BOGUS.cs)** ?? - Código completo listo para usar

### ?? Documentación Interactiva

- **[Swagger UI](https://localhost:5001/swagger)** - Documentación interactiva de la API (cuando la app esté corriendo)

## ?? Contribuir

Este es un proyecto educativo. Las contribuciones son bienvenidas:

1. Fork el proyecto
2. Crea una rama para tu característica (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Licencia

Este proyecto es material educativo y está disponible para uso en cursos y capacitaciones de .NET.

## ?? Autores

- **TodoApp Team** - [hispafox](https://github.com/hispafox)

## ?? Agradecimientos

- Microsoft por .NET 8
- Comunidad de ASP.NET Core
- Todos los contribuidores

---

**?? Si este proyecto te fue útil, considera darle una estrella en GitHub!**
