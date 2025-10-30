namespace TodoApp.Api.Models;

/// <summary>
/// Representa un ítem de tarea en el sistema.
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Identificador único de la tarea.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título descriptivo de la tarea.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
/// Indica si la tarea está completada.
    /// </summary>
    public bool IsComplete { get; set; }
}
