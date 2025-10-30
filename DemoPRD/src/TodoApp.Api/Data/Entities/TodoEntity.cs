namespace TodoApp.Api.Data.Entities
{
    /// <summary>
    /// Entidad de dominio para representar una tarea en la base de datos
    /// </summary>
    public class TodoEntity
    {
    /// <summary>
        /// Identificador único de la tarea
   /// </summary>
public int Id { get; set; }
     
  /// <summary>
        /// Título descriptivo de la tarea
      /// </summary>
    public string Title { get; set; } = string.Empty;
   
        /// <summary>
    /// Indica si la tarea está completada
        /// </summary>
        public bool IsComplete { get; set; }
   
        /// <summary>
        /// Fecha y hora de creación de la tarea (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Fecha y hora de la última actualización (UTC). Null si nunca se actualizó
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
