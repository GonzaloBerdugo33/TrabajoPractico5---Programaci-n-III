using MiApiTP.Modelos;
using MiApiTP.Repositorios;

namespace MiApiTP.Servicios
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repositorio;

        public UsuarioService(IUsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<Usuario> ValidarCredenciales(string email, string password)
        {
            var usuario = await _repositorio.ObtenerPorEmailAsync(email);
            if (usuario != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash))
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public async Task CrearUsuario(Usuario usuario)
        {
            await _repositorio.AgregarAsync(usuario);
        }

        public async Task ActualizarUsuario(Usuario usuario)
        {
            await _repositorio.ActualizarAsync(usuario);
        }

        public async Task EliminarUsuario(int id)
        {
            await _repositorio.EliminarAsync(id);
        }
    }
}