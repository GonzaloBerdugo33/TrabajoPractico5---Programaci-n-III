using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MiApiTPContext _context;

        public ProductoRepository(MiApiTPContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ObtenerTodosAsync() 
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<List<Producto>> ObtenerPaginadoAsync(int pagina, int tamanoPagina)
        {
            return await _context.Productos
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync();
        }

        public async Task<Producto> ObtenerPorIdAsync(int id) 
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AgregarAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto producto) 
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id) 
        {
           bool tieneSalidas = await _context.Salidas.AnyAsync(s => s.ProductoId == id);
           bool tieneIngresos = await _context.Ingresos.AnyAsync(i => i.ProductoId == id);

            if (tieneSalidas || tieneIngresos) 
            {
                throw new Exception("No se puede eliminar producto porque cuenta con Salidas o Ingresos");
            }
            else
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto != null)
                {
                    _context.Productos.Remove(producto);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
