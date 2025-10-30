using GestionUsuarios.Api.Models;
using GestionUsuarios.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerTodos()
        {
            var usuarios = await _service.ObtenerTodosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Usuario>> ObtenerPorId(int id)
        {
            var usuario = await _service.ObtenerPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Usuario>> Crear(Usuario usuario)
        {
            try
            {
                var nuevoUsuario = await _service.CrearAsync(usuario);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoUsuario.Id }, nuevoUsuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Actualizar(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del usuario");

            try
            {
                var resultado = await _service.ActualizarAsync(usuario);
                if (!resultado)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _service.EliminarAsync(id);
                if (!resultado)
                    return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
