# Guía de Inicio Rápido - API de Gestión de Tareas

## ?? Ejecutar la API

### Opción 1: Usando dotnet CLI
```bash
cd TareasAPI
dotnet run
```

### Opción 2: Con Hot Reload (desarrollo)
```bash
cd TareasAPI
dotnet watch run
```

### Opción 3: Desde Visual Studio
1. Abre el proyecto en Visual Studio
2. Presiona F5 o haz clic en el botón "Play"

## ?? Acceder a la API

Una vez ejecutada, la API estará disponible en:
- **Swagger UI**: https://localhost:{puerto}/swagger
- **API Base**: https://localhost:{puerto}/api/tareas

El puerto se mostrará en la consola al ejecutar la aplicación.

## ?? Datos de Ejemplo

La API viene precargada con 3 tareas de ejemplo:
1. "Completar documentación del proyecto" - Pendiente
2. "Revisar código del equipo" - Pendiente
3. "Preparar presentación para cliente" - Completada

## ?? Probar la API

### Usando Swagger UI (Recomendado para principiantes)
1. Ejecuta la aplicación
2. Navega a https://localhost:{puerto}/swagger
3. Expande cualquier endpoint
4. Haz clic en "Try it out"
5. Ingresa los parámetros necesarios
6. Haz clic en "Execute"

### Usando el archivo .http (VS Code con REST Client)
1. Instala la extensión "REST Client" en VS Code
2. Abre el archivo `TareasAPI-Tests.http`
3. Actualiza el puerto si es necesario (línea 2)
4. Haz clic en "Send Request" sobre cualquier petición

### Usando cURL
```bash
# Obtener todas las tareas
curl -k https://localhost:7116/api/tareas

# Crear una nueva tarea
curl -k -X POST https://localhost:7116/api/tareas \
  -H "Content-Type: application/json" \
  -d "{\"descripcion\":\"Mi nueva tarea\",\"fechaLimite\":\"2024-12-31T23:59:59Z\"}"

# Obtener tarea por ID
curl -k https://localhost:7116/api/tareas/1

# Actualizar tarea
curl -k -X PUT https://localhost:7116/api/tareas/1 \
  -H "Content-Type: application/json" \
  -d "{\"completada\":true}"

# Eliminar tarea
curl -k -X DELETE https://localhost:7116/api/tareas/1
```

### Usando Postman
1. Importa la colección desde Swagger UI o crea manualmente las peticiones
2. Base URL: `https://localhost:{puerto}/api/tareas`
3. Configura los headers: `Content-Type: application/json`

## ?? Endpoints Disponibles

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/tareas` | Obtener todas las tareas |
| GET | `/api/tareas/{id}` | Obtener una tarea específica |
| GET | `/api/tareas/estado/{completada}` | Filtrar por estado |
| POST | `/api/tareas` | Crear nueva tarea |
| PUT | `/api/tareas/{id}` | Actualizar tarea |
| PATCH | `/api/tareas/{id}/completar` | Marcar como completada |
| DELETE | `/api/tareas/{id}` | Eliminar tarea |

## ?? Ejemplos de Respuestas

### GET /api/tareas
```json
[
  {
    "id": 1,
    "descripcion": "Completar documentación del proyecto",
    "fechaLimite": "2024-12-15T23:59:59Z",
    "completada": false,
    "fechaCreacion": "2024-12-10T10:00:00Z"
  }
]
```

### POST /api/tareas
**Request:**
```json
{
  "descripcion": "Nueva tarea importante",
  "fechaLimite": "2024-12-31T23:59:59Z"
}
```

**Response (201 Created):**
```json
{
  "id": 4,
  "descripcion": "Nueva tarea importante",
  "fechaLimite": "2024-12-31T23:59:59Z",
  "completada": false,
  "fechaCreacion": "2024-12-10T11:30:00Z"
}
```

## ?? Errores Comunes

### Error de certificado SSL
Si usas cURL y obtienes un error SSL, agrega la opción `-k`:
```bash
curl -k https://localhost:7116/api/tareas
```

### Error 404 - Not Found
- Verifica que la URL sea correcta
- Asegúrate de incluir `/api/` en la ruta

### Error 400 - Bad Request
- Revisa que el JSON esté bien formado
- Verifica que todos los campos requeridos estén presentes
- Asegúrate de que la descripción tenga entre 5 y 500 caracteres
- Verifica que la fecha límite no sea en el pasado

## ??? Configuración

### Cambiar el puerto
Edita `Properties/launchSettings.json`:
```json
"https": {
  "commandName": "Project",
  "applicationUrl": "https://localhost:7116;http://localhost:5116",
  ...
}
```

### Deshabilitar HTTPS (no recomendado para producción)
En `Program.cs`, comenta la línea:
```csharp
// app.UseHttpsRedirection();
```

## ?? Recursos Adicionales

- [Documentación ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Swagger/OpenAPI](https://swagger.io/)
- [REST API Best Practices](https://restfulapi.net/)

## ?? Solución de Problemas

### La aplicación no inicia
```bash
# Limpiar y reconstruir
dotnet clean
dotnet build
dotnet run
```

### Error de puerto en uso
```bash
# Cambiar el puerto en launchSettings.json
# O matar el proceso que usa el puerto
netstat -ano | findstr :7116
taskkill /PID {PID} /F
```

### Ver logs detallados
En `appsettings.Development.json`, cambia el nivel de logging:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Debug"
    }
  }
}
```
