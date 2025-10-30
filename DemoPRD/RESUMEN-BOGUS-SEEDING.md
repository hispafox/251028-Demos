# ?? Resumen Rápido: Seeding con Bogus

## ?? Instalación

```bash
dotnet add package Bogus --version 35.6.1
```

## ?? Uso Rápido

### 1. Generación Simple

```csharp
using Bogus;

var faker = new Faker<TodoEntity>("es")
    .RuleFor(t => t.Id, f => f.IndexFaker)
    .RuleFor(t => t.Title, f => f.Lorem.Sentence(5))
    .RuleFor(t => t.IsComplete, f => f.Random.Bool())
    .RuleFor(t => t.CreatedAt, f => f.Date.Past());

var todos = faker.Generate(50); // Genera 50 items
```

### 2. Integración con DbContext

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<TodoEntity>().HasData(
        TodoDataSeeder.GenerateTodos(count: 50, seed: 12345)
    );
}
```

### 3. Seeding Dinámico en Program.cs

```csharp
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
await context.SeedDatabaseAsync();
}
```

## ?? Estructura de Archivos

```
Data/
??? Seeders/
?   ??? TodoDataSeeder.cs          # Generador de datos con Bogus
??? Extensions/
 ??? DatabaseSeederExtensions.cs # Métodos de extensión
```

## ?? Casos de Uso

### ? Testing
```csharp
var testData = TodoDataSeeder.GenerateTodos(100, seed: 12345);
// Datos reproducibles con semilla fija
```

### ? Demos
```csharp
var demoData = TodoDataSeeder.GenerateCategorizedTodos(seed: 42);
// Datos categorizados y realistas
```

### ? Desarrollo
```csharp
await context.AddMoreSeedDataAsync(count: 1000);
// Agregar más datos sin migración
```

## ?? Configuración

### appsettings.Development.json
```json
{
  "SeedDatabase": true,
  "DatabaseProvider": "SQLite"
}
```

## ?? Ventajas

| Aspecto | Seed Manual | Bogus |
|---------|-------------|-------|
| **Volumen** | Tedioso | Miles en segundos |
| **Realismo** | Genérico | Datos realistas |
| **Mantenimiento** | Alto | Bajo |
| **Localización** | Manual | Automática (es, en, etc.) |

## ?? Generadores Útiles

```csharp
// Nombres
f.Name.FullName()    // "Juan Pérez"
f.Name.FirstName()       // "María"

// Fechas
f.Date.Recent(30)        // Últimos 30 días
f.Date.Past()  // Fecha pasada aleatoria
f.Date.Between(start, end)

// Texto
f.Lorem.Sentence(5)      // Oración de 5 palabras
f.Lorem.Paragraph()      // Párrafo completo

// Comercial
f.Commerce.Product() // "Teclado mecánico"
f.Commerce.Department()  // "Electrónica"
f.Company.CompanyName()  // "Tech Solutions SA"

// Booleanos
f.Random.Bool()          // true/false 50%
f.Random.Bool(0.3f)      // true 30%, false 70%

// Selección aleatoria
f.PickRandom(lista)      // Elemento aleatorio de lista
```

## ?? Comandos Útiles

```bash
# Crear migración con seeding
dotnet ef migrations add AddSeedData

# Aplicar migración
dotnet ef database update

# Regenerar base de datos
dotnet ef database drop
dotnet ef database update

# Ver script SQL generado
dotnet ef migrations script -o seed-script.sql
```

## ?? Mejores Prácticas

### ? DO
- Usar semillas (seeds) para tests reproducibles
- Generar datos categorizados para demos
- Limpiar change tracker en volúmenes grandes
- Usar configuración para habilitar/deshabilitar

### ? DON'T
- No usar seeding dinámico en producción
- No generar datos sensibles reales
- No hacer seeding sin controlar volumen
- No olvidar índices en grandes volúmenes

## ?? Recursos

- **Documentación oficial**: https://github.com/bchavez/Bogus
- **PRD completo**: Ver `PRD-Persistencia-TodoApp.md`
- **Ejemplos**: Carpeta `Data/Seeders/`

---

**Versión**: 1.0  
**Última actualización**: 2024  
**Autor**: Equipo de Desarrollo
