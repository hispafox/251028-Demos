# Product Requirements Document (PRD)
## TodoApp - Implementaci�n de Capa de Persistencia con Entity Framework Core

---

## ?? Tabla de Contenidos
1. [Informaci�n General](#informaci�n-general)
2. [Objetivos del Proyecto](#objetivos-del-proyecto)
3. [Alcance de la Implementaci�n](#alcance-de-la-implementaci�n)
4. [Arquitectura Propuesta](#arquitectura-propuesta)
5. [Patrones de Dise�o](#patrones-de-dise�o)
6. [Modelo de Datos](#modelo-de-datos)
7. [Capa de Persistencia](#capa-de-persistencia)
8. [DTOs y Mapeo](#dtos-y-mapeo)
9. [Configuraci�n de Entity Framework](#configuraci�n-de-entity-framework)
10. [Migraciones](#migraciones)
11. [Seeding de Datos con Bogus](#seeding-de-datos-con-bogus)
12. [Actualizaci�n de Servicios](#actualizaci�n-de-servicios)
13. [Actualizaci�n de Tests](#actualizaci�n-de-tests)
14. [Dependencias y Paquetes](#dependencias-y-paquetes)
15. [Plan de Implementaci�n](#plan-de-implementaci�n)
16. [Configuraci�n y Despliegue](#configuraci�n-y-despliegue)

---

## ?? Informaci�n General

### Nombre del Proyecto
**TodoApp - Implementaci�n de Capa de Persistencia**

### Versi�n
2.0.0

### Fecha de Creaci�n
2024

### Prop�sito
Migrar la aplicaci�n TodoApp desde almacenamiento en memoria a una arquitectura con persistencia real utilizando Entity Framework Core, implementando el patr�n Repository, DTOs para separaci�n de capas, y mejores pr�cticas de acceso a datos.

### Contexto
Este PRD extiende el PRD original de TodoApp (v1.0.0) agregando persistencia de datos como funcionalidad principal que estaba fuera del alcance inicial.

---

## ?? Objetivos del Proyecto

### Objetivos Principales

1. **Implementar Persistencia Real con Entity Framework Core**
   - Migrar de almacenamiento en memoria a base de datos
   - Configurar DbContext y modelos de entidades
   - Implementar Code-First con migraciones

2. **Aplicar Patr�n Repository**
 - Abstraer el acceso a datos del resto de la aplicaci�n
   - Implementar interfaces de repositorio gen�ricas y espec�ficas
   - Mejorar testabilidad y mantenibilidad

3. **Implementar DTOs (Data Transfer Objects)**
   - Separar modelos de dominio de objetos de transferencia
   - Evitar exponer entidades directamente a trav�s de la API
   - Implementar validaciones en DTOs

4. **Mantener Compatibilidad con Tests Existentes**
   - Adaptar tests unitarios para usar repositorios mockeados
   - Actualizar tests de integraci�n para usar base de datos en memoria
 - Preservar la cobertura de pruebas existente

### Objetivos Secundarios

- Implementar AutoMapper para mapeo autom�tico entre entidades y DTOs
- Implementar Bogus para generaci�n de datos de prueba realistas
- Agregar manejo de transacciones
- Implementar Unit of Work pattern (opcional)
- Configurar diferentes proveedores de base de datos (SQL Server, PostgreSQL, SQLite)

---

## ?? Alcance de la Implementaci�n

### ? Incluido en el Alcance

? **Capa de Persistencia**
   - Configuraci�n de Entity Framework Core
   - DbContext para TodoApp
   - Modelos de entidad con configuraciones Fluent API
   
? **Patr�n Repository**
   - IRepository<T> gen�rico
   - ITodoRepository espec�fico con operaciones personalizadas
   - TodoRepository con implementaci�n completa

? **DTOs (Data Transfer Objects)**
   - TodoItemDto para lectura
   - CreateTodoItemDto para creaci�n
   - UpdateTodoItemDto para actualizaci�n
   - Validaciones con Data Annotations

? **Mapeo de Objetos**
 - Configuraci�n de AutoMapper
   - Profiles para mapeo entidades ? DTOs
   - Mapeo manual como alternativa

? **Migraciones**
   - Migraci�n inicial con tabla Todos
   - Scripts de seed data con Bogus para desarrollo
   - Estrategia de migraciones para diferentes entornos
   - Generaci�n de datos realistas con locale espa�ol

? **Configuraci�n Multi-Base de Datos**
   - SQL Server para producci�n
   - SQLite para desarrollo local
- InMemory para tests

? **Actualizaci�n de Capa de Servicio**
   - TodoService usando repositorios en lugar de List<T>
   - Manejo de transacciones en operaciones complejas
   - Validaciones de negocio

? **Actualizaci�n de Tests**
   - Tests unitarios con repositorios mockeados
   - Tests de integraci�n con base de datos en memoria
   - Tests E2E con base de datos real (SQLite)

### ? Fuera del Alcance

? Unit of Work complejo (se implementar� en versi�n futura)  
? Auditor�a autom�tica de cambios  
? Soft delete (eliminaci�n l�gica)  
? Versionado de entidades  
? Cach� distribuido (Redis)  
? Sincronizaci�n offline  
? Replicaci�n de datos  
? Sharding de base de datos  

---

## ??? Arquitectura Propuesta

### Estructura de Carpetas Actualizada

```
TodoApp/
??? src/
?   ??? TodoApp.Api/
?     ??? Controllers/      # Controladores REST
?       ?   ??? TodosController.cs
?       ??? Services/    # L�gica de negocio
?       ?   ??? ITodoService.cs
?       ?   ??? TodoService.cs
?       ??? Data/          # ? NUEVO: Capa de datos
?       ? ??? TodoDbContext.cs
?       ?   ??? Repositories/
?       ?   ?   ??? IRepository.cs
?       ?   ?   ??? Repository.cs
?       ?   ?   ??? ITodoRepository.cs
?       ?   ?   ??? TodoRepository.cs
?       ?   ??? Entities/
?       ?   ?   ??? TodoEntity.cs
?       ? ??? Configurations/
?       ?   ?   ??? TodoEntityConfiguration.cs
? ?   ??? Seeders/  # ? NUEVO: Generaci�n de datos
?       ?   ?   ??? TodoDataSeeder.cs
? ?   ??? Extensions/        # ? NUEVO: M�todos de extensi�n
?       ?       ??? DatabaseSeederExtensions.cs
?       ??? DTOs/   # ? NUEVO: Objetos de transferencia
?    ?   ??? TodoItemDto.cs
?       ?   ??? CreateTodoItemDto.cs
?  ? ??? UpdateTodoItemDto.cs
?  ??? Mappings/     # ? NUEVO: Configuraci�n de mapeos
?       ???? TodoMappingProfile.cs
?       ??? Models/      # ?? DEPRECADO (mantenido por compatibilidad)
?       ?   ??? TodoItem.cs
?       ??? Migrations/       # ? NUEVO: Migraciones EF Core
?       ??? Program.cs
?
??? tests/
    ??? TodoApp.UnitTests/# Tests con mocks
 ??? TodoApp.IntegrationTests/ # Tests con DB en memoria
    ??? TodoApp.E2ETests/         # Tests con DB real
```

### Diagrama de Capas

```
???????????????????????????????????????????????????????????
?Presentation Layer      ?
?                    (Controllers + DTOs)         ?
?    TodosController ? CreateTodoItemDto, TodoItemDto     ?
???????????????????????????????????????????????????????????
?
          ?
???????????????????????????????????????????????????????????
?    Business Layer    ?
?      (Services + Mapping)         ?
?    ITodoService ? TodoService ? AutoMapper        ?
???????????????????????????????????????????????????????????
        ?
     ?
???????????????????????????????????????????????????????????
?              Data Access Layer   ?
?             (Repositories + Entities)               ?
?    ITodoRepository ? TodoRepository ? TodoEntity        ?
???????????????????????????????????????????????????????????
          ?
 ?
???????????????????????????????????????????????????????????
?   Persistence Layer         ?
?        (EF Core + Database)         ?
?    TodoDbContext ? SQL Server / SQLite / InMemory       ?
???????????????????????????????????????????????????????????
```

---

## ?? Patrones de Dise�o

### 1. Repository Pattern

#### Prop�sito
Abstraer el acceso a datos y proporcionar una interfaz orientada a colecciones para trabajar con el dominio.

#### Beneficios
- ? Desacoplamiento entre l�gica de negocio y acceso a datos
- ? Facilita el testing con mocks
- ? Centraliza consultas y l�gica de acceso a datos
- ? Permite cambiar el mecanismo de persistencia sin afectar servicios

#### Implementaci�n

**Interface Gen�rica:**
```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
```

**Interface Espec�fica:**
```csharp
public interface ITodoRepository : IRepository<TodoEntity>
{
    Task<IEnumerable<TodoEntity>> GetCompletedAsync();
    Task<IEnumerable<TodoEntity>> GetPendingAsync();
    Task<int> GetCountAsync();
}
```

---

### 2. DTO Pattern (Data Transfer Object)

#### Prop�sito
Transferir datos entre capas sin exponer la estructura interna de las entidades.

#### Beneficios
- ? Oculta detalles de implementaci�n de entidades
- ? Permite diferentes representaciones para lectura/escritura
- ? Reduce over-posting vulnerabilities
- ? Facilita versionado de API

#### Implementaci�n

```csharp
// Para lectura
public class TodoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

// Para creaci�n
public class CreateTodoItemDto
{
    [Required(ErrorMessage = "El t�tulo es requerido")]
    [MaxLength(200, ErrorMessage = "El t�tulo no puede exceder 200 caracteres")]
    public string Title { get; set; } = string.Empty;
    
  public bool IsComplete { get; set; }
}

// Para actualizaci�n
public class UpdateTodoItemDto
{
    [Required(ErrorMessage = "El t�tulo es requerido")]
    [MaxLength(200, ErrorMessage = "El t�tulo no puede exceder 200 caracteres")]
    public string Title { get; set; } = string.Empty;
    
    public bool IsComplete { get; set; }
}
```

---

### 3. Unit of Work Pattern (Simplificado)

#### Prop�sito
Mantener un seguimiento de los cambios y coordinar la escritura de m�ltiples repositorios.

#### Implementaci�n Simplificada
En esta versi�n, el DbContext act�a como Unit of Work impl�cito:
- `SaveChangesAsync()` persiste todos los cambios de forma transaccional
- No se implementa una clase UnitOfWork expl�cita (simplicidad)

---

## ?? Modelo de Datos

### Entidad de Dominio: TodoEntity

```csharp
namespace TodoApp.Api.Data.Entities
{
    public class TodoEntity
{
        public int Id { get; set; }
        
        public string Title { get; set; } = string.Empty;
        
      public bool IsComplete { get; set; }
        
        public DateTime CreatedAt { get; set; }
     
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties (futuro)
        // public int? UserId { get; set; }
        // public UserEntity? User { get; set; }
    }
}
```

### Configuraci�n con Fluent API

```csharp
public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
{
    public void Configure(EntityTypeBuilder<TodoEntity> builder)
    {
        builder.ToTable("Todos");
        
        builder.HasKey(t => t.Id);
        
  builder.Property(t => t.Id)
   .ValueGeneratedOnAdd();
        
        builder.Property(t => t.Title)
            .IsRequired()
   .HasMaxLength(200);
        
        builder.Property(t => t.IsComplete)
          .IsRequired()
          .HasDefaultValue(false);
        
 builder.Property(t => t.CreatedAt)
       .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()"); // SQL Server
            // .HasDefaultValueSql("datetime('now')"); // SQLite
        
 builder.Property(t => t.UpdatedAt)
    .IsRequired(false);
        
   // �ndices para mejorar rendimiento
        builder.HasIndex(t => t.IsComplete)
      .HasDatabaseName("IX_Todos_IsComplete");
        
        builder.HasIndex(t => t.CreatedAt)
        .HasDatabaseName("IX_Todos_CreatedAt");
    }
}
```

### Esquema de Base de Datos

**Tabla: Todos**

| Columna | Tipo | Constraints | Descripci�n |
|---------|------|-------------|-------------|
| Id | INT | PK, IDENTITY(1,1) | Identificador �nico |
| Title | NVARCHAR(200) | NOT NULL | T�tulo de la tarea |
| IsComplete | BIT | NOT NULL, DEFAULT 0 | Estado de completado |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Fecha de creaci�n |
| UpdatedAt | DATETIME2 | NULL | Fecha de �ltima actualizaci�n |

**�ndices:**
- `PK_Todos` (Clustered) en `Id`
- `IX_Todos_IsComplete` (Non-Clustered) en `IsComplete`
- `IX_Todos_CreatedAt` (Non-Clustered) en `CreatedAt`

---

## ?? Capa de Persistencia

### DbContext: TodoDbContext

```csharp
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data.Entities;
using TodoApp.Api.Data.Configurations;
using TodoApp.Api.Data.Seeders;

namespace TodoApp.Api.Data
{
    public class TodoDbContext : DbContext
    {
   public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
  }
        
        public DbSet<TodoEntity> Todos => Set<TodoEntity>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
 // Aplicar configuraciones
     modelBuilder.ApplyConfiguration(new TodoEntityConfiguration());
         
     // ? SEED DATA CON BOGUS
  
      // Opci�n 1: Seed simple con 50 registros
         // modelBuilder.Entity<TodoEntity>().HasData(
     //     TodoDataSeeder.GenerateTodos(count: 50, seed: 12345)
    // );
            
            // Opci�n 2: Seed categorizado (recomendado)
  modelBuilder.Entity<TodoEntity>().HasData(
              TodoDataSeeder.GenerateCategorizedTodos(seed: 42)
       );
     
            // Opci�n 3: Seed manual (original - comentado)
        // modelBuilder.Entity<TodoEntity>().HasData(
            //     new TodoEntity
            //     {
            //         Id = 1,
   //         Title = "Aprender Entity Framework Core",
     //      IsComplete = false,
       //         CreatedAt = DateTime.UtcNow
         //  },
        //     new TodoEntity
    //     {
   //         Id = 2,
            //         Title = "Implementar patr�n Repository",
        //IsComplete = false,
        //     CreatedAt = DateTime.UtcNow
            //     }
      // );
        }
}
}
```

### Repositorio Gen�rico

```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly TodoDbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public Repository(TodoDbContext context)
    {
   _context = context;
        _dbSet = context.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public virtual async Task<T> AddAsync(T entity)
    {
    await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
     if (entity == null)
       return false;
        
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    return true;
    }
    
    public virtual async Task<bool> ExistsAsync(int id)
    {
        var entity = await GetByIdAsync(id);
    return entity != null;
    }
}
```

### Repositorio Espec�fico: TodoRepository

```csharp
public class TodoRepository : Repository<TodoEntity>, ITodoRepository
{
  public TodoRepository(TodoDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<TodoEntity>> GetCompletedAsync()
    {
        return await _dbSet
            .Where(t => t.IsComplete)
    .OrderByDescending(t => t.UpdatedAt)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<TodoEntity>> GetPendingAsync()
    {
      return await _dbSet
     .Where(t => !t.IsComplete)
      .OrderBy(t => t.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<int> GetCountAsync()
    {
        return await _dbSet.CountAsync();
    }
  
    // Sobrescribir para agregar tracking de UpdatedAt
    public override async Task<TodoEntity> UpdateAsync(TodoEntity entity)
    {
  entity.UpdatedAt = DateTime.UtcNow;
        return await base.UpdateAsync(entity);
    }
}
```

---

## ?? DTOs y Mapeo

### DTOs Completos

```csharp
// DTOs/TodoItemDto.cs
namespace TodoApp.Api.DTOs
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
     public bool IsComplete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

// DTOs/CreateTodoItemDto.cs
namespace TodoApp.Api.DTOs
{
    public class CreateTodoItemDto
    {
        [Required(ErrorMessage = "El t�tulo es requerido")]
   [StringLength(200, MinimumLength = 1, 
     ErrorMessage = "El t�tulo debe tener entre 1 y 200 caracteres")]
public string Title { get; set; } = string.Empty;
        
public bool IsComplete { get; set; } = false;
    }
}

// DTOs/UpdateTodoItemDto.cs
namespace TodoApp.Api.DTOs
{
    public class UpdateTodoItemDto
    {
  [Required(ErrorMessage = "El t�tulo es requerido")]
        [StringLength(200, MinimumLength = 1, 
        ErrorMessage = "El t�tulo debe tener entre 1 y 200 caracteres")]
     public string Title { get; set; } = string.Empty;
        
        public bool IsComplete { get; set; }
    }
}
```

### AutoMapper Profile

```csharp
using AutoMapper;
using TodoApp.Api.Data.Entities;
using TodoApp.Api.DTOs;

namespace TodoApp.Api.Mappings
{
public class TodoMappingProfile : Profile
    {
        public TodoMappingProfile()
    {
 // Entity -> DTO
            CreateMap<TodoEntity, TodoItemDto>();
  
     // CreateDTO -> Entity
     CreateMap<CreateTodoItemDto, TodoEntity>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
         .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
        .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
       
            // UpdateDTO -> Entity
    CreateMap<UpdateTodoItemDto, TodoEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
         .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
      .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
  }
}
```

### Mapeo Manual (Alternativa)

```csharp
public static class TodoMapper
{
    public static TodoItemDto ToDto(this TodoEntity entity)
    {
        return new TodoItemDto
        {
         Id = entity.Id,
     Title = entity.Title,
            IsComplete = entity.IsComplete,
  CreatedAt = entity.CreatedAt,
    UpdatedAt = entity.UpdatedAt
        };
    }
    
    public static TodoEntity ToEntity(this CreateTodoItemDto dto)
    {
        return new TodoEntity
      {
      Title = dto.Title,
            IsComplete = dto.IsComplete,
      CreatedAt = DateTime.UtcNow
 };
    }
    
    public static void UpdateEntity(this UpdateTodoItemDto dto, TodoEntity entity)
    {
   entity.Title = dto.Title;
        entity.IsComplete = dto.IsComplete;
   entity.UpdatedAt = DateTime.UtcNow;
    }
}
```

---

## ?? Configuraci�n de Entity Framework

### Program.cs - Configuraci�n de Servicios

```csharp
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data;
using TodoApp.Api.Data.Repositories;
using TodoApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext seg�n el entorno
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbProvider = builder.Configuration["DatabaseProvider"] ?? "SQLite";

switch (dbProvider)
{
    case "SqlServer":
 builder.Services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlServer(connectionString));
        break;
    
case "PostgreSQL":
 builder.Services.AddDbContext<TodoDbContext>(options =>
   options.UseNpgsql(connectionString));
        break;
    
    case "SQLite":
    default:
        builder.Services.AddDbContext<TodoDbContext>(options =>
options.UseSqlite(connectionString ?? "Data Source=todos.db"));
   break;
}

// Registrar repositorios
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Registrar servicios
builder.Services.AddScoped<ITodoService, TodoService>();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Resto de la configuraci�n
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones autom�ticamente en desarrollo
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    dbContext.Database.Migrate();
    
 app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=todos.db",
    "SqlServerConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoAppDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "PostgreSQLConnection": "Host=localhost;Database=todoappdb;Username=postgres;Password=postgres"
  },
  "DatabaseProvider": "SQLite",
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

### appsettings.Development.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=todos-dev.db"
  },
  "DatabaseProvider": "SQLite",
  "Logging": {
    "LogLevel": {
 "Default": "Debug",
      "Microsoft.AspNetCore": "Debug",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

### appsettings.Production.json

```json
{
  "DatabaseProvider": "SqlServer",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  }
}
```

---

## ??? Migraciones

### Crear Migraci�n Inicial

```bash
# Asegurarse de tener las herramientas de EF Core
dotnet tool install --global dotnet-ef

# Crear la migraci�n inicial
cd src/TodoApp.Api
dotnet ef migrations add InitialCreate

# Aplicar la migraci�n
dotnet ef database update
```

### Script de Migraci�n para SQL Server

```sql
-- Generado por: dotnet ef migrations script
CREATE TABLE [Todos] (
    [Id] int NOT NULL IDENTITY,
  [Title] nvarchar(200) NOT NULL,
    [IsComplete] bit NOT NULL DEFAULT 0,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Todos] PRIMARY KEY ([Id])
);

CREATE INDEX [IX_Todos_IsComplete] ON [Todos] ([IsComplete]);
CREATE INDEX [IX_Todos_CreatedAt] ON [Todos] ([CreatedAt]);

-- Seed data
INSERT INTO [Todos] ([Id], [Title], [IsComplete], [CreatedAt])
VALUES 
    (1, N'Aprender Entity Framework Core', 0, GETUTCDATE()),
    (2, N'Implementar patr�n Repository', 0, GETUTCDATE());
```

### Comandos �tiles de Migraciones

```bash
# Ver migraciones pendientes
dotter ef migrations list

# Generar script SQL
dotnet ef migrations script -o migrations.sql

# Revertir �ltima migraci�n
dotnet ef migrations remove

# Actualizar a una migraci�n espec�fica
dotnet ef database update <MigrationName>

# Revertir todas las migraciones
dotnet ef database update 0

# Eliminar base de datos
dotnet ef database drop
```

---

## ?? Seeding de Datos con Bogus

### Introducci�n a Bogus

**Bogus** es una biblioteca de generaci�n de datos falsos para .NET, inspirada en la biblioteca Faker de JavaScript. Permite crear datos de prueba realistas y consistentes de manera f�cil y r�pida.

### �Por qu� usar Bogus para Seeding?

#### Ventajas

? **Datos Realistas**
- Genera datos que parecen reales (nombres, fechas, textos)
- M�ltiples locales soportados (espa�ol, ingl�s, etc.)
- Datos consistentes y l�gicos

? **Flexibilidad**
- Control total sobre la generaci�n de datos
- Reglas personalizables
- Semillas (seeds) para reproducibilidad

? **Productividad**
- Menos c�digo que escribir manualmente
- F�cil de mantener
- R�pida generaci�n de grandes vol�menes de datos

? **Testing**
- Ideal para pruebas de carga
- Datos variados para edge cases
- Datos determin�sticos cuando se necesita

#### Comparaci�n con Seed Data Manual

| Aspecto | Seed Manual | Bogus |
|---------|-------------|-------|
| **Volumen** | Tedioso para muchos registros | Genera miles f�cilmente |
| **Realismo** | Datos gen�ricos | Datos realistas |
| **Mantenimiento** | Alto (cambios manuales) | Bajo (cambios en reglas) |
| **Variedad** | Limitada | Ilimitada |
| **Localizaci�n** | Manual | Autom�tica |

---

### Instalaci�n de Bogus

#### Agregar Paquete NuGet

```bash
cd src/TodoApp.Api
dotnet add package Bogus --version 35.6.1
```

#### Actualizaci�n del .csproj

```xml
<ItemGroup>
  <!-- Paquetes existentes -->
  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.16" />
  <PackageReference Include="AutoMapper" Version="13.0.1" />
  
  <!-- ? NUEVO: Bogus para generaci�n de datos -->
  <PackageReference Include="Bogus" Version="35.6.1" />
</ItemGroup>
```

---

### Implementaci�n de Seeding con Bogus

#### 1. Crear Clase TodoDataSeeder

Crear archivo: `Data/Seeders/TodoDataSeeder.cs`

```csharp
using Bogus;
using TodoApp.Api.Data.Entities;

namespace TodoApp.Api.Data.Seeders
{
    public static class TodoDataSeeder
    {
  /// <summary>
        /// Genera una lista de TodoEntities usando Bogus
        /// </summary>
/// <param name="count">N�mero de registros a generar</param>
        /// <param name="seed">Semilla para reproducibilidad (opcional)</param>
        /// <returns>Lista de TodoEntity</returns>
        public static List<TodoEntity> GenerateTodos(int count = 50, int? seed = null)
        {
     // Configurar el locale en espa�ol
            Randomizer.Seed = seed.HasValue ? new Random(seed.Value) : new Random();
            
          var todoId = 1;
            
       var faker = new Faker<TodoEntity>("es")
            .StrictMode(false)
       .RuleFor(t => t.Id, f => todoId++)
  .RuleFor(t => t.Title, f => GenerateTodoTitle(f))
       .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.3f)) // 30% completadas
      .RuleFor(t => t.CreatedAt, f => f.Date.Between(
  DateTime.UtcNow.AddMonths(-6), 
      DateTime.UtcNow))
        .RuleFor(t => t.UpdatedAt, (f, t) => 
           {
     // Solo tareas completadas tienen UpdatedAt
 if (t.IsComplete)
         {
   return f.Date.Between(t.CreatedAt, DateTime.UtcNow);
          }
           return null;
        });
            
   return faker.Generate(count);
        }
        
  /// <summary>
        /// Genera tareas categorizadas por tipo
   /// </summary>
    public static List<TodoEntity> GenerateCategorizedTodos(int seed = 42)
        {
            Randomizer.Seed = new Random(seed);
            var todos = new List<TodoEntity>();
    var id = 1;
            
            // Tareas de desarrollo (15)
 todos.AddRange(GenerateDevelopmentTodos(ref id, 15));
    
   // Tareas personales (10)
            todos.AddRange(GeneratePersonalTodos(ref id, 10));
  
      // Tareas administrativas (10)
   todos.AddRange(GenerateAdministrativeTodos(ref id, 10));
            
       // Tareas de reuniones (5)
        todos.AddRange(GenerateMeetingTodos(ref id, 5));
            
   return todos;
        }
    
        private static List<TodoEntity> GenerateDevelopmentTodos(ref int id, int count)
        {
          var faker = new Faker<TodoEntity>("es")
                .RuleFor(t => t.Id, f => id++)
         .RuleFor(t => t.Title, f => f.PickRandom(
          $"Implementar autenticaci�n JWT",
        $"Crear endpoint para {f.Hacker.Noun()}",
       $"Corregir bug en {f.System.FileName()}",
          $"Escribir tests para {f.Hacker.Verb()}",
      $"Optimizar consulta de {f.Database.Column()}",
      $"Refactorizar clase {f.Name.LastName()}Service",
              $"Actualizar dependencias del proyecto",
    $"Implementar patr�n {f.Hacker.Noun()}",
             $"Configurar CI/CD pipeline",
      $"Documentar API endpoint {f.Hacker.Abbreviation()}
                ))
       .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.4f))
         .RuleFor(t => t.CreatedAt, f => f.Date.Recent(30))
        .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(7) : null);
            
 return faker.Generate(count);
        }
        
        private static List<TodoEntity> GeneratePersonalTodos(ref int id, int count)
        {
         var faker = new Faker<TodoEntity>("es")
           .RuleFor(t => t.Id, f => id++)
             .RuleFor(t => t.Title, f => f.PickRandom(
          $"Comprar {f.Commerce.Product()}",
                $"Llamar al m�dico",
         $"Renovar suscripci�n de {f.Commerce.ProductName()}",
    $"Leer libro: {f.Lorem.Sentence(3)}",
     $"Hacer ejercicio",
             $"Preparar presentaci�n",
 $"Organizar escritorio",
           $"Pagar factura de {f.Commerce.Department()}",
   $"Reservar cita con {f.Name.FirstName()}",
             $"Planificar vacaciones"
  ))
        .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.2f))
            .RuleFor(t => t.CreatedAt, f => f.Date.Recent(60))
        .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(30) : null);
            
   return faker.Generate(count);
        }
     
     private static List<TodoEntity> GenerateAdministrativeTodos(ref int id, int count)
        {
   var faker = new Faker<TodoEntity>("es")
      .RuleFor(t => t.Id, f => id++)
   .RuleFor(t => t.Title, f => f.PickRandom(
          $"Aprobar timesheet del equipo",
     $"Revisar presupuesto de {f.Commerce.Department()}",
         $"Completar evaluaci�n de {f.Name.FullName()}",
         $"Firmar contrato con {f.Company.CompanyName()}",
          $"Actualizar pol�tica de {f.Commerce.ProductName()}",
      $"Revisar solicitud de {f.Name.FirstName()}",
       $"Archivar documentos de {f.Date.Month()}",
     $"Preparar reporte trimesal",
   $"Organizar reuni�n de planificaci�n",
         $"Verificar compliance de {f.Commerce.Department()}
    ))
          .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.5f))
    .RuleFor(t => t.CreatedAt, f => f.Date.Recent(45))
      .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(15) : null);
      
            return faker.Generate(count);
 }
        
  private static List<TodoEntity> GenerateMeetingTodos(ref int id, int count)
        {
            var faker = new Faker<TodoEntity>("es")
  .RuleFor(t => t.Id, f => id++)
         .RuleFor(t => t.Title, f => f.PickRandom(
            $"Reuni�n de standup diario",
  $"Sprint planning con {f.Name.FirstName()}",
 $"Retrospectiva del sprint",
     $"Revisi�n de c�digo con el equipo",
      $"One-on-one con {f.Name.FullName()}",
         $"Demo para stakeholders",
    $"Sesi�n de arquitectura",
      $"Reuni�n con cliente {f.Company.CompanyName()}",
         $"Training sobre {f.Hacker.Noun()}",
  $"Comit� de revisi�n t�cnica"
         ))
     .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.6f))
           .RuleFor(t => t.CreatedAt, f => f.Date.Recent(14))
  .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(7) : null);
  
 return faker.Generate(count);
        }
    }
}
```

---

#### 2. Integrar Seeding en DbContext

Actualizar `Data/TodoDbContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data.Entities;
using TodoApp.Api.Data.Configurations;
using TodoApp.Api.Data.Seeders;

namespace TodoApp.Api.Data
{
    public class TodoDbContext : DbContext
    {
   public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
  }
        
        public DbSet<TodoEntity> Todos => Set<TodoEntity>();
        
   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
 // Aplicar configuraciones
     modelBuilder.ApplyConfiguration(new TodoEntityConfiguration());
         
     // ? SEED DATA CON BOGUS
  
      // Opci�n 1: Seed simple con 50 registros
         // modelBuilder.Entity<TodoEntity>().HasData(
     //     TodoDataSeeder.GenerateTodos(count: 50, seed: 12345)
    // );
            
            // Opci�n 2: Seed categorizado (recomendado)
  modelBuilder.Entity<TodoEntity>().HasData(
              TodoDataSeeder.GenerateCategorizedTodos(seed: 42)
       );
     
            // Opci�n 3: Seed manual (original - comentado)
        // modelBuilder.Entity<TodoEntity>().HasData(
            //     new TodoEntity
            //     {
            //         Id = 1,
   //         Title = "Aprender Entity Framework Core",
     //      IsComplete = false,
       //         CreatedAt = DateTime.UtcNow
         //  },
        //     new TodoEntity
    //     {
   //         Id = 2,
            //         Title = "Implementar patr�n Repository",
        //IsComplete = false,
        //     CreatedAt = DateTime.UtcNow
            //     }
      // );
        }
}
}
```

---

#### 3. Crear Extension Method para Seeding Din�mico

Para entornos de desarrollo, podemos agregar datos din�micamente sin migraciones.

Crear archivo: `Data/Extensions/DatabaseSeederExtensions.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data.Seeders;

namespace TodoApp.Api.Data.Extensions
{
    public static class DatabaseSeederExtensions
    {
        /// <summary>
        /// Inicializa la base de datos con datos de prueba
        /// </summary>
        public static async Task SeedDatabaseAsync(this TodoDbContext context, bool clearExisting = false)
        {
            // Asegurar que la base de datos existe
  await context.Database.EnsureCreatedAsync();
            
          // Aplicar migraciones pendientes
    if (context.Database.GetPendingMigrations().Any())
      {
         await context.Database.MigrateAsync();
        }
            
       // Si se solicita, limpiar datos existentes
  if (clearExisting && await context.Todos.AnyAsync())
     {
         context.Todos.RemoveRange(context.Todos);
                await context.SaveChangesAsync();
  }
   
        // Solo agregar si no hay datos
  if (!await context.Todos.AnyAsync())
            {
       var todos = TodoDataSeeder.GenerateCategorizedTodos(seed: 42);
    await context.Todos.AddRangeAsync(todos);
      await context.SaveChangesAsync();
        }
      }
   
        /// <summary>
        /// Agrega m�s datos de prueba a la base de datos existente
     /// </summary>
   public static async Task AddMoreSeedDataAsync(this TodoDbContext context, int count = 50)
        {
    var maxId = await context.Todos.MaxAsync(t => (int?)t.Id) ?? 0;
       var todos = TodoDataSeeder.GenerateTodos(count, seed: DateTime.Now.Millisecond);
            
            // Ajustar IDs para evitar conflictos
          foreach (var todo in todos)
        {
           todo.Id = ++maxId;
 }
  
            await context.Todos.AddRangeAsync(todos);
   await context.SaveChangesAsync();
        }
    }
}
```

---

#### 4. Configurar Seeding en Program.cs

Actualizar `Program.cs` para aplicar seeding:

```csharp
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data;
using TodoApp.Api.Data.Extensions;
using TodoApp.Api.Data.Repositories;
using TodoApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext seg�n el entorno
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbProvider = builder.Configuration["DatabaseProvider"] ?? "SQLite";

switch (dbProvider)
{
    case "SqlServer":
 builder.Services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlServer(connectionString));
        break;
    
case "PostgreSQL":
 builder.Services.AddDbContext<TodoDbContext>(options =>
   options.UseNpgsql(connectionString));
        break;
    
    case "SQLite":
    default:
        builder.Services.AddDbContext<TodoDbContext>(options =>
options.UseSqlite(connectionString ?? "Data Source=todos.db"));
   break;
}

// Registrar repositorios y servicios
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Resto de la configuraci�n
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ? APLICAR MIGRACIONES Y SEEDING EN DESARROLLO
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    
    try
    {
        var context = services.GetRequiredService<TodoDbContext>();
        
        // Aplicar migraciones
    await context.Database.MigrateAsync();
        
     // Aplicar seeding (solo si la configuraci�n lo permite)
        var seedDatabase = builder.Configuration.GetValue<bool>("SeedDatabase", true);
     if (seedDatabase)
        {
      await context.SeedDatabaseAsync(clearExisting: false);
     }
}
    catch (Exception ex)
    {
      var logger = services.GetRequiredService<ILogger<Program>>();
     logger.LogError(ex, "Error al aplicar migraciones o seeding");
    }
  
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

#### 5. Configuraci�n en appsettings

Actualizar `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=todos-dev.db"
  },
  "DatabaseProvider": "SQLite",
  "SeedDatabase": true,
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Debug",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

---

## ?? Conclusi�n

Este PRD complementa el PRD original de TodoApp agregando una capa de persistencia completa con:

? **Entity Framework Core** para acceso a datos moderno  
? **Patr�n Repository** para abstracci�n y testabilidad  
? **DTOs** para separaci�n de capas y seguridad  
? **AutoMapper** para mapeo eficiente  
? **Bogus** para generaci�n de datos realistas de prueba  
? **Migraciones** para control de versiones de BD  
? **Seeding estrat�gico** con datos categorizados y realistas  
? **Tests actualizados** manteniendo cobertura  
? **Multi-base de datos** para flexibilidad  

La aplicaci�n TodoApp ahora est� lista para escenarios empresariales con persistencia real, generaci�n de datos de prueba profesionales, y manteniendo las mejores pr�cticas de desarrollo en .NET 8.

### Beneficios del Seeding con Bogus

?? **Datos Realistas**: Genera t�tulos, fechas y estados que parecen reales  
?? **Productividad**: Crear 1000 registros toma segundos  
?? **Localizaci�n**: Soporte para espa�ol y m�ltiples idiomas  
?? **Testing**: Datos reproducibles con semillas  
?? **Demos**: Base de datos lista para presentaciones  

---

**Versi�n del Documento**: 2.1  
**�ltima Actualizaci�n**: 2024  
**Relacionado con**: PRD-TodoApp.md v1.0  
**Preparado por**: Equipo de Desarrollo

**Cambios en v2.1**:
- ? Agregada secci�n completa sobre Seeding con Bogus
- ? Implementaci�n de `TodoDataSeeder` con generaci�n categorizada
- ? Extension methods para seeding din�mico
- ? Estrategias de seeding (est�tico, din�mico, h�brido)
- ? Ejemplos pr�cticos y mejores pr�cticas
- ? Consideraciones de rendimiento para grandes vol�menes
