# ?? TodoApp - API de Gestión de Tareas

## ?? Descripción

**TodoApp** es una aplicación de ejemplo construida con **ASP.NET Core 8.0** que demuestra las mejores prácticas para desarrollar una API REST con arquitectura en capas y una estrategia de testing completa.

La aplicación implementa operaciones CRUD (Create, Read, Update, Delete) para gestionar tareas (todos) y está completamente documentada y testeada.

---

## ??? Estructura del Proyecto

```
TodoApp/
??? src/
?   ??? TodoApp.Api/      # ?? Aplicación principal (API REST)
?       ??? Controllers/       # Endpoints HTTP
?       ??? Models/         # Entidades de dominio
?       ??? Services/        # Lógica de negocio
? ??? Program.cs               # Configuración y startup
?     ??? DEVELOPMENT_GUIDE.md     # ?? Guía de desarrollo
?
??? tests/
    ??? TodoApp.UnitTests/           # ?? Pruebas unitarias
    ?   ??? TESTING_INSTRUCTIONS.md
    ??? TodoApp.IntegrationTests/    # ?? Pruebas de integración
  ?   ??? TESTING_INSTRUCTIONS.md
    ??? TodoApp.E2ETests/            # ?? Pruebas End-to-End
        ??? TESTING_INSTRUCTIONS.md
```

---

## ?? Inicio Rápido

### Prerrequisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Clonar el Repositorio

```bash
git clone https://github.com/hispafox/251028-Demos.git
cd 251028-Demos/CursoNet/C0501/TodoApp
```

### Ejecutar la Aplicación

```bash
# Restaurar dependencias
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar la API
cd src/TodoApp.Api
dotnet run
```

La API estará disponible en:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000
- **Swagger UI**: https://localhost:5001/swagger

### Ejecutar las Pruebas

```bash
# Desde la raíz del proyecto TodoApp

# Ejecutar todas las pruebas
dotnet test

# Ejecutar solo pruebas unitarias
dotnet test tests/TodoApp.UnitTests

# Ejecutar solo pruebas de integración
dotnet test tests/TodoApp.IntegrationTests

# Ejecutar solo pruebas E2E
dotnet test tests/TodoApp.E2ETests

# Ejecutar con reporte de cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

---

## ?? Características

### API REST Completa

- ? **Operaciones CRUD** para tareas (TodoItems)
- ? **Arquitectura en capas** (Controllers ? Services ? Models)
- ? **Dependency Injection** nativo de ASP.NET Core
- ? **Validación de datos** con Data Annotations
- ? **Manejo de errores** robusto
- ? **Documentación Swagger/OpenAPI** interactiva

### Testing Completo

- ? **Pruebas Unitarias** (xUnit + Moq) - >80% cobertura
- ? **Pruebas de Integración** (WebApplicationFactory) - >70% endpoints
- ? **Pruebas End-to-End** (escenarios completos de usuario)
- ? **Patrón AAA** (Arrange-Act-Assert) en todos los tests

### Documentación

- ? **Guía de Testing General** ([TESTING_GUIDE.md](TESTING_GUIDE.md))
- ? **Guía de Desarrollo de API** ([src/TodoApp.Api/DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md))
- ? **Instrucciones específicas por tipo de test**
- ? **Comentarios XML** en el código
- ? **Swagger UI** con ejemplos

---

## ?? API Endpoints

### Gestión de Tareas (Todos)

| Método | Endpoint | Descripción | Código de Estado |
|--------|----------|-------------|------------------|
| GET | `/api/todos` | Listar todas las tareas | 200 OK |
| GET | `/api/todos/{id}` | Obtener tarea por ID | 200 OK, 404 Not Found |
| POST | `/api/todos` | Crear nueva tarea | 201 Created, 400 Bad Request |
| PUT | `/api/todos/{id}` | Actualizar tarea | 200 OK, 404 Not Found, 400 Bad Request |
| DELETE | `/api/todos/{id}` | Eliminar tarea | 204 No Content, 404 Not Found |

### Ejemplo de Uso

#### Crear una Tarea (POST)

**Request:**
```bash
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Completar informe mensual",
    "isComplete": false
  }'
```

**Response:**
```json
{
  "id": 1,
  "title": "Completar informe mensual",
  "isComplete": false
}
```

#### Listar Tareas (GET)

**Request:**
```bash
curl -X GET https://localhost:5001/api/todos
```

**Response:**
```json
[
  {
    "id": 1,
  "title": "Completar informe mensual",
    "isComplete": false
  },
  {
    "id": 2,
    "title": "Revisar emails",
    "isComplete": true
  }
]
```

---

## ?? Testing

### Pirámide de Testing

```
        /\
       /  \  E2E Tests (10%)
      / ?? \  5-15 escenarios
     /______\
/        \
   / Integration \  Integration Tests (20%)
  /    ??       \20-50 tests
 /______________\
/      \
/   Unit Tests    \  Unit Tests (70%)
/      ??  \  100+ tests
/__________________\
```

### Cobertura Actual

| Tipo de Test | Cantidad | Cobertura | Estado |
|--------------|----------|-----------|--------|
| **Unit Tests** | 7 tests | ~60% | ?? En progreso |
| **Integration Tests** | 4 tests | ~50% | ?? En progreso |
| **E2E Tests** | 1 escenario | Crítico cubierto | ?? Completo |

### Ejecutar Tests por Tipo

```bash
# Unit Tests - Rápidos (< 500ms)
dotnet test tests/TodoApp.UnitTests

# Integration Tests - Moderados (< 5s)
dotnet test tests/TodoApp.IntegrationTests

# E2E Tests - Lentos (< 30s)
dotnet test tests/TodoApp.E2ETests
```

---

## ?? Documentación

### ?? Índice Completo de Documentación

**? [DOCUMENTATION_INDEX.md](DOCUMENTATION_INDEX.md)** - Guía de navegación de toda la documentación

Este índice te ayuda a encontrar rápidamente la información que necesitas según tu rol y tarea.

### Guías Principales

1. **[TESTING_GUIDE.md](TESTING_GUIDE.md)** - Guía general de testing
   - Tipos de pruebas y cuándo usarlas
   - Pirámide de testing
   - Comandos esenciales
   - Mejores prácticas

2. **[src/TodoApp.Api/DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)** - Guía de desarrollo de la API
   - Arquitectura del proyecto
   - Convenciones de código
   - Patrones de diseño
   - Cómo agregar nuevos endpoints

### Guías Específicas de Testing

- ?? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
- ?? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
- ?? [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

---

## ??? Tecnologías Utilizadas

### Backend (API)

- **Framework**: ASP.NET Core 8.0
- **Lenguaje**: C# 12.0
- **Documentación**: Swagger/OpenAPI (Swashbuckle)
- **Arquitectura**: Clean Architecture simplificada

### Testing

- **Framework de Tests**: xUnit 2.5.3
- **Mocking**: Moq 4.20.72
- **Integration Testing**: Microsoft.AspNetCore.Mvc.Testing 8.0.16
- **E2E Testing**: Selenium WebDriver 4.32.0 (opcional)
- **Code Coverage**: Coverlet 6.0.0

### Herramientas de Desarrollo

- **IDE**: Visual Studio 2022 / Visual Studio Code
- **Control de Versiones**: Git
- **CI/CD**: Compatible con GitHub Actions, Azure DevOps

---

## ?? Patrones y Principios

### Patrones Implementados

1. **Dependency Injection** - Desacoplamiento y testabilidad
2. **Repository Pattern** (via Services) - Abstracción de datos
3. **Controller Pattern** (MVC) - Separación de responsabilidades
4. **AAA Pattern** (Arrange-Act-Assert) - Estructura de tests

### Principios SOLID

- ? **Single Responsibility** - Cada clase tiene una sola responsabilidad
- ? **Open/Closed** - Extensible sin modificar código existente
- ? **Liskov Substitution** - Interfaces intercambiables
- ? **Interface Segregation** - Interfaces específicas
- ? **Dependency Inversion** - Dependemos de abstracciones

---

## ?? Comandos Útiles

### Desarrollo

```bash
# Hot reload (recarga automática)
dotnet watch run --project src/TodoApp.Api

# Build específico
dotnet build -c Release

# Publicar para producción
dotnet publish src/TodoApp.Api -c Release -o ./publish
```

### Testing

```bash
# Test con verbosidad detallada
dotnet test --logger "console;verbosity=detailed"

# Test con cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Test solo fallidos
dotnet test --filter Category=Failed
```

### Gestión de Paquetes

```bash
# Listar paquetes instalados
dotnet list package

# Agregar paquete
dotnet add package [PackageName]

# Actualizar paquete
dotnet add package [PackageName] --version [Version]

# Restaurar dependencias
dotnet restore
```

---

## ?? Roadmap

### ? Completado

- [x] API REST básica con CRUD
- [x] Pruebas unitarias para servicios
- [x] Pruebas de integración para endpoints
- [x] Pruebas E2E de flujo completo
- [x] Documentación Swagger
- [x] Guías de desarrollo y testing

### ?? En Progreso

- [ ] Aumentar cobertura de tests unitarios a >80%
- [ ] Aumentar cobertura de tests de integración a >70%
- [ ] Agregar más escenarios E2E

### ?? Planificado

- [ ] Integración con base de datos (Entity Framework Core)
- [ ] Autenticación y autorización (JWT)
- [ ] Paginación y filtrado en endpoints
- [ ] Rate limiting
- [ ] Logging con Serilog
- [ ] Containerización (Docker)
- [ ] CI/CD con GitHub Actions
- [ ] Health checks
- [ ] Versioning de API

---

## ?? Contribuir

### Workflow de Contribución

1. **Fork** el repositorio
2. **Crear** una rama feature (`git checkout -b feature/AmazingFeature`)
3. **Implementar** cambios siguiendo las guías de desarrollo
4. **Escribir tests** para los cambios (unitarios, integración, E2E según corresponda)
5. **Commit** los cambios (`git commit -m 'Add some AmazingFeature'`)
6. **Push** a la rama (`git push origin feature/AmazingFeature`)
7. **Abrir** un Pull Request

### Checklist Antes de PR

- [ ] Todos los tests pasan (`dotnet test`)
- [ ] Build exitoso (`dotnet build`)
- [ ] Sin warnings de compilación
- [ ] Cobertura de tests mantenida o mejorada
- [ ] Documentación actualizada
- [ ] Código sigue las convenciones del proyecto

---

## ?? Licencia

Este proyecto es un ejemplo educativo desarrollado como parte del curso de .NET 8.

---

## ?? Contacto y Soporte

### Documentación

- **Guía General**: [TESTING_GUIDE.md](TESTING_GUIDE.md)
- **API Development**: [src/TodoApp.Api/DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)
- **Unit Tests**: [tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
- **Integration Tests**: [tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
- **E2E Tests**: [tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

### Recursos Externos

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [xUnit Documentation](https://xunit.net/)
- [Moq Documentation](https://github.com/moq/moq4/wiki/Quickstart)
- [Testing in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/)

---

## ?? Objetivos de Aprendizaje

Este proyecto demuestra:

? Desarrollo de **API REST** con ASP.NET Core  
? **Arquitectura en capas** y separación de responsabilidades  
? **Dependency Injection** y principios SOLID  
? **Testing completo** (Unit, Integration, E2E)  
? **Documentación** de código y API  
? **Mejores prácticas** de desarrollo .NET  

---

**Proyecto**: TodoApp - API de Gestión de Tareas  
**Versión**: 1.0  
**Framework**: .NET 8.0  
**Última actualización**: 2024  
**Repositorio**: https://github.com/hispafox/251028-Demos
