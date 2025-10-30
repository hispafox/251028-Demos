namespace TodoApp.Api.Data.Entities
{
    /// <summary>
    /// Entidad de dominio para representar una tarea en la base de datos
    /// </summary>
    public class TodoEntity
    {
    /// <summary>
        /// Identificador �nico de la tarea
   /// </summary>
public int Id { get; set; }
     
  /// <summary>
        /// T�tulo descriptivo de la tarea
      /// </summary>
    public string Title { get; set; } = string.Empty;
   
        /// <summary>
    /// Indica si la tarea est� completada
        /// </summary>
        public bool IsComplete { get; set; }
   
        /// <summary>
        /// Fecha y hora de creaci�n de la tarea (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Fecha y hora de la �ltima actualizaci�n (UTC). Null si nunca se actualiz�
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
