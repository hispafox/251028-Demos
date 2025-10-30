# ?? Resumen R�pido: Seeding con Bogus

## ?? Instalaci�n

```bash
dotnet add package Bogus --version 35.6.1
```

## ?? Uso R�pido

### 1. Generaci�n Simple

```csharp
using Bogus;

var faker = new Faker<TodoEntity>("es")
    .RuleFor(t => t.Id, f => f.IndexFaker)
    .RuleFor(t => t.Title, f => f.Lorem.Sentence(5))
    .RuleFor(t => t.IsComplete, f => f.Random.Bool())
    .RuleFor(t => t.CreatedAt, f => f.Date.Past());

var todos = faker.Generate(50); // Genera 50 items
```

### 2. Integraci�n con DbContext

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<TodoEntity>().HasData(
        TodoDataSeeder.GenerateTodos(count: 50, seed: 12345)
    );
}
```

### 3. Seeding Din�mico en Program.cs

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
 ??? DatabaseSeederExtensions.cs # M�todos de extensi�n
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
// Agregar m�s datos sin migraci�n
```

## ?? Configuraci�n

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
| **Realismo** | Gen�rico | Datos realistas |
| **Mantenimiento** | Alto | Bajo |
| **Localizaci�n** | Manual | Autom�tica (es, en, etc.) |

## ?? Generadores �tiles

```csharp
// Nombres
f.Name.FullName()    // "Juan P�rez"
f.Name.FirstName()       // "Mar�a"

// Fechas
f.Date.Recent(30)        // �ltimos 30 d�as
f.Date.Past()  // Fecha pasada aleatoria
f.Date.Between(start, end)

// Texto
f.Lorem.Sentence(5)      // Oraci�n de 5 palabras
f.Lorem.Paragraph()      // P�rrafo completo

// Comercial
f.Commerce.Product() // "Teclado mec�nico"
f.Commerce.Department()  // "Electr�nica"
f.Company.CompanyName()  // "Tech Solutions SA"

// Booleanos
f.Random.Bool()          // true/false 50%
f.Random.Bool(0.3f)      // true 30%, false 70%

// Selecci�n aleatoria
f.PickRandom(lista)      // Elemento aleatorio de lista
```

## ?? Comandos �tiles

```bash
# Crear migraci�n con seeding
dotnet ef migrations add AddSeedData

# Aplicar migraci�n
dotnet ef database update

# Regenerar base de datos
dotnet ef database drop
dotnet ef database update

# Ver script SQL generado
dotnet ef migrations script -o seed-script.sql
```

## ?? Mejores Pr�cticas

### ? DO
- Usar semillas (seeds) para tests reproducibles
- Generar datos categorizados para demos
- Limpiar change tracker en vol�menes grandes
- Usar configuraci�n para habilitar/deshabilitar

### ? DON'T
- No usar seeding din�mico en producci�n
- No generar datos sensibles reales
- No hacer seeding sin controlar volumen
- No olvidar �ndices en grandes vol�menes

## ?? Recursos

- **Documentaci�n oficial**: https://github.com/bchavez/Bogus
- **PRD completo**: Ver `PRD-Persistencia-TodoApp.md`
- **Ejemplos**: Carpeta `Data/Seeders/`

---

**Versi�n**: 1.0  
**�ltima actualizaci�n**: 2024  
**Autor**: Equipo de Desarrollo
