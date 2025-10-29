using Microsoft.AspNetCore.Mvc;
using TareasAPI.Models;

namespace TareasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
 private static readonly List<Project> _projects = new();
 private static int _nextId =1;

 [HttpGet]
 public ActionResult<IEnumerable<Project>> GetAll()
 {
 return Ok(_projects);
 }

 [HttpGet("{id}")]
 public ActionResult<Project> Get(int id)
 {
 var p = _projects.FirstOrDefault(x => x.Id == id);
 if (p == null) return NotFound();
 return Ok(p);
 }

 [HttpPost]
 public ActionResult<Project> Create([FromBody] Project project)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);
 project.Id = _nextId++;
 project.CreatedAt = DateTime.UtcNow;
 _projects.Add(project);
 return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
 }

 [HttpPut("{id}")]
 public ActionResult<Project> Update(int id, [FromBody] Project project)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);
 var existing = _projects.FirstOrDefault(x => x.Id == id);
 if (existing == null) return NotFound();
 existing.Name = project.Name;
 existing.Description = project.Description;
 existing.UpdatedAt = DateTime.UtcNow;
 return Ok(existing);
 }

 [HttpDelete("{id}")]
 public IActionResult Delete(int id)
 {
 var existing = _projects.FirstOrDefault(x => x.Id == id);
 if (existing == null) return NotFound();

 // On delete, set ProjectId = null for tasks in memory store if present
 var tareasRepoType = typeof(TareasAPI.Repositories.TareaRepository);
 var tareasField = tareasRepoType.GetField("_tareas", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
 if (tareasField != null)
 {
 var tareas = (List<Tarea>?)tareasField.GetValue(null);
 if (tareas != null)
 {
 foreach (var t in tareas.Where(x => x.ProjectId == id))
 {
 t.ProjectId = null;
 }
 }
 }

 _projects.Remove(existing);
 return NoContent();
 }

 [HttpGet("{id}/tasks")]
 public ActionResult<IEnumerable<Tarea>> GetTasks(int id)
 {
 var project = _projects.FirstOrDefault(x => x.Id == id);
 if (project == null) return NotFound();

 var tareasRepoType = typeof(TareasAPI.Repositories.TareaRepository);
 var tareasField = tareasRepoType.GetField("_tareas", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
 if (tareasField == null) return Ok(Array.Empty<Tarea>());

 var tareas = (List<Tarea>?)tareasField.GetValue(null) ?? new List<Tarea>();
 var result = tareas.Where(x => x.ProjectId == id).ToList();
 return Ok(result);
 }
}
