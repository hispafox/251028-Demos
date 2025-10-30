using System.ComponentModel.DataAnnotations;

namespace TareasAPI.Models;

public class Project
{
 public int Id { get; set; }

 [Required]
 [StringLength(200)]
 public string Name { get; set; } = string.Empty;

 public string? Description { get; set; }

 [Required]
 public DateTime CreatedAt { get; set; }

 public DateTime? UpdatedAt { get; set; }

 // Navigation
 public ICollection<Tarea>? Tareas { get; set; }
}
