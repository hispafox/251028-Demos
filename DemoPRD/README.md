# TodoApp - API RESTful para Gesti�n de Tareas

API RESTful completa desarrollada con .NET 8 para la gesti�n de tareas (To-Do List), implementando las mejores pr�cticas de desarrollo y una estrategia de pruebas exhaustiva.

## ?? Caracter�sticas

- ? **API RESTful completa** con operaciones CRUD
- ? **Arquitectura limpia** con separaci�n de responsabilidades
- ? **Pruebas exhaustivas** (Unitarias, Integraci�n y E2E)
- ? **Documentaci�n autom�tica** con Swagger/OpenAPI
- ? **Inyecci�n de dependencias** configurada
- ? **.NET 8** con las �ltimas caracter�sticas

## ?? Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (17.8+) o VS Code con C# DevKit
- Git (opcional)

## ??? Instalaci�n y Configuraci�n

### 1. Clonar el Repositorio

```bash
git clone https://github.com/hispafox/251028-Demos.git
cd 251028-Demos/DemoPRD
```

### 2. Restaurar Dependencias

```bash
dotnet restore
```

### 3. Compilar la Soluci�n

```bash
dotnet build
```

### 4. Ejecutar la Aplicaci�n

```bash
cd src/TodoApp.Api
dotnet run
```

La aplicaci�n estar� disponible en:
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

### Solo pruebas de integraci�n
```bash
dotnet test tests/TodoApp.IntegrationTests/TodoApp.IntegrationTests.csproj
```

### Solo pruebas E2E
```bash
dotnet test tests/TodoApp.E2ETests/TodoApp.E2ETests.csproj
```

### Con cobertura de c�digo
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## ?? Estructura del Proyecto

```
TodoApp/
??? src/
?   ??? TodoApp.Api/  # Proyecto principal de la API
?       ??? Controllers/          # Controladores REST
?       ??? Services/   # L�gica de negocio
?       ??? Models/   # Modelos de datos
?       ??? Program.cs  # Punto de entrada
?
??? tests/
?   ??? TodoApp.UnitTests/        # Pruebas unitarias
?   ??? TodoApp.IntegrationTests/ # Pruebas de integraci�n
???? TodoApp.E2ETests/ # Pruebas end-to-end
?
??? TodoApp.sln           # Archivo de soluci�n
??? README.md     # Este archivo
```

## ?? API Endpoints

| M�todo | Endpoint | Descripci�n |
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

### Pir�mide de Pruebas

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

2. **Pruebas de Integraci�n** (`TodoApp.IntegrationTests`)
   - Prueban la interacci�n entre componentes
   - Usan WebApplicationFactory
   - Peticiones HTTP reales

3. **Pruebas E2E** (`TodoApp.E2ETests`)
   - Prueban flujos completos de usuario
   - Simulan escenarios del mundo real
   - Verifican consistencia de datos

## ??? Arquitectura

### Patrones Implementados

- **Dependency Injection**: Inyecci�n de dependencias
- **Repository Pattern**: Interfaz ITodoService
- **RESTful API**: Endpoints REST est�ndar
- **Controller-Service Pattern**: Separaci�n de responsabilidades

### Tecnolog�as

- **Framework**: ASP.NET Core 8.0
- **Lenguaje**: C# 12
- **Testing**: xUnit, Moq
- **Documentaci�n**: Swagger/OpenAPI

## ?? Modelo de Datos

### TodoItem

```json
{
  "id": 1,
  "title": "Completar documentaci�n",
  "isComplete": false
}
```

| Propiedad | Tipo | Descripci�n |
|-----------|------|-------------|
| `id` | `int` | Identificador �nico |
| `title` | `string` | T�tulo de la tarea (requerido) |
| `isComplete` | `bool` | Estado de completado |

## ?? Limitaciones Actuales

- **Almacenamiento en memoria**: Los datos se pierden al reiniciar
- **No thread-safe**: No apto para entornos concurrentes
- **Sin persistencia**: No hay base de datos configurada
- **Sin autenticaci�n**: No hay control de acceso

## ?? Mejoras Futuras

- [ ] Persistencia con Entity Framework Core
- [ ] Autenticaci�n y autorizaci�n JWT
- [ ] Paginaci�n y filtros
- [ ] Logging con Serilog
- [ ] Frontend (Blazor/React/Angular)
- [ ] Docker y contenedores
- [ ] CI/CD con GitHub Actions

## ?? Documentaci�n

- [PRD Completo](PRD-TodoApp.md) - Documento de requisitos del producto
- [Swagger UI](https://localhost:5001/swagger) - Documentaci�n interactiva de la API

## ?? Contribuir

Este es un proyecto educativo. Las contribuciones son bienvenidas:

1. Fork el proyecto
2. Crea una rama para tu caracter�stica (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Licencia

Este proyecto es material educativo y est� disponible para uso en cursos y capacitaciones de .NET.

## ?? Autores

- **TodoApp Team** - [hispafox](https://github.com/hispafox)

## ?? Agradecimientos

- Microsoft por .NET 8
- Comunidad de ASP.NET Core
- Todos los contribuidores

---

**? Si este proyecto te fue �til, considera darle una estrella en GitHub!**
