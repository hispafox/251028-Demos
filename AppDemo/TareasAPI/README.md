# API de Gestión de Tareas

Una API REST completa para gestionar tareas, desarrollada con ASP.NET Core 8.

## ?? Características

- ? CRUD completo de tareas (Crear, Leer, Actualizar, Eliminar)
- ? Filtrado de tareas por estado (completadas/pendientes)
- ? Validación de datos
- ? Documentación con Swagger
- ? Logging integrado
- ? CORS habilitado

## ?? Modelo de Datos

Cada tarea contiene:
- **Id**: Identificador único (generado automáticamente)
- **Descripcion**: Descripción de la tarea (5-500 caracteres)
- **FechaLimite**: Fecha límite para completar la tarea
- **Completada**: Estado de la tarea (completada/pendiente)
- **FechaCreacion**: Fecha de creación (generada automáticamente)

## ??? Requisitos

- .NET 8.0 SDK
- Visual Studio 2022 o VS Code (opcional)

## ?? Instalación y Ejecución

1. Navegar al directorio del proyecto:
```bash
cd TareasAPI
```

2. Restaurar dependencias:
```bash
dotnet restore
```

3. Ejecutar la aplicación:
```bash
dotnet run
```

4. La API estará disponible en:
   - HTTPS: `https://localhost:7XXX` (el puerto se muestra en la consola)
   - HTTP: `http://localhost:5XXX`

5. Acceder a Swagger UI:
   - `https://localhost:7XXX/swagger`

## ?? Endpoints

### Obtener todas las tareas
```http
GET /api/tareas
```

### Obtener una tarea por ID
```http
GET /api/tareas/{id}
```

### Obtener tareas por estado
```http
GET /api/tareas/estado/{completada}
```
Ejemplo: `/api/tareas/estado/true` (tareas completadas)

### Crear una nueva tarea
```http
POST /api/tareas
Content-Type: application/json

{
  "descripcion": "Completar el informe mensual",
  "fechaLimite": "2024-12-31T23:59:59Z"
}
```

### Actualizar una tarea
```http
PUT /api/tareas/{id}
Content-Type: application/json

{
  "descripcion": "Nueva descripción",
  "fechaLimite": "2024-12-31T23:59:59Z",
  "completada": true
}
```

### Marcar tarea como completada
```http
PATCH /api/tareas/{id}/completar
```

### Eliminar una tarea
```http
DELETE /api/tareas/{id}
```

## ?? Ejemplos de Uso con cURL

### Crear una tarea:
```bash
curl -X POST https://localhost:7XXX/api/tareas \
  -H "Content-Type: application/json" \
  -d "{\"descripcion\":\"Revisar código\",\"fechaLimite\":\"2024-12-25T23:59:59Z\"}"
```

### Obtener todas las tareas:
```bash
curl https://localhost:7XXX/api/tareas
```

### Actualizar una tarea:
```bash
curl -X PUT https://localhost:7XXX/api/tareas/1 \
  -H "Content-Type: application/json" \
  -d "{\"completada\":true}"
```

## ?? Validaciones

- La descripción debe tener entre 5 y 500 caracteres
- La fecha límite no puede ser en el pasado
- Todos los campos requeridos deben proporcionarse al crear una tarea

## ??? Arquitectura

```
TareasAPI/
??? Controllers/           # Controladores de la API
?   ??? TareasController.cs
??? Models/               # Modelos de datos
?   ??? Tarea.cs
??? DTOs/                 # Data Transfer Objects
?   ??? CrearTareaDto.cs
?   ??? ActualizarTareaDto.cs
??? Repositories/         # Capa de datos
?   ??? ITareaRepository.cs
?   ??? TareaRepository.cs
??? Program.cs           # Configuración de la aplicación
```

## ?? Próximas Mejoras

- [ ] Persistencia con Entity Framework Core
- [ ] Autenticación y autorización
- [ ] Paginación de resultados
- [ ] Búsqueda y filtros avanzados
- [ ] Notificaciones de tareas próximas a vencer
- [ ] Categorías y etiquetas para tareas

## ?? Licencia

Este proyecto es de código abierto y está disponible para uso educativo.
