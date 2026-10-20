using MiApiTP.Modelos;
using MiApiTP.Repositorios;

namespace MiApiTP.Servicios
{
    public class SalidaService
    {
        private readonly ISalidaProductoRepository _salidaRepository;
        private readonly IProductoRepository _productoRepository;

        public SalidaService(ISalidaProductoRepository salidaRepository, IProductoRepository productoRepository)
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
            if (salida.DetallesProductos == null || !salida.DetallesProductos.Any())
            {
                throw new Exception("La salida debe contener al menos un detalle de producto.");
            }

            foreach (var detalles in salida.DetallesProductos)
            {
                var producto = await _productoRepository.ObtenerPorIdAsync(detalles.ProductoId);

                if (producto == null)
                {
                    throw new Exception($"El producto con ID {detalles.ProductoId} no existe.");
                }

                Console.WriteLine($"--- DEBUG --- Producto: {producto.Nombre} | Stock leído de BD: {producto.Stock} | Cantidad a restar: {detalles.Cantidad}");

                if (producto.Stock < detalles.Cantidad)
                {
                    throw new Exception($"No se realizo la salida del producto {producto.Nombre} por falta de stock");
                }
                detalles.PrecioUnitario = producto.Precio;
                producto.Stock -= detalles.Cantidad;

                Console.WriteLine($"--- DEBUG --- Nuevo stock calculado a guardar: {producto.Stock}");

                await _productoRepository.ActualizarAsync(producto);
            }

            await _salidaRepository.AgregarAsync(salida);
        }
    }
}
