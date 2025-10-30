using TodoApp.Api.Models;

namespace TodoApp.Api.Services;

/// <summary>
/// Interfaz para el servicio de gestión de tareas.
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Obtiene todas las tareas.
    /// </summary>
    /// <returns>Colección de todas las tareas.</returns>
    IEnumerable<TodoItem> GetAll();

    /// <summary>
    /// Obtiene una tarea por su ID.
    /// </summary>
    /// <param name="id">ID de la tarea.</param>
    /// <returns>La tarea si existe, null en caso contrario.</returns>
    TodoItem? GetById(int id);

    /// <summary>
    /// Agrega una nueva tarea.
    /// </summary>
    /// <param name="item">Tarea a agregar.</param>
    /// <returns>La tarea agregada con su ID asignado.</returns>
    TodoItem Add(TodoItem item);

    /// <summary>
    /// Actualiza una tarea existente.
  /// </summary>
    /// <param name="id">ID de la tarea a actualizar.</param>
    /// <param name="item">Datos actualizados de la tarea.</param>
    /// <returns>La tarea actualizada si existe, null en caso contrario.</returns>
    TodoItem? Update(int id, TodoItem item);

    /// <summary>
    /// Elimina una tarea.
    /// </summary>
    /// <param name="id">ID de la tarea a eliminar.</param>
    /// <returns>true si se eliminó correctamente, false si no existe.</returns>
    bool Delete(int id);
}
