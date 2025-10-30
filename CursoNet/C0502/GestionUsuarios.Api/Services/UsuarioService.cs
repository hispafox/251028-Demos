using GestionUsuarios.Api.Models;
using GestionUsuarios.Api.Repositories;

namespace GestionUsuarios.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));

            return await _repository.ObtenerPorIdAsync(id);
        }

        public async Task<Usuario> CrearAsync(Usuario usuario)
        {
            ValidarUsuario(usuario);

            if (await _repository.ExisteEmailAsync(usuario.Email))
                throw new InvalidOperationException("Ya existe un usuario con ese email");

            return await _repository.CrearAsync(usuario);
        }

        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (usuario.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero", nameof(usuario.Id));

            var usuarioExistente = await _repository.ObtenerPorIdAsync(usuario.Id);
            if (usuarioExistente == null)
                return false;

            // Comprobar que no se intenta cambiar el email a uno ya existente
            if (usuarioExistente.Email != usuario.Email && await _repository.ExisteEmailAsync(usuario.Email))
                throw new InvalidOperationException("Ya existe otro usuario con ese email");

            ValidarUsuario(usuario);
            return await _repository.ActualizarAsync(usuario);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));

            return await _repository.EliminarAsync(id);
        }

        private void ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío", nameof(usuario.Nombre));

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new ArgumentException("El email no puede estar vacío", nameof(usuario.Email));

            if (!usuario.Email.Contains("@"))
                throw new ArgumentException("El formato del email no es válido", nameof(usuario.Email));
        }
    }
}
