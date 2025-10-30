using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.DTOs
{
    /// <summary>
    /// DTO para creaci�n de nuevas tareas
    /// </summary>
    public class CreateTodoItemDto
    {
        [Required(ErrorMessage = "El t�tulo es requerido")]
        [StringLength(200, MinimumLength = 1, 
     ErrorMessage = "El t�tulo debe tener entre 1 y 200 caracteres")]
        public string Title { get; set; } = string.Empty;
        
      public bool IsComplete { get; set; } = false;
  }
}
