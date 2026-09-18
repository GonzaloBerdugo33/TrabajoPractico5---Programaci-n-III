using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MiApiTPContext _contex;

        public ProductoRepository(MiApiTPContext context)
        {
            _contex = context;
        }

        public async Task<List<Producto>> ObtenerTodosAsync() 
        {
            return await _contex.Productos.ToListAsync();
        }

        public async Task<List<Producto>> ObtenerPaginadoAsync(int pagina, int tamanoPagina)
        {
            return await _contex.Productos
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync();
        }

        public async Task<Producto> ObtenerPorIdAsync(int id) 
        {
            return await _contex.Productos.FindAsync(id);
        }

        public async Task AgregarAsync(Producto producto)
        {
            _contex.Productos.Add(producto);
            await _contex.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto producto) 
        {
            _contex.Productos.Update(producto);
            await _contex.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id) 
        {
            var producto = await _contex.Productos.FindAsync(id);
            if (producto != null) {
                _contex.Productos.Remove(producto);
                await _contex.SaveChangesAsync();
            }
        }
    }
}
