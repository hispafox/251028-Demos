using TodoApp.Api.Models;

namespace TodoApp.Api.Services;

/// <summary>
/// Implementaci�n del servicio de gesti�n de tareas.
/// Utiliza almacenamiento en memoria.
/// </summary>
public class TodoService : ITodoService
{
    private readonly List<TodoItem> _todos = new();
    private int _nextId = 1;

  /// <summary>
    /// Obtiene todas las tareas.
    /// </summary>
    public IEnumerable<TodoItem> GetAll()
    {
        return _todos.ToList();
    }

    /// <summary>
    /// Obtiene una tarea por su ID.
    /// </summary>
    public TodoItem? GetById(int id)
    {
        return _todos.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Agrega una nueva tarea.
    /// </summary>
    /// <exception cref="ArgumentNullException">Si el item es null.</exception>
    /// <exception cref="ArgumentException">Si el t�tulo est� vac�o.</exception>
    public TodoItem Add(TodoItem item)
    {
        if (item == null)
    {
        throw new ArgumentNullException(nameof(item), "El item no puede ser nulo");
        }

        if (string.IsNullOrWhiteSpace(item.Title))
   {
            throw new ArgumentException("El t�tulo no puede estar vac�o", nameof(item));
        }

        item.Id = _nextId++;
        _todos.Add(item);
return item;
    }

    /// <summary>
    /// Actualiza una tarea existente.
    /// </summary>
    /// <exception cref="ArgumentNullException">Si el item es null.</exception>
    /// <exception cref="ArgumentException">Si el t�tulo est� vac�o.</exception>
    public TodoItem? Update(int id, TodoItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "El item no puede ser nulo");
        }

        if (string.IsNullOrWhiteSpace(item.Title))
        {
     throw new ArgumentException("El t�tulo no puede estar vac�o", nameof(item));
   }

        var existingTodo = GetById(id);
        if (existingTodo == null)
        {
  return null;
     }

        existingTodo.Title = item.Title;
        existingTodo.IsComplete = item.IsComplete;
 return existingTodo;
    }

    /// <summary>
    /// Elimina una tarea.
    /// </summary>
    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo == null)
        {
        return false;
   }

        _todos.Remove(todo);
        return true;
    }
}
