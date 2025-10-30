namespace TodoApp.Api.Models;

/// <summary>
/// Representa un �tem de tarea en el sistema.
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Identificador �nico de la tarea.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// T�tulo descriptivo de la tarea.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
/// Indica si la tarea est� completada.
    /// </summary>
    public bool IsComplete { get; set; }
}
