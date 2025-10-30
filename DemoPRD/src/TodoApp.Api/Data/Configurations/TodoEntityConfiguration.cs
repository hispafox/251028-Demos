using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Api.Data.Entities;

namespace TodoApp.Api.Data.Configurations
{
/// <summary>
/// Configuraci�n de Fluent API para la entidad TodoEntity
    /// </summary>
    public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
 {
        public void Configure(EntityTypeBuilder<TodoEntity> builder)
        {
            // Nombre de la tabla
  builder.ToTable("Todos");
        
  // Clave primaria
   builder.HasKey(t => t.Id);
      
      // Configuraci�n de Id
        builder.Property(t => t.Id)
      .ValueGeneratedOnAdd()
        .IsRequired();
            
       // Configuraci�n de Title
   builder.Property(t => t.Title)
     .IsRequired()
     .HasMaxLength(200);
            
   // Configuraci�n de IsComplete
      builder.Property(t => t.IsComplete)
   .IsRequired()
 .HasDefaultValue(false);
            
            // Configuraci�n de CreatedAt
      builder.Property(t => t.CreatedAt)
        .IsRequired();
           // Para SQL Server: .HasDefaultValueSql("GETUTCDATE()")
   // Para SQLite usaremos DateTime.UtcNow en c�digo
         
            // Configuraci�n de UpdatedAt
     builder.Property(t => t.UpdatedAt)
      .IsRequired(false);

// �ndices para mejorar rendimiento
            builder.HasIndex(t => t.IsComplete)
          .HasDatabaseName("IX_Todos_IsComplete");
         
         builder.HasIndex(t => t.CreatedAt)
             .HasDatabaseName("IX_Todos_CreatedAt");
        }
    }
}
