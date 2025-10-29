# ?? Guía de Migración a Entity Framework Core

Esta guía te ayudará a migrar la API de gestión de tareas desde almacenamiento en memoria a una base de datos real usando Entity Framework Core.

## ?? Requisitos Previos

- .NET 8.0 SDK
- SQL Server / PostgreSQL / SQLite (elige uno)
- Conocimiento básico de EF Core

## ?? Paso 1: Instalar Paquetes NuGet

### Para SQL Server
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Para SQLite (más simple para desarrollo)
```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Para PostgreSQL
```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

## ?? Paso 2: Crear el DbContext

Crea el archivo `Data/TareasDbContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using TareasAPI.Models;

namespace TareasAPI.Data;

public class TareasDbContext : DbContext
{
    public TareasDbContext(DbContextOptions<TareasDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tarea> Tareas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la entidad Tarea
        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(500);
            
            entity.Property(e => e.FechaLimite)
                .IsRequired();
            
            entity.Property(e => e.Completada)
                .IsRequired()
                .HasDefaultValue(false);
            
            entity.Property(e => e.FechaCreacion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()"); // Para SQL Server
                // .HasDefaultValueSql("datetime('now')"); // Para SQLite
        });

        // Datos semilla (opcional)
        modelBuilder.Entity<Tarea>().HasData(
            new Tarea
            {
                Id = 1,
                Descripcion = "Completar documentación del proyecto",
                FechaLimite = DateTime.UtcNow.AddDays(5),
                Completada = false,
                FechaCreacion = DateTime.UtcNow
            },
            new Tarea
            {
                Id = 2,
                Descripcion = "Revisar código del equipo",
                FechaLimite = DateTime.UtcNow.AddDays(2),
                Completada = false,
                FechaCreacion = DateTime.UtcNow
            }
        );
    }
}
```

## ??? Paso 3: Actualizar el Repositorio

Crea `Repositories/TareaRepositoryEF.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using TareasAPI.Data;
using TareasAPI.Models;

namespace TareasAPI.Repositories;

public class TareaRepositoryEF : ITareaRepository
{
    private readonly TareasDbContext _context;
    private readonly ILogger<TareaRepositoryEF> _logger;

    public TareaRepositoryEF(TareasDbContext context, ILogger<TareaRepositoryEF> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Tarea>> ObtenerTodasAsync()
    {
        return await _context.Tareas
            .OrderByDescending(t => t.FechaCreacion)
            .ToListAsync();
    }

    public async Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        return await _context.Tareas.FindAsync(id);
    }

    public async Task<Tarea> CrearAsync(Tarea tarea)
    {
        tarea.FechaCreacion = DateTime.UtcNow;
        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Tarea creada con ID {Id}", tarea.Id);
        return tarea;
    }

    public async Task<Tarea?> ActualizarAsync(int id, Tarea tareaActualizada)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea == null)
            return null;

        tarea.Descripcion = tareaActualizada.Descripcion;
        tarea.FechaLimite = tareaActualizada.FechaLimite;
        tarea.Completada = tareaActualizada.Completada;

        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Tarea actualizada con ID {Id}", id);
        return tarea;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea == null)
            return false;

        _context.Tareas.Remove(tarea);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Tarea eliminada con ID {Id}", id);
        return true;
    }

    public async Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada)
    {
        return await _context.Tareas
            .Where(t => t.Completada == completada)
            .OrderByDescending(t => t.FechaCreacion)
            .ToListAsync();
    }
}
```

## ?? Paso 4: Actualizar Program.cs

Reemplaza el contenido relacionado con servicios:

```csharp
using Microsoft.EntityFrameworkCore;
using TareasAPI.Data;
using TareasAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();

// Configurar DbContext (ELIGE UNA OPCIÓN)

// Opción 1: SQL Server
builder.Services.AddDbContext<TareasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Opción 2: SQLite (para desarrollo)
// builder.Services.AddDbContext<TareasDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Opción 3: PostgreSQL
// builder.Services.AddDbContext<TareasDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el repositorio con EF Core
builder.Services.AddScoped<ITareaRepository, TareaRepositoryEF>();

// ... resto del código sin cambios
```

## ?? Paso 5: Actualizar appsettings.json

Agrega la cadena de conexión:

### Para SQL Server
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TareasDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Para SQLite
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=tareas.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Para PostgreSQL
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=tareasdb;Username=postgres;Password=tu_password"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

## ?? Paso 6: Crear Migraciones

```bash
# Crear la primera migración
dotnet ef migrations add InitialCreate

# Aplicar la migración (crear la base de datos)
dotnet ef database update
```

## ?? Paso 7: Verificar

```bash
# Ejecutar la aplicación
dotnet run

# La base de datos se creará automáticamente
# Prueba los endpoints en Swagger
```

## ?? Comandos Útiles de EF Core

```bash
# Crear una nueva migración
dotnet ef migrations add NombreMigracion

# Aplicar migraciones pendientes
dotnet ef database update

# Revertir a una migración específica
dotnet ef database update NombreMigracion

# Eliminar la última migración (si no se aplicó)
dotnet ef migrations remove

# Ver el script SQL de una migración
dotnet ef migrations script

# Eliminar la base de datos
dotnet ef database drop

# Ver información de la base de datos
dotnet ef dbcontext info

# Listar migraciones
dotnet ef migrations list
```

## ?? Características Adicionales a Implementar

### 1. Paginación
```csharp
public async Task<(IEnumerable<Tarea> Tareas, int Total)> ObtenerPaginadoAsync(
    int pagina, int tamaño)
{
    var query = _context.Tareas.OrderByDescending(t => t.FechaCreacion);
    
    var total = await query.CountAsync();
    var tareas = await query
        .Skip((pagina - 1) * tamaño)
        .Take(tamaño)
        .ToListAsync();
    
    return (tareas, total);
}
```

### 2. Búsqueda
```csharp
public async Task<IEnumerable<Tarea>> BuscarAsync(string termino)
{
    return await _context.Tareas
        .Where(t => t.Descripcion.Contains(termino))
        .ToListAsync();
}
```

### 3. Índices para Mejorar Rendimiento
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Tarea>(entity =>
    {
        // ... configuración existente ...
        
        // Índices
        entity.HasIndex(e => e.Completada);
        entity.HasIndex(e => e.FechaLimite);
        entity.HasIndex(e => e.FechaCreacion);
    });
}
```

## ?? Solución de Problemas

### Error: "No se puede conectar a la base de datos"
- Verifica la cadena de conexión
- Asegúrate de que el servidor de BD esté ejecutándose
- Para SQL Server, verifica que SQL Server esté instalado

### Error: "Las migraciones fallan"
```bash
# Limpiar y reintentar
dotnet ef database drop --force
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Error: "DbContext not registered"
- Verifica que AddDbContext esté en Program.cs
- Verifica el nombre del servicio registrado

## ?? Recursos Adicionales

- [EF Core Documentation](https://docs.microsoft.com/ef/core/)
- [EF Core Migrations](https://docs.microsoft.com/ef/core/managing-schemas/migrations/)
- [EF Core Connection Strings](https://www.connectionstrings.com/)
- [SQLite Browser](https://sqlitebrowser.org/) - Para ver bases de datos SQLite

## ? Checklist de Migración

- [ ] Instalar paquetes NuGet
- [ ] Crear DbContext
- [ ] Crear repositorio con EF Core
- [ ] Actualizar Program.cs
- [ ] Configurar cadena de conexión
- [ ] Crear primera migración
- [ ] Aplicar migración
- [ ] Probar endpoints
- [ ] Verificar datos en la base de datos
- [ ] Actualizar documentación

---

**Nota**: Esta migración te permitirá tener persistencia real de datos, pero también agrega complejidad. Para proyectos de aprendizaje, la versión en memoria puede ser suficiente.
