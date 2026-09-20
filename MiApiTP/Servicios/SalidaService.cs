using MiApiTP.Modelos;
using MiApiTP.Repositorios;

namespace MiApiTP.Servicios
{
    public class SalidaService
    {
        private readonly ISalidaProductoRepository _salidaRepository;
        private readonly IProductoRepository _productoRepository;

        public SalidaService (ISalidaProductoRepository salidaRepository, IProductoRepository productoRepository) 
        {
            _salidaRepository = salidaRepository;
            _productoRepository = productoRepository;
        }

        public async Task<List<SalidaProducto>> ListarSalidas()
        {
            return await _salidaRepository.ObtenerTodosAsync();
        }

        public async Task<SalidaProducto> ObtenerPorId(int id)
        {
            return await _salidaRepository.ObtenerPorIdAsync(id);
        }

        public async Task RegistrarSalida(SalidaProducto salida) 
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(salida.ProductoId);
            if(producto.Stock < salida.Cantidad)
            {
                throw new Exception("No se realizo la salida de producto por falta de stock");
            }
            else 
            {
                producto.Stock -= salida.Cantidad;
                await _productoRepository.ActualizarAsync(producto);
                await _salidaRepository.AgregarAsync(salida);
            }

        }
    }
}
