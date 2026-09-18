using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MiApiTPContext _context;

        public ClienteRepository(MiApiTPContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ObtenerTodosAsync() 
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<List<Cliente>> ObtenerPaginadoAsync(int pagina, int tamanoPagina)
        {
            return await _context.Clientes
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();
        }

        public async Task<Cliente> ObtenerPorIdAsync(int id) 
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task AgregarAsync(Cliente cliente) 
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Cliente cliente) 
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id) 
        {
            var tieneSalidas = await _context.Salidas.AnyAsync(s => s.ClienteId == id);
            if (tieneSalidas)
            {
                throw new Exception("No se puede eliminar cliente, esta asociado a una salida de producto");
            }
            else 
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if(cliente != null) 
                {
                    _context.Clientes.Remove(cliente);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
