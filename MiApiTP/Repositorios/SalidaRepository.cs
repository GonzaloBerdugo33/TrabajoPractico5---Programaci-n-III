using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public class SalidaRepository : ISalidaProductoRepository
    {
        private readonly MiApiTPContext _context;

        public SalidaRepository(MiApiTPContext context)
        {
            _context = context;
        }

        public async Task<List<SalidaProducto>> ObtenerTodosAsync()
        {
            return await _context.Salidas
                .Include(s => s.Cliente)
                .Include(s => s.Usuario)
                .Include(s => s.DetallesProductos)
                .ToListAsync();
        }

        public async Task<SalidaProducto> ObtenerPorIdAsync(int id)
        {
            return await _context.Salidas
                .Include(s => s.DetallesProductos)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AgregarAsync(SalidaProducto salida)
        {
            _context.Salidas.Add(salida);
            await _context.SaveChangesAsync();
        }
    }
}
