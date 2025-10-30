using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.DTOs
{
    /// <summary>
    /// DTO para actualización de tareas existentes
    /// </summary>
    public class UpdateTodoItemDto
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, MinimumLength = 1, 
     ErrorMessage = "El título debe tener entre 1 y 200 caracteres")]
  public string Title { get; set; } = string.Empty;
        
        public bool IsComplete { get; set; }
    }
}
