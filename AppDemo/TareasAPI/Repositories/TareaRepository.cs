using TareasAPI.Models;

namespace TareasAPI.Repositories;

public class TareaRepository : ITareaRepository
{
    // make internal static so ProjectsController can access it for demo purposes
    internal static List<Tarea> _tareas = new();

    private int _nextId = 1;

    public TareaRepository()
    {
        // datos de ejemplo
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
    }

    public Task<IEnumerable<Tarea>> ObtenerTodasAsync()
    {
        return Task.FromResult<IEnumerable<Tarea>>(_tareas.AsEnumerable());
    }

    public Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(tarea);
    }

    public Task<Tarea> CrearAsync(Tarea tarea)
    {
        tarea.Id = _nextId++;
        // FechaCreacion ya debe estar establecida por el controlador en este diseño
        _tareas.Add(tarea);
        return Task.FromResult(tarea);
    }

    public Task<Tarea?> ActualizarAsync(int id, Tarea tarea)
    {
        var existing = _tareas.FirstOrDefault(t => t.Id == id);
        if (existing == null) return Task.FromResult<Tarea?>(null);

        existing.Descripcion = tarea.Descripcion;
        existing.FechaLimite = tarea.FechaLimite;
        existing.Completada = tarea.Completada;
        existing.FechaInicio = tarea.FechaInicio;
        existing.FechaCreacion = tarea.FechaCreacion;
        existing.ProjectId = tarea.ProjectId;

        return Task.FromResult(existing);
    }

    public Task<bool> EliminarAsync(int id)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        if (tarea == null) return Task.FromResult(false);
        _tareas.Remove(tarea);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada)
    {
        var res = _tareas.Where(t => t.Completada == completada).AsEnumerable();
        return Task.FromResult(res);
    }

    public Task<IEnumerable<Tarea>> ObtenerPorProjectIdAsync(int projectId)
    {
        var res = _tareas.Where(t => t.ProjectId == projectId).AsEnumerable();
        return Task.FromResult(res);
    }
}
