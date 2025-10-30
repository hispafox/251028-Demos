# ?? TodoApp - API de Gesti�n de Tareas

## ?? Descripci�n

**TodoApp** es una aplicaci�n de ejemplo construida con **ASP.NET Core 8.0** que demuestra las mejores pr�cticas para desarrollar una API REST con arquitectura en capas y una estrategia de testing completa.

La aplicaci�n implementa operaciones CRUD (Create, Read, Update, Delete) para gestionar tareas (todos) y est� completamente documentada y testeada.

---

## ??? Estructura del Proyecto

```
TodoApp/
??? src/
?   ??? TodoApp.Api/      # ?? Aplicaci�n principal (API REST)
?       ??? Controllers/       # Endpoints HTTP
?       ??? Models/         # Entidades de dominio
?       ??? Services/        # L�gica de negocio
? ??? Program.cs               # Configuraci�n y startup
?     ??? DEVELOPMENT_GUIDE.md     # ?? Gu�a de desarrollo
?
??? tests/
    ??? TodoApp.UnitTests/           # ?? Pruebas unitarias
    ?   ??? TESTING_INSTRUCTIONS.md
    ??? TodoApp.IntegrationTests/    # ?? Pruebas de integraci�n
  ?   ??? TESTING_INSTRUCTIONS.md
    ??? TodoApp.E2ETests/            # ?? Pruebas End-to-End
        ??? TESTING_INSTRUCTIONS.md
```

---

## ?? Inicio R�pido

### Prerrequisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Clonar el Repositorio

```bash
git clone https://github.com/hispafox/251028-Demos.git
cd 251028-Demos/CursoNet/C0501/TodoApp
```

### Ejecutar la Aplicaci�n

```bash
# Restaurar dependencias
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar la API
cd src/TodoApp.Api
dotnet run
```

La API estar� disponible en:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000
- **Swagger UI**: https://localhost:5001/swagger

### Ejecutar las Pruebas

```bash
# Desde la ra�z del proyecto TodoApp

# Ejecutar todas las pruebas
dotnet test

# Ejecutar solo pruebas unitarias
dotnet test tests/TodoApp.UnitTests

# Ejecutar solo pruebas de integraci�n
dotnet test tests/TodoApp.IntegrationTests

# Ejecutar solo pruebas E2E
dotnet test tests/TodoApp.E2ETests

# Ejecutar con reporte de cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

---

## ?? Caracter�sticas

### API REST Completa

- ? **Operaciones CRUD** para tareas (TodoItems)
- ? **Arquitectura en capas** (Controllers ? Services ? Models)
- ? **Dependency Injection** nativo de ASP.NET Core
- ? **Validaci�n de datos** con Data Annotations
- ? **Manejo de errores** robusto
- ? **Documentaci�n Swagger/OpenAPI** interactiva

### Testing Completo

- ? **Pruebas Unitarias** (xUnit + Moq) - >80% cobertura
- ? **Pruebas de Integraci�n** (WebApplicationFactory) - >70% endpoints
- ? **Pruebas End-to-End** (escenarios completos de usuario)
- ? **Patr�n AAA** (Arrange-Act-Assert) en todos los tests

### Documentaci�n

- ? **Gu�a de Testing General** ([TESTING_GUIDE.md](TESTING_GUIDE.md))
- ? **Gu�a de Desarrollo de API** ([src/TodoApp.Api/DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md))
- ? **Instrucciones espec�ficas por tipo de test**
- ? **Comentarios XML** en el c�digo
- ? **Swagger UI** con ejemplos

---

## ?? API Endpoints

### Gesti�n de Tareas (Todos)

| M�todo | Endpoint | Descripci�n | C�digo de Estado |
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

### Pir�mide de Testing

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
| **E2E Tests** | 1 escenario | Cr�tico cubierto | ?? Completo |

### Ejecutar Tests por Tipo

```bash
# Unit Tests - R�pidos (< 500ms)
dotnet test tests/TodoApp.UnitTests

# Integration Tests - Moderados (< 5s)
dotnet test tests/TodoApp.IntegrationTests

# E2E Tests - Lentos (< 30s)
dotnet test tests/TodoApp.E2ETests
```

---

## ?? Documentaci�n

### ?? �ndice Completo de Documentaci�n

**? [DOCUMENTATION_INDEX.md](DOCUMENTATION_INDEX.md)** - Gu�a de navegaci�n de toda la documentaci�n

Este �ndice te ayuda a encontrar r�pidamente la informaci�n que necesitas seg�n tu rol y tarea.

### Gu�as Principales

1. **[TESTING_GUIDE.md](TESTING_GUIDE.md)** - Gu�a general de testing
   - Tipos de pruebas y cu�ndo usarlas
   - Pir�mide de testing
   - Comandos esenciales
   - Mejores pr�cticas

2. **[src/TodoApp.Api/DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)** - Gu�a de desarrollo de la API
   - Arquitectura del proyecto
   - Convenciones de c�digo
   - Patrones de dise�o
   - C�mo agregar nuevos endpoints

### Gu�as Espec�ficas de Testing

- ?? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
- ?? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
- ?? [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

---

## ??? Tecnolog�as Utilizadas

### Backend (API)

- **Framework**: ASP.NET Core 8.0
- **Lenguaje**: C# 12.0
- **Documentaci�n**: Swagger/OpenAPI (Swashbuckle)
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
2. **Repository Pattern** (via Services) - Abstracci�n de datos
3. **Controller Pattern** (MVC) - Separaci�n de responsabilidades
4. **AAA Pattern** (Arrange-Act-Assert) - Estructura de tests

### Principios SOLID

- ? **Single Responsibility** - Cada clase tiene una sola responsabilidad
- ? **Open/Closed** - Extensible sin modificar c�digo existente
- ? **Liskov Substitution** - Interfaces intercambiables
- ? **Interface Segregation** - Interfaces espec�ficas
- ? **Dependency Inversion** - Dependemos de abstracciones

---

## ?? Comandos �tiles

### Desarrollo

```bash
# Hot reload (recarga autom�tica)
dotnet watch run --project src/TodoApp.Api

# Build espec�fico
dotnet build -c Release

# Publicar para producci�n
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

### Gesti�n de Paquetes

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

- [x] API REST b�sica con CRUD
- [x] Pruebas unitarias para servicios
- [x] Pruebas de integraci�n para endpoints
- [x] Pruebas E2E de flujo completo
- [x] Documentaci�n Swagger
- [x] Gu�as de desarrollo y testing

### ?? En Progreso

- [ ] Aumentar cobertura de tests unitarios a >80%
- [ ] Aumentar cobertura de tests de integraci�n a >70%
- [ ] Agregar m�s escenarios E2E

### ?? Planificado

- [ ] Integraci�n con base de datos (Entity Framework Core)
- [ ] Autenticaci�n y autorizaci�n (JWT)
- [ ] Paginaci�n y filtrado en endpoints
- [ ] Rate limiting
- [ ] Logging con Serilog
- [ ] Containerizaci�n (Docker)
- [ ] CI/CD con GitHub Actions
- [ ] Health checks
- [ ] Versioning de API

---

## ?? Contribuir

### Workflow de Contribuci�n

1. **Fork** el repositorio
2. **Crear** una rama feature (`git checkout -b feature/AmazingFeature`)
3. **Implementar** cambios siguiendo las gu�as de desarrollo
4. **Escribir tests** para los cambios (unitarios, integraci�n, E2E seg�n corresponda)
5. **Commit** los cambios (`git commit -m 'Add some AmazingFeature'`)
6. **Push** a la rama (`git push origin feature/AmazingFeature`)
7. **Abrir** un Pull Request

### Checklist Antes de PR

- [ ] Todos los tests pasan (`dotnet test`)
- [ ] Build exitoso (`dotnet build`)
- [ ] Sin warnings de compilaci�n
- [ ] Cobertura de tests mantenida o mejorada
- [ ] Documentaci�n actualizada
- [ ] C�digo sigue las convenciones del proyecto

---

## ?? Licencia

Este proyecto es un ejemplo educativo desarrollado como parte del curso de .NET 8.

---

## ?? Contacto y Soporte

### Documentaci�n

- **Gu�a General**: [TESTING_GUIDE.md](TESTING_GUIDE.md)
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
? **Arquitectura en capas** y separaci�n de responsabilidades  
? **Dependency Injection** y principios SOLID  
? **Testing completo** (Unit, Integration, E2E)  
? **Documentaci�n** de c�digo y API  
? **Mejores pr�cticas** de desarrollo .NET  

---

**Proyecto**: TodoApp - API de Gesti�n de Tareas  
**Versi�n**: 1.0  
**Framework**: .NET 8.0  
**�ltima actualizaci�n**: 2024  
**Repositorio**: https://github.com/hispafox/251028-Demos
