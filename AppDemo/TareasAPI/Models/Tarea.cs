using System.ComponentModel.DataAnnotations;

namespace TareasAPI.Models;

public class Tarea
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(500, MinimumLength = 5)]
    public string Descripcion { get; set; } = string.Empty;
    
    [Required]
    public DateTime FechaLimite { get; set; }
    
    public bool Completada { get; set; } = false;
    
    [Required]
    public DateTime FechaInicio { get; set; }
    
    [Required]
    public DateTime FechaCreacion { get; set; }

    // Project association (optional)
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
}
