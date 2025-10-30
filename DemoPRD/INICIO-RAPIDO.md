# ?? Guía de Inicio Rápido - TodoApp

## ? Inicio Rápido en 5 Minutos

### 1?? Pre-requisitos (1 min)

```bash
# Verificar que tienes .NET 8 instalado
dotnet --version
# Debe mostrar: 8.0.x
```

Si no tienes .NET 8: https://dotnet.microsoft.com/download/dotnet/8.0

---

### 2?? Clonar y Restaurar (2 min)

```bash
# Clonar el repositorio
git clone https://github.com/hispafox/251028-Demos.git
cd 251028-Demos/DemoPRD

# Restaurar dependencias
dotnet restore

# Compilar
dotnet build
```

---

### 3?? Configurar Base de Datos (1 min)

#### Opción A: SQLite (Recomendado para inicio rápido)

```bash
cd src/TodoApp.Api

# Aplicar migraciones (crea la base de datos automáticamente)
dotnet ef database update

# O simplemente ejecutar (las migraciones se aplican automáticamente)
dotnet run
```

#### Opción B: SQL Server

Editar `src/TodoApp.Api/appsettings.Development.json`:

```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoAppDb;Trusted_Connection=True;"
  }
}
```

```bash
dotnet ef database update
```

---

### 4?? Ejecutar la Aplicación (1 min)

```bash
cd src/TodoApp.Api
dotnet run
```

**? Listo!** La aplicación está corriendo en:
- ?? https://localhost:5001
- ?? Swagger UI: https://localhost:5001/swagger

---

## ?? Primeros Pasos

### Ver la Documentación Interactiva

1. Abre tu navegador
2. Ve a: https://localhost:5001/swagger
3. Explora los endpoints disponibles

---

### Probar la API (Con Swagger)

#### 1. **GET /api/todos** - Ver todas las tareas

1. En Swagger UI, haz click en `GET /api/todos`
2. Click en **"Try it out"**
3. Click en **"Execute"**
4. Verás las tareas generadas automáticamente con Bogus ??

#### 2. **POST /api/todos** - Crear una tarea

1. Click en `POST /api/todos`
2. Click en **"Try it out"**
3. Edita el JSON:
   ```json
   {
     "title": "Mi primera tarea",
     "isComplete": false
   }
 ```
4. Click en **"Execute"**
5. Verás la respuesta con el ID generado

#### 3. **PUT /api/todos/{id}** - Actualizar una tarea

1. Click en `PUT /api/todos/{id}`
2. Click en **"Try it out"**
3. Ingresa el ID (ejemplo: 1)
4. Edita el JSON:
   ```json
   {
     "title": "Tarea completada",
     "isComplete": true
   }
   ```
5. Click en **"Execute"**

#### 4. **DELETE /api/todos/{id}** - Eliminar una tarea

1. Click en `DELETE /api/todos/{id}`
2. Click en **"Try it out"**
3. Ingresa el ID
4. Click en **"Execute"**

---

### Probar la API (Con curl)

```bash
# Ver todas las tareas
curl https://localhost:5001/api/todos

# Crear una tarea
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d '{"title":"Aprender Entity Framework","isComplete":false}'

# Actualizar una tarea
curl -X PUT https://localhost:5001/api/todos/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"EF Core dominado","isComplete":true}'

# Eliminar una tarea
curl -X DELETE https://localhost:5001/api/todos/1
```

---

## ?? Explorar los Datos Generados

### Ver la Base de Datos

#### SQLite (Recomendado: DB Browser for SQLite)

1. Descargar: https://sqlitebrowser.org/
2. Abrir: `src/TodoApp.Api/todos-dev.db`
3. Ver la tabla `Todos` con datos generados por Bogus ??

#### SQL Server (SSMS o Azure Data Studio)

1. Conectar a: `(localdb)\mssqllocaldb`
2. Base de datos: `TodoAppDb`
3. Explorar tabla: `dbo.Todos`

### Datos de Ejemplo Generados

Bogus genera automáticamente **40 tareas** categorizadas:

- ? **15 tareas de desarrollo**: "Implementar autenticación JWT", "Corregir bug en...", etc.
- ? **10 tareas personales**: "Comprar...", "Llamar a...", etc.
- ? **10 tareas administrativas**: "Aprobar timesheet", "Revisar presupuesto", etc.
- ? **5 tareas de reuniones**: "Sprint planning", "One-on-one", etc.

Todos con:
- ?? Fechas realistas (últimos 2-6 meses)
- ? Estados variados (30% completadas)
- ?? Títulos realistas en español

---

## ?? Ejecutar los Tests

### Todos los tests

```bash
# Desde la raíz del proyecto
dotnet test
```

**? Deberías ver:**
```
? Passed: 15+ tests
?? Total time: ~10 segundos
?? Code coverage: ~85%
```

### Tests individuales

```bash
# Solo tests unitarios
dotnet test tests/TodoApp.UnitTests/

# Solo tests de integración
dotnet test tests/TodoApp.IntegrationTests/

# Solo tests E2E
dotnet test tests/TodoApp.E2ETests/
```

---

## ?? Regenerar la Base de Datos

### Limpiar y recrear con nuevos datos

```bash
cd src/TodoApp.Api

# Eliminar base de datos
dotnet ef database drop

# Recrear con migraciones y seeding
dotnet ef database update

# O simplemente ejecutar (se recrea automáticamente)
dotnet run
```

---

## ?? Jugar con Bogus

### Generar más datos

#### Opción 1: Cambiar la configuración

Editar `src/TodoApp.Api/Data/TodoDbContext.cs`:

```csharp
// Cambiar de 40 a 100 tareas
modelBuilder.Entity<TodoEntity>().HasData(
    TodoDataSeeder.GenerateTodos(count: 100, seed: 42)
);
```

Luego:
```bash
dotnet ef migrations add AddMoreData
dotnet ef database update
```

#### Opción 2: Usar endpoint de desarrollo

Si estás en modo Development, puedes usar:

```bash
# Agregar 50 tareas más dinámicamente
curl -X POST https://localhost:5001/api/todos/seed?count=50
```

---

## ?? Explorar la Documentación

### Para Principiantes

1. **[README.md](README.md)** - Introducción (este archivo)
2. **[RESUMEN-BOGUS-SEEDING.md](RESUMEN-BOGUS-SEEDING.md)** - Guía rápida de Bogus
3. **[EJEMPLOS-USO.md](EJEMPLOS-USO.md)** - Más ejemplos de la API

### Para Desarrolladores

1. **[PRD-Persistencia-TodoApp.md](PRD-Persistencia-TodoApp.md)** - Diseño completo con Bogus
2. **[EJEMPLO-CODIGO-BOGUS.cs](EJEMPLO-CODIGO-BOGUS.cs)** - Código completo de seeding
3. **[COMANDOS.md](COMANDOS.md)** - Comandos útiles

### Índice Completo

- **[INDICE-DOCUMENTACION.md](INDICE-DOCUMENTACION.md)** - Navegación completa de docs

---

## ??? Comandos Esenciales

### Desarrollo

```bash
# Ejecutar la aplicación
dotnet run

# Ejecutar con hot reload
dotnet watch run

# Ver logs detallados
dotnet run --verbosity detailed
```

### Base de Datos

```bash
# Ver migraciones
dotnet ef migrations list

# Crear migración
dotnet ef migrations add MiNuevaMigracion

# Aplicar migraciones
dotnet ef database update

# Revertir migración
dotnet ef database update <MigracionAnterior>

# Eliminar base de datos
dotnet ef database drop
```

### Tests

```bash
# Todos los tests
dotnet test

# Con cobertura
dotnet test /p:CollectCoverage=true

# Verbose
dotnet test --logger "console;verbosity=detailed"
```

---

## ?? Solución de Problemas

### Error: "No se puede conectar a la base de datos"

**Solución para SQLite:**
```bash
# Verificar que existe el archivo
ls src/TodoApp.Api/*.db

# Si no existe, aplicar migraciones
dotnet ef database update
```

**Solución para SQL Server:**
```bash
# Verificar conexión
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"

# Si falla, iniciar LocalDB
sqllocaldb start mssqllocaldb
```

### Error: "Port 5001 is already in use"

```bash
# Cambiar el puerto en launchSettings.json
# O detener el proceso que usa el puerto
netstat -ano | findstr :5001
taskkill /PID <PID> /F
```

### Error: "Migrations not found"

```bash
# Crear la migración inicial
dotnet ef migrations add InitialCreate

# Aplicar
dotnet ef database update
```

### Error: "Bogus package not found"

```bash
# Instalar Bogus
dotnet add package Bogus --version 35.6.1

# Restaurar
dotnet restore
```

---

## ? Checklist de Verificación

Después de la instalación, verifica que todo funcione:

- [ ] ? La aplicación compila sin errores (`dotnet build`)
- [ ] ? Los tests pasan (`dotnet test`)
- [ ] ? La aplicación corre (`dotnet run`)
- [ ] ? Swagger UI funciona (https://localhost:5001/swagger)
- [ ] ? GET /api/todos devuelve ~40 tareas generadas con Bogus
- [ ] ? POST /api/todos crea una nueva tarea
- [ ] ? La base de datos persiste datos entre reinicios

---

## ?? Próximos Pasos

### 1. Explorar el Código

```bash
# Abrir en Visual Studio Code
code .

# O en Visual Studio
start TodoApp.sln
```

### 2. Modificar el Seeder

Editar `src/TodoApp.Api/Data/Seeders/TodoDataSeeder.cs` y experimentar con diferentes datos.

### 3. Crear Nuevos Endpoints

Agregar funcionalidad siguiendo los patrones existentes:
- Nuevo DTO ? Nuevo método en Repository ? Nuevo método en Service ? Nuevo endpoint en Controller

### 4. Leer la Documentación Completa

Ver **[PRD-Persistencia-TodoApp.md](PRD-Persistencia-TodoApp.md)** para entender toda la arquitectura.

---

## ?? ¿Necesitas Ayuda?

- ?? **Documentación completa**: [INDICE-DOCUMENTACION.md](INDICE-DOCUMENTACION.md)
- ?? **Ejemplos de código**: [EJEMPLO-CODIGO-BOGUS.cs](EJEMPLO-CODIGO-BOGUS.cs)
- ?? **Guía de Bogus**: [RESUMEN-BOGUS-SEEDING.md](RESUMEN-BOGUS-SEEDING.md)
- ?? **Comandos útiles**: [COMANDOS.md](COMANDOS.md)

---

## ?? ¡Éxito!

Si llegaste hasta aquí, tu entorno está listo. Ahora puedes:

? Desarrollar nuevas funcionalidades  
? Experimentar con Bogus  
? Extender la API  
? Aprender Entity Framework Core  
? Practicar tests  

**¡Feliz codificación! ??**

---

**Versión:** 1.0  
**Última actualización:** 2024  
**Tiempo estimado:** 5-10 minutos  
**Nivel:** Principiante - Intermedio
