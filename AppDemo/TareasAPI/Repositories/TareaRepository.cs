using TareasAPI.Models;

namespace TareasAPI.Repositories;

public class TareaRepository : ITareaRepository
{
    private readonly List<Tarea> _tareas = new();
    private int _nextId = 1;

    public TareaRepository()
    {
        // Datos de ejemplo
        _tareas.Add(new Tarea
        {
            Id = _nextId++,
            Descripcion = "Completar documentación del proyecto",
            FechaInicio = DateTime.UtcNow,
            FechaLimite = DateTime.UtcNow.AddDays(5),
            FechaCreacion = DateTime.UtcNow,
            Completada = false
        });

        _tareas.Add(new Tarea
        {
            Id = _nextId++,
            Descripcion = "Revisar código del equipo",
            FechaInicio = DateTime.UtcNow,
            FechaLimite = DateTime.UtcNow.AddDays(2),
            FechaCreacion = DateTime.UtcNow,
            Completada = false
        });

        _tareas.Add(new Tarea
        {
            Id = _nextId++,
            Descripcion = "Preparar presentación para cliente",
            FechaInicio = DateTime.UtcNow,
            FechaLimite = DateTime.UtcNow.AddDays(7),
            FechaCreacion = DateTime.UtcNow,
            Completada = true
        });
    }

    public Task<IEnumerable<Tarea>> ObtenerTodasAsync()
    {
        return Task.FromResult(_tareas.AsEnumerable());
    }

    public Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(tarea);
    }

    public Task<Tarea> CrearAsync(Tarea tarea)
    {
        tarea.Id = _nextId++;
        // FechaCreacion ya debe estar establecida en el controlador
        // No necesitamos validar ni modificar FechaInicio aquí, ya viene del DTO
        _tareas.Add(tarea);
        return Task.FromResult(tarea);
    }

    public Task<Tarea?> ActualizarAsync(int id, Tarea tareaActualizada)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        if (tarea == null)
            return Task.FromResult<Tarea?>(null);

        tarea.Descripcion = tareaActualizada.Descripcion;
        tarea.FechaLimite = tareaActualizada.FechaLimite;
        tarea.FechaInicio = tareaActualizada.FechaInicio;
        tarea.Completada = tareaActualizada.Completada;
        // FechaCreacion nunca se actualiza

        return Task.FromResult<Tarea?>(tarea);
    }

    public Task<bool> EliminarAsync(int id)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        if (tarea == null)
            return Task.FromResult(false);

        _tareas.Remove(tarea);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada)
    {
        var tareas = _tareas.Where(t => t.Completada == completada);
        return Task.FromResult(tareas.AsEnumerable());
    }
}
