using GestionUsuarios.Api.Models;

namespace GestionUsuarios.Api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private static List<Usuario> _usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Juan Pérez", Email = "juan@ejemplo.com", Activo = true },
            new Usuario { Id = 2, Nombre = "María López", Email = "maria@ejemplo.com", Activo = true },
            new Usuario { Id = 3, Nombre = "Carlos Ruiz", Email = "carlos@ejemplo.com", Activo = false }
        };

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            //var prueba =  await ObtenerPorIdAsync(1);

            // Simulamos operación asíncrona
            return await Task.FromResult(_usuarios);
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            return await Task.FromResult(usuario);
        }

        public async Task<Usuario> CrearAsync(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (_usuarios.Any(u => u.Email == usuario.Email))
                throw new InvalidOperationException("Ya existe un usuario con ese email");

            var nuevoId = _usuarios.Count > 0 ? _usuarios.Max(u => u.Id) + 1 : 1;
            usuario.Id = nuevoId;
            _usuarios.Add(usuario);

            return await Task.FromResult(usuario);
        }

        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            var index = _usuarios.FindIndex(u => u.Id == usuario.Id);
            if (index < 0)
                return await Task.FromResult(false);

            _usuarios[index] = usuario;
            return await Task.FromResult(true);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var index = _usuarios.FindIndex(u => u.Id == id);
            if (index < 0)
                return await Task.FromResult(false);

            _usuarios.RemoveAt(index);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await Task.FromResult(_usuarios.Any(u => u.Email == email));
        }
    }
}
