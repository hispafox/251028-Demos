using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.DTOs
{
    /// <summary>
    /// DTO para actualizaci�n de tareas existentes
    /// </summary>
    public class UpdateTodoItemDto
    {
        [Required(ErrorMessage = "El t�tulo es requerido")]
        [StringLength(200, MinimumLength = 1, 
     ErrorMessage = "El t�tulo debe tener entre 1 y 200 caracteres")]
  public string Title { get; set; } = string.Empty;
        
        public bool IsComplete { get; set; }
    }
}
