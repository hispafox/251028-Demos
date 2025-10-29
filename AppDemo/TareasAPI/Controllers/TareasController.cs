using Microsoft.AspNetCore.Mvc;
using TareasAPI.DTOs;
using TareasAPI.Models;
using TareasAPI.Repositories;

namespace TareasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly ITareaRepository _repository;
    private readonly ILogger<TareasController> _logger;

    public TareasController(ITareaRepository repository, ILogger<TareasController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las tareas
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerTodas()
    {
        var tareas = await _repository.ObtenerTodasAsync();
        return Ok(tareas);
    }

    /// <summary>
    /// Obtiene una tarea por su ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tarea>> ObtenerPorId(int id)
    {
        var tarea = await _repository.ObtenerPorIdAsync(id);
        
        if (tarea == null)
        {
            _logger.LogWarning("Tarea con ID {Id} no encontrada", id);
            return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
        }

        return Ok(tarea);
    }

    /// <summary>
    /// Obtiene tareas filtradas por estado
    /// </summary>
    [HttpGet("estado/{completada}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerPorEstado(bool completada)
    {
        var tareas = await _repository.ObtenerPorEstadoAsync(completada);
        return Ok(tareas);
    }

    /// <summary>
    /// Crea una nueva tarea
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Tarea>> Crear([FromBody] CrearTareaDto tareaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validar que la fecha de inicio no sea en el pasado
        if (tareaDto.FechaInicio.HasValue && tareaDto.FechaInicio.Value < DateTime.UtcNow)
        {
            return BadRequest(new { mensaje = "La fecha de inicio no puede ser en el pasado" });
        }

        // Validar que la fecha límite no sea en el pasado
        if (tareaDto.FechaLimite.HasValue && tareaDto.FechaLimite.Value < DateTime.UtcNow)
        {
            return BadRequest(new { mensaje = "La fecha límite no puede ser en el pasado" });
        }

        var tarea = new Tarea
        {
            Descripcion = tareaDto.Descripcion,
            FechaLimite = tareaDto.FechaLimite!.Value,
            FechaInicio = tareaDto.FechaInicio!.Value,
            FechaCreacion = DateTime.UtcNow,
            ProjectId = tareaDto.ProjectId
        };

        var tareaCreada = await _repository.CrearAsync(tarea);
        _logger.LogInformation("Tarea creada con ID {Id}", tareaCreada.Id);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = tareaCreada.Id }, tareaCreada);
    }

    /// <summary>
    /// Actualiza una tarea existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tarea>> Actualizar(int id, [FromBody] ActualizarTareaDto tareaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var tareaExistente = await _repository.ObtenerPorIdAsync(id);
        if (tareaExistente == null)
        {
            return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
        }

        // Actualizar solo los campos que se proporcionaron
        if (tareaDto.Descripcion != null)
            tareaExistente.Descripcion = tareaDto.Descripcion;

        if (tareaDto.FechaInicio.HasValue)
        {
            if (tareaDto.FechaInicio.Value < DateTime.UtcNow)
            {
                return BadRequest(new { mensaje = "La fecha de inicio no puede ser en el pasado" });
            }
            tareaExistente.FechaInicio = tareaDto.FechaInicio.Value;
        }

        if (tareaDto.FechaLimite.HasValue)
        {
            if (tareaDto.FechaLimite.Value < DateTime.UtcNow)
            {
                return BadRequest(new { mensaje = "La fecha límite no puede ser en el pasado" });
            }
            tareaExistente.FechaLimite = tareaDto.FechaLimite.Value;
        }

        if (tareaDto.Completada.HasValue)
            tareaExistente.Completada = tareaDto.Completada.Value;

        // allow updating ProjectId
        if (tareaDto.ProjectId.HasValue)
            tareaExistente.ProjectId = tareaDto.ProjectId.Value;

        var tareaActualizada = await _repository.ActualizarAsync(id, tareaExistente);
        _logger.LogInformation("Tarea con ID {Id} actualizada", id);

        return Ok(tareaActualizada);
    }

    /// <summary>
    /// Elimina una tarea
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _repository.EliminarAsync(id);
        
        if (!eliminado)
        {
            return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
        }

        _logger.LogInformation("Tarea con ID {Id} eliminada", id);
        return NoContent();
    }

    /// <summary>
    /// Marca una tarea como completada
    /// </summary>
    [HttpPatch("{id}/completar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tarea>> MarcarComoCompletada(int id)
    {
        var tarea = await _repository.ObtenerPorIdAsync(id);
        
        if (tarea == null)
        {
            return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
        }

        tarea.Completada = true;
        var tareaActualizada = await _repository.ActualizarAsync(id, tarea);
        _logger.LogInformation("Tarea con ID {Id} marcada como completada", id);

        return Ok(tareaActualizada);
    }

    // Assign project to task
    [HttpPost("{taskId}/assign-project")]
    public async Task<IActionResult> AssignProject(int taskId, [FromBody] AssignProjectDto dto)
    {
        // validate task and project existence
        var tarea = await _repository.ObtenerPorIdAsync(taskId);
        if (tarea == null) return NotFound(new { mensaje = $"No se encontró la tarea con ID {taskId}" });

        // check project exists in ProjectsController in-memory store
        var proyectosType = typeof(ProjectsController);
        var field = proyectosType.GetField("_projects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        if (field == null) return BadRequest(new { mensaje = "No se pudo validar el proyecto" });
        var proyectos = (List<Project>?)field.GetValue(null);
        if (proyectos == null || !proyectos.Any(p => p.Id == dto.ProjectId)) return NotFound(new { mensaje = $"No se encontró el proyecto con ID {dto.ProjectId}" });

        tarea.ProjectId = dto.ProjectId;
        await _repository.ActualizarAsync(taskId, tarea);
        return NoContent();
    }

    [HttpPost("{taskId}/remove-project")]
    public async Task<IActionResult> RemoveProject(int taskId)
    {
        var tarea = await _repository.ObtenerPorIdAsync(taskId);
        if (tarea == null) return NotFound(new { mensaje = $"No se encontró la tarea con ID {taskId}" });

        tarea.ProjectId = null;
        await _repository.ActualizarAsync(taskId, tarea);
        return NoContent();
    }
}
