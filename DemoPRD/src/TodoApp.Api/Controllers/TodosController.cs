using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models;
using TodoApp.Api.Services;

namespace TodoApp.Api.Controllers;

/// <summary>
/// Controlador para la gestión de tareas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    /// <summary>
    /// Obtiene todas las tareas.
    /// </summary>
    /// <returns>Lista de todas las tareas.</returns>
    /// <response code="200">Retorna la lista de tareas.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<TodoItem>> GetAll()
    {
      var todos = _todoService.GetAll();
        return Ok(todos);
  }

    /// <summary>
 /// Obtiene una tarea específica por ID.
    /// </summary>
    /// <param name="id">ID de la tarea.</param>
    /// <returns>La tarea solicitada.</returns>
    /// <response code="200">Retorna la tarea.</response>
    /// <response code="404">Si la tarea no existe.</response>
    [HttpGet("{id}")]
 [ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TodoItem> GetById(int id)
    {
   var todo = _todoService.GetById(id);
        if (todo == null)
   {
            return NotFound();
        }
        return Ok(todo);
    }

    /// <summary>
    /// Crea una nueva tarea.
    /// </summary>
    /// <param name="item">Datos de la nueva tarea.</param>
    /// <returns>La tarea creada.</returns>
    /// <response code="201">Retorna la tarea creada.</response>
    /// <response code="400">Si los datos son inválidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TodoItem> Create([FromBody] TodoItem item)
    {
        try
        {
         var createdItem = _todoService.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }
 catch (ArgumentException ex)
        {
          return BadRequest(ex.Message);
 }
    }

    /// <summary>
    /// Actualiza una tarea existente.
    /// </summary>
    /// <param name="id">ID de la tarea a actualizar.</param>
    /// <param name="item">Datos actualizados.</param>
    /// <returns>La tarea actualizada.</returns>
    /// <response code="200">Retorna la tarea actualizada.</response>
    /// <response code="400">Si los datos son inválidos.</response>
    /// <response code="404">Si la tarea no existe.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TodoItem> Update(int id, [FromBody] TodoItem item)
    {
        try
        {
            var updatedItem = _todoService.Update(id, item);
      if (updatedItem == null)
   {
   return NotFound();
        }
        return Ok(updatedItem);
  }
        catch (ArgumentException ex)
        {
 return BadRequest(ex.Message);
 }
    }

    /// <summary>
    /// Elimina una tarea.
    /// </summary>
    /// <param name="id">ID de la tarea a eliminar.</param>
    /// <returns>Sin contenido si se eliminó correctamente.</returns>
 /// <response code="204">Si la tarea se eliminó correctamente.</response>
    /// <response code="404">Si la tarea no existe.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var result = _todoService.Delete(id);
        if (!result)
        {
        return NotFound();
        }
        return NoContent();
    }
}
