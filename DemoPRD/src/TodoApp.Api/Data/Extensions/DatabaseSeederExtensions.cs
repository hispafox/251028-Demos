using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data.Seeders;

namespace TodoApp.Api.Data.Extensions
{
    /// <summary>
    /// Métodos de extensión para seeding de base de datos
    /// </summary>
    public static class DatabaseSeederExtensions
    {
        /// <summary>
        /// Inicializa la base de datos con datos de prueba
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
/// <param name="clearExisting">Si debe limpiar datos existentes</param>
        public static async Task SeedDatabaseAsync(
 this TodoDbContext context, 
   bool clearExisting = false)
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
        /// Agrega más datos de prueba a la base de datos existente
 /// </summary>
        /// <param name="context">Contexto de base de datos</param>
   /// <param name="count">Cantidad de registros a agregar</param>
        public static async Task AddMoreSeedDataAsync(
            this TodoDbContext context, 
   int count = 50)
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
        
        /// <summary>
        /// Seed masivo para pruebas de rendimiento
        /// </summary>
  /// <param name="context">Contexto de base de datos</param>
        /// <param name="count">Cantidad total de registros</param>
  public static async Task BulkSeedAsync(
     this TodoDbContext context, 
            int count = 50000)
    {
   const int batchSize = 1000;
  var batches = count / batchSize;
         var maxId = await context.Todos.MaxAsync(t => (int?)t.Id) ?? 0;
            
        for (int i = 0; i < batches; i++)
 {
          var todos = TodoDataSeeder.GenerateTodos(batchSize, seed: i);
   
                // Ajustar IDs
          foreach (var todo in todos)
                {
                    todo.Id = ++maxId;
 }
     
 await context.Todos.AddRangeAsync(todos);
        await context.SaveChangesAsync();
          
          // Limpiar change tracker para liberar memoria
      context.ChangeTracker.Clear();
    
      Console.WriteLine($"? Batch {i + 1}/{batches} completado ({(i + 1) * batchSize} registros)");
            }
      }
    }
}
