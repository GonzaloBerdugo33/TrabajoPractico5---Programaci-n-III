using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MiApiTPContext _context;

        public UsuarioRepository(MiApiTPContext context) 
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerTodosAsync() 
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id) 
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> ObtenerPorEmailAsync(string email) 
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AgregarAsync(Usuario usuario) 
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Usuario usuario) 
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id) 
        {
            bool tieneIngresos = await _context.Ingresos.AnyAsync(i => i.UsuarioId == id);
            bool tieneSalidas = await _context.Salidas.AnyAsync(s => s.UsuarioId == id);
            if (tieneIngresos || tieneSalidas) 
            {
                throw new Exception("No se puede eliminar Usuario porque tiene Salidas e Ingresos asociados");
            }
            else 
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if(usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
