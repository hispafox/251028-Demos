using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data.Configurations;
using TodoApp.Api.Data.Entities;
using TodoApp.Api.Data.Seeders;

namespace TodoApp.Api.Data
{
    /// <summary>
    /// Contexto de base de datos para TodoApp
    /// </summary>
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
         
       // Aplicar configuraciones Fluent API
     modelBuilder.ApplyConfiguration(new TodoEntityConfiguration());
       
    // Seed data eliminado - se maneja en tiempo de ejecución en Program.cs
            // para evitar problemas de modelo no determinístico con datos dinámicos
        }
    }
}
