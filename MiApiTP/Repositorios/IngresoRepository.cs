using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public class IngresoRepository : IIngresoProductoRepository
    {
        private readonly MiApiTPContext _context;

        public IngresoRepository(MiApiTPContext context) 
        {
            _context = context;
        }

        public async Task<List<IngresoProducto>> ObtenerTodosAsync()
        {
            return await _context.Ingresos.ToListAsync();
        }

        public async Task<IngresoProducto> ObtenerPorIdAsync(int id) 
        {
            return await _context.Ingresos.FindAsync(id);
        }

        public async Task AgregarAsync(IngresoProducto ingreso)
        {
            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();
        }
    }
}
