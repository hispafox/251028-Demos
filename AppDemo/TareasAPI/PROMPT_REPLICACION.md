# ?? PROMPT PARA REPLICAR EL PROYECTO "API DE GESTIÓN DE TAREAS"

## ?? PROMPT COMPLETO PARA IA (GitHub Copilot, ChatGPT, Claude, etc.)

Copia y pega este prompt completo a tu asistente de IA:

---

```
Crea una API REST completa de gestión de tareas usando ASP.NET Core 8.0 con las siguientes características específicas:

## ??? ESTRUCTURA DEL PROYECTO

Nombre del proyecto: **TareasAPI**
Framework: **.NET 8.0**
Tipo: **ASP.NET Core Web API**

### Estructura de carpetas y archivos:
```
TareasAPI/
??? Controllers/
?   ??? TareasController.cs
??? Models/
?   ??? Tarea.cs
??? DTOs/
?   ??? CrearTareaDto.cs
?   ??? ActualizarTareaDto.cs
??? Repositories/
?   ??? ITareaRepository.cs
?   ??? TareaRepository.cs
??? Properties/
?   ??? launchSettings.json
??? Program.cs
??? appsettings.json
??? appsettings.Development.json
??? TareasAPI.csproj
??? TareasAPI-Tests.http
??? README.md
??? GUIA_INICIO.md
??? RESUMEN.md
??? INICIO_RAPIDO.md
??? MIGRACION_EF_CORE.md
```

## ?? MODELO DE DATOS

### Clase Tarea (Models/Tarea.cs):
- **Id**: int (auto-incrementado)
- **Descripcion**: string (requerido, no vacío)
- **FechaLimite**: DateTime (requerido)
- **Completada**: bool (default: false)
- **FechaCreacion**: DateTime (default: DateTime.UtcNow)

Namespace: `TareasAPI.Models`

## ?? DTOs (Data Transfer Objects)

### CrearTareaDto (DTOs/CrearTareaDto.cs):
- **Descripcion**: string, [Required], [StringLength(500, MinimumLength = 5)]
  - ErrorMessage: "La descripción es obligatoria"
  - ErrorMessage: "La descripción debe tener entre 5 y 500 caracteres"
- **FechaLimite**: DateTime, [Required]
  - ErrorMessage: "La fecha límite es obligatoria"

Namespace: `TareasAPI.DTOs`

### ActualizarTareaDto (DTOs/ActualizarTareaDto.cs):
- **Descripcion**: string? (nullable), [StringLength(500, MinimumLength = 5)]
- **FechaLimite**: DateTime? (nullable)
- **Completada**: bool? (nullable)

Namespace: `TareasAPI.DTOs`

## ??? REPOSITORIO (Repository Pattern)

### Interfaz ITareaRepository (Repositories/ITareaRepository.cs):
Métodos asíncronos:
- `Task<IEnumerable<Tarea>> ObtenerTodasAsync()`
- `Task<Tarea?> ObtenerPorIdAsync(int id)`
- `Task<Tarea> CrearAsync(Tarea tarea)`
- `Task<Tarea?> ActualizarAsync(int id, Tarea tarea)`
- `Task<bool> EliminarAsync(int id)`
- `Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada)`

Namespace: `TareasAPI.Repositories`

### Implementación TareaRepository (Repositories/TareaRepository.cs):
- Almacenamiento en memoria usando `List<Tarea>`
- Variable privada `_nextId` para auto-incremento (inicia en 1)
- Constructor con 3 tareas de ejemplo:
  1. "Completar documentación del proyecto" - Pendiente, +5 días
  2. "Revisar código del equipo" - Pendiente, +2 días
  3. "Preparar presentación para cliente" - Completada, +7 días

Namespace: `TareasAPI.Repositories`

## ?? CONTROLADOR (Controllers/TareasController.cs)

**Ruta base**: `[Route("api/[controller]")]`
**Atributo**: `[ApiController]`

### Dependencias inyectadas:
- `ITareaRepository _repository`
- `ILogger<TareasController> _logger`

### Endpoints (7 en total):

1. **GET /api/tareas**
   - Nombre método: `ObtenerTodas()`
   - Retorna: `ActionResult<IEnumerable<Tarea>>`
   - Status: 200 OK
   - Documentación XML: "Obtiene todas las tareas"

2. **GET /api/tareas/{id}**
   - Nombre método: `ObtenerPorId(int id)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 200 OK / 404 Not Found
   - Logging: Warning cuando no se encuentra
   - Mensaje de error: `{ mensaje: "No se encontró la tarea con ID {id}" }`
   - Documentación XML: "Obtiene una tarea por su ID"

3. **GET /api/tareas/estado/{completada}**
   - Nombre método: `ObtenerPorEstado(bool completada)`
   - Retorna: `ActionResult<IEnumerable<Tarea>>`
   - Status: 200 OK
   - Documentación XML: "Obtiene tareas filtradas por estado"

4. **POST /api/tareas**
   - Nombre método: `Crear([FromBody] CrearTareaDto tareaDto)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 201 Created / 400 Bad Request
   - Validaciones:
     - ModelState.IsValid
     - FechaLimite no puede ser en el pasado
   - Mensaje de error: `{ mensaje: "La fecha límite no puede ser en el pasado" }`
   - Logging: Información al crear
   - Retorna: CreatedAtAction con location header
   - Documentación XML: "Crea una nueva tarea"

5. **PUT /api/tareas/{id}**
   - Nombre método: `Actualizar(int id, [FromBody] ActualizarTareaDto tareaDto)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 200 OK / 400 Bad Request / 404 Not Found
   - Validaciones:
     - ModelState.IsValid
     - Tarea existe
     - FechaLimite no puede ser en el pasado (si se proporciona)
   - Actualiza solo campos proporcionados (no nulos)
   - Logging: Información al actualizar
   - Documentación XML: "Actualiza una tarea existente"

6. **PATCH /api/tareas/{id}/completar**
   - Nombre método: `MarcarComoCompletada(int id)`
   - Retorna: `ActionResult<Tarea>`
   - Status: 200 OK / 404 Not Found
   - Acción: Marca Completada = true
   - Logging: Información al marcar
   - Documentación XML: "Marca una tarea como completada"

7. **DELETE /api/tareas/{id}**
   - Nombre método: `Eliminar(int id)`
   - Retorna: `IActionResult`
   - Status: 204 No Content / 404 Not Found
   - Mensaje de error: `{ mensaje: "No se encontró la tarea con ID {id}" }`
   - Logging: Información al eliminar
   - Documentación XML: "Elimina una tarea"

Namespace: `TareasAPI.Controllers`

## ?? CONFIGURACIÓN (Program.cs)

### Servicios registrados:
```csharp
builder.Services.AddControllers();
builder.Services.AddSingleton<ITareaRepository, TareaRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "API de Gestión de Tareas",
        Description = "Una API REST para gestionar tareas con ASP.NET Core 8",
        Contact = new OpenApiContact {
            Name = "Equipo de Desarrollo"
        }
    });
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

### Pipeline de middleware:
```csharp
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
```

## ?? ARCHIVOS DE CONFIGURACIÓN

### appsettings.json:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "TareasAPI": "Information"
    }
  },
  "AllowedHosts": "*",
  "AppSettings": {
    "ApplicationName": "API de Gestión de Tareas",
    "Version": "1.0.0",
    "MaxTareaDescriptionLength": 500,
    "MinTareaDescriptionLength": 5
  }
}
```

### appsettings.Development.json:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### launchSettings.json:
```json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:9384",
      "sslPort": 44370
    }
  },
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:5196",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7091;http://localhost:5196",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### TareasAPI.csproj:
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.21" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>
</Project>
```

## ?? ARCHIVO DE PRUEBAS (TareasAPI-Tests.http)

Crea un archivo con variables y todas las peticiones HTTP para probar:
- Variables: hostname, port, host
- GET todas las tareas
- GET tarea por ID
- GET tareas completadas
- GET tareas pendientes
- POST crear nueva tarea
- PUT actualizar tarea completa
- PUT actualizar solo descripción
- PATCH marcar como completada
- DELETE eliminar tarea
- POST crear con fecha inválida (caso de error)
- POST crear con descripción corta (caso de error)

## ?? DOCUMENTACIÓN

### Crear 5 archivos de documentación en Markdown:

1. **README.md**: Documentación completa con:
   - Descripción del proyecto
   - Características (emoji checkmarks)
   - Modelo de datos
   - Requisitos
   - Instalación y ejecución
   - Todos los endpoints con ejemplos
   - Ejemplos de uso con cURL
   - Validaciones
   - Arquitectura
   - Próximas mejoras sugeridas
   - Licencia

2. **GUIA_INICIO.md**: Guía detallada con:
   - 3 opciones para ejecutar (dotnet CLI, watch, Visual Studio)
   - Cómo acceder a la API
   - Datos de ejemplo precargados
   - 4 formas de probar (Swagger, .http, cURL, Postman)
   - Tabla de endpoints
   - Ejemplos de respuestas JSON
   - Errores comunes y soluciones
   - Configuración (puertos, HTTPS)
   - Recursos adicionales
   - Solución de problemas

3. **RESUMEN.md**: Resumen ejecutivo con:
   - Estado del proyecto (COMPLETADO)
   - Estructura completa del proyecto
   - Funcionalidades implementadas
   - Tabla de endpoints
   - Modelo de datos
   - Validaciones
   - Cómo ejecutar (3 pasos)
   - Cómo probar (3 opciones)
   - Datos de ejemplo
   - Tecnologías utilizadas
   - Archivos de documentación
   - Conceptos aplicados
   - Próximas mejoras
   - Notas importantes
   - Estado final con emojis

4. **INICIO_RAPIDO.md**: Guía ultra-rápida con:
   - 3 pasos para ejecutar
   - Prueba rápida en Swagger
   - Lista de archivos de ayuda
   - Comandos útiles
   - Tabla de endpoints principales
   - Lista de características
   - Tecnologías usadas
   - Próximos pasos
   - Enlaces de ayuda

5. **MIGRACION_EF_CORE.md**: Guía completa de migración a EF Core con:
   - Requisitos previos
   - Instalación de paquetes (SQL Server, SQLite, PostgreSQL)
   - Código del DbContext completo
   - Código del TareaRepositoryEF completo
   - Actualización de Program.cs
   - Cadenas de conexión para cada BD
   - Comandos de migraciones
   - Comandos útiles de EF Core
   - Características adicionales (paginación, búsqueda, índices)
   - Solución de problemas
   - Recursos adicionales
   - Checklist de migración

## ?? ESTILO Y CONVENCIONES

- **Idioma**: Español para todo (nombres, comentarios, mensajes)
- **Naming**: PascalCase para clases y métodos, camelCase para variables
- **Async/Await**: Todos los métodos del repositorio y controlador
- **Nullable**: Habilitado en el proyecto
- **Status Codes**: Usar StatusCodes.Status* constants
- **ProducesResponseType**: Documentar todos los status codes posibles
- **Logging**: Usar ILogger para operaciones importantes
- **Error messages**: Retornar objetos con propiedad "mensaje"
- **Documentation**: Comentarios XML summary en todos los endpoints

## ? CARACTERÍSTICAS ESPECIALES

1. **Validación inteligente en actualización**: Solo actualizar campos no nulos
2. **Endpoint especial PATCH**: Marcar como completada sin enviar todo el objeto
3. **Logging contextual**: Incluir el ID en los mensajes de log
4. **Datos de ejemplo**: 3 tareas precargadas con fechas relativas
5. **Swagger configurado**: Con título, descripción y contacto
6. **CORS abierto**: Para desarrollo frontend
7. **Auto-incremento manual**: Controlar IDs en memoria
8. **FechaCreacion automática**: Asignada en el repositorio
9. **Validación de fechas pasadas**: En creación y actualización
10. **CreatedAtAction**: Retornar location header al crear

## ?? CHECKLIST DE COMPLETITUD

Asegúrate de crear TODO lo siguiente:
- [ ] Modelo Tarea con 5 propiedades
- [ ] Dos DTOs con validaciones Data Annotations
- [ ] Interfaz ITareaRepository con 6 métodos
- [ ] TareaRepository con List en memoria y 3 datos de ejemplo
- [ ] TareasController con 7 endpoints completamente funcionales
- [ ] Program.cs con Swagger, CORS, y DI configurado
- [ ] 3 archivos de configuración JSON completos
- [ ] Archivo .csproj con 2 paquetes NuGet
- [ ] launchSettings.json con 3 perfiles
- [ ] Archivo .http con todas las pruebas
- [ ] 5 archivos de documentación Markdown completos
- [ ] Comentarios XML en todos los endpoints
- [ ] Manejo de errores en todos los endpoints
- [ ] Logging en operaciones CRUD
- [ ] Validaciones de ModelState
- [ ] Validaciones de fechas pasadas
- [ ] Respuestas con mensajes descriptivos
- [ ] Status codes apropiados

## ?? RESULTADO ESPERADO

El proyecto debe:
? Compilar sin errores
? Ejecutarse con `dotnet run`
? Abrir Swagger automáticamente
? Mostrar 3 tareas de ejemplo al hacer GET
? Validar correctamente todos los campos
? Rechazar fechas en el pasado
? Registrar logs en consola
? Responder con status codes apropiados
? Funcionar completamente sin base de datos
? Estar listo para migrar a EF Core

## ?? EXTRAS IMPORTANTES

- Usar `DateTime.UtcNow` para fechas (no DateTime.Now)
- CORS debe estar registrado Y usado en el pipeline
- Swagger solo debe activarse en Development
- UseHttpsRedirection debe estar habilitado
- UseAuthorization debe estar en el pipeline (aunque no se use aún)
- Todos los métodos deben ser asíncronos (async/await)
- Los namespaces deben coincidir con la estructura de carpetas
```

---

## ?? INSTRUCCIONES DE USO DEL PROMPT

### Para GitHub Copilot Chat:
1. Abre una nueva ventana de VS Code
2. Presiona `Ctrl+Shift+P` ? "New Folder..."
3. Presiona `Ctrl+I` (Copilot Chat inline)
4. Pega el prompt completo
5. Espera a que Copilot cree todo el proyecto

### Para ChatGPT/Claude:
1. Abre una nueva conversación
2. Pega el prompt completo
3. Pide que genere archivo por archivo
4. Copia cada archivo a tu proyecto

### Para Copilot Edits:
1. Crea la carpeta del proyecto
2. Abre Copilot Edits (`Ctrl+Shift+I`)
3. Pega el prompt
4. Selecciona "Create new files"
5. Revisa y acepta los cambios

## ?? VALIDACIÓN DEL PROYECTO CREADO

Después de crear el proyecto, valida que:

```bash
# 1. Compilar
dotnet build

# 2. Verificar que no hay errores
# Salida esperada: Build succeeded. 0 Warning(s). 0 Error(s)

# 3. Ejecutar
dotnet run

# 4. Probar en navegador
# https://localhost:7091/swagger

# 5. Probar GET
# Debe devolver 3 tareas de ejemplo

# 6. Probar POST
# Crear una tarea nueva y verificar que devuelve status 201
```

## ?? MÉTRICAS DEL PROYECTO

- **Archivos totales**: 16 archivos principales
- **Líneas de código**: ~800 líneas (sin contar documentación)
- **Endpoints**: 7 endpoints REST
- **DTOs**: 2 clases
- **Modelos**: 1 clase
- **Interfaces**: 1 interfaz
- **Implementaciones**: 1 repositorio
- **Controladores**: 1 controlador
- **Archivos de configuración**: 4 archivos
- **Documentación**: 5 archivos Markdown
- **Pruebas**: 12 casos de prueba en .http
- **Paquetes NuGet**: 2 paquetes

## ?? CONCEPTOS CLAVE IMPLEMENTADOS

1. **Repository Pattern** - Separación de lógica de datos
2. **Dependency Injection** - Inyección de dependencias nativa
3. **DTOs** - Objetos de transferencia de datos
4. **Data Annotations** - Validación declarativa
5. **Async/Await** - Programación asíncrona
6. **REST API** - Arquitectura RESTful completa
7. **Swagger/OpenAPI** - Documentación automática
8. **CORS** - Cross-Origin Resource Sharing
9. **Logging** - Registro de eventos
10. **Status Codes** - Códigos HTTP apropiados
11. **Model Validation** - Validación de entrada
12. **Error Handling** - Manejo de errores descriptivo

## ? CHECKLIST FINAL

Antes de considerar el proyecto completo, verifica:

- [ ] Todos los archivos están creados
- [ ] El proyecto compila sin errores
- [ ] Swagger se abre automáticamente
- [ ] Los 7 endpoints funcionan correctamente
- [ ] Las validaciones rechazan datos inválidos
- [ ] Los logs aparecen en la consola
- [ ] Las 3 tareas de ejemplo están disponibles
- [ ] Los archivos .http funcionan (si usas VS Code)
- [ ] Toda la documentación está completa
- [ ] Los status codes son correctos
- [ ] Los mensajes de error son descriptivos

## ?? LEARNING PATH SUGERIDO

Después de crear este proyecto:

1. **Día 1-2**: Familiarízate con la estructura y prueba todos los endpoints
2. **Día 3-4**: Modifica funcionalidades existentes
3. **Día 5-6**: Agrega nuevas funcionalidades (categorías, prioridades)
4. **Día 7-10**: Migra a Entity Framework Core (usa MIGRACION_EF_CORE.md)
5. **Día 11-15**: Agrega autenticación JWT
6. **Día 16-20**: Agrega pruebas unitarias
7. **Día 21-25**: Agrega pruebas de integración
8. **Día 26-30**: Despliega en Azure o AWS

---

**Versión del Prompt**: 1.0.0  
**Compatible con**: .NET 8.0  
**Última actualización**: Diciembre 2024  
**Creado por**: Proyecto TareasAPI Original

¡Buena suerte replicando el proyecto! ??
