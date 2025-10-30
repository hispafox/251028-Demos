using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services;

namespace TodoApp.Api.Controllers;

/// <summary>
/// Controlador para la gestión de tareas con persistencia
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
    /// Obtiene todas las tareas
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
    {
        var items = await _todoService.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Obtiene todas las tareas completadas
    /// </summary>
    [HttpGet("completed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetCompleted()
    {
      var items = await _todoService.GetCompletedAsync();
        return Ok(items);
    }

    /// <summary>
    /// Obtiene todas las tareas pendientes
    /// </summary>
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetPending()
    {
        var items = await _todoService.GetPendingAsync();
        return Ok(items);
    }

    /// <summary>
    /// Obtiene una tarea específica por ID
  /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemDto>> GetById(int id)
    {
     var item = await _todoService.GetByIdAsync(id);
        if (item == null)
   return NotFound();

   return Ok(item);
  }

    /// <summary>
    /// Crea una nueva tarea
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemDto dto)
 {
        if (!ModelState.IsValid)
        return BadRequest(ModelState);

        try
        {
  var newItem = await _todoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }
        catch (ArgumentException ex)
        {
    return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza una tarea existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemDto>> Update(int id, [FromBody] UpdateTodoItemDto dto)
    {
        if (!ModelState.IsValid)
    return BadRequest(ModelState);

        try
        {
      var updatedItem = await _todoService.UpdateAsync(id, dto);
            if (updatedItem == null)
    return NotFound();

            return Ok(updatedItem);
        }
        catch (ArgumentException ex)
  {
    return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Elimina una tarea
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
     var result = await _todoService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
