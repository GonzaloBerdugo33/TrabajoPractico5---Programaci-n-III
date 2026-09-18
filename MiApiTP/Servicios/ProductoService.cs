using MiApiTP.Modelos;
using MiApiTP.Repositorios;

namespace MiApiTP.Servicios
{
    public class ProductoService
    {
        private readonly IProductoRepository _repositorio;

        public ProductoService(IProductoRepository repositorio) 
        {
            _repositorio = repositorio;
        }

        public async Task<List<Producto>> ObtenerPaginado(int pagina, int tamanoPagina) 
        {
            return await _repositorio.ObtenerPaginadoAsync(pagina, tamanoPagina);
        }

        public async Task<Producto> ObtenerPorId(int id) 
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task CrearProducto(Producto producto) 
        {
            await _repositorio.AgregarAsync(producto);
        }

        public async Task ActualizarProducto(Producto producto) 
        {
            await _repositorio.ActualizarAsync(producto);
        }

        public async Task EliminarProducto(int id) 
        {
            await _repositorio.EliminarAsync(id);
        }
    }
}
