using TareasAPI.Models;

namespace TareasAPI.Repositories;

public interface ITareaRepository
{
    Task<IEnumerable<Tarea>> ObtenerTodasAsync();
    Task<Tarea?> ObtenerPorIdAsync(int id);
    Task<Tarea> CrearAsync(Tarea tarea);
    Task<Tarea?> ActualizarAsync(int id, Tarea tarea);
    Task<bool> EliminarAsync(int id);
    Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada);
}
