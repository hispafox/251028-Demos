using TodoApp.Api.DTOs;

namespace TodoApp.Api.Services;

/// <summary>
/// Interfaz para el servicio de gestión de tareas con persistencia
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Obtiene todas las tareas
    /// </summary>
    Task<IEnumerable<TodoItemDto>> GetAllAsync();

    /// <summary>
    /// Obtiene una tarea por su ID
    /// </summary>
    Task<TodoItemDto?> GetByIdAsync(int id);

    /// <summary>
    /// Crea una nueva tarea
    /// </summary>
    Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto);

    /// <summary>
    /// Actualiza una tarea existente
    /// </summary>
    Task<TodoItemDto?> UpdateAsync(int id, UpdateTodoItemDto dto);

    /// <summary>
    /// Elimina una tarea
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Obtiene todas las tareas completadas
    /// </summary>
    Task<IEnumerable<TodoItemDto>> GetCompletedAsync();

    /// <summary>
    /// Obtiene todas las tareas pendientes
    /// </summary>
    Task<IEnumerable<TodoItemDto>> GetPendingAsync();
}
