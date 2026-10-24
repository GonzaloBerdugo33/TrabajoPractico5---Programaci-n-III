using MiApiTP.DTOs;
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

        public async Task<List<ListarSalidaDTO>> ListarSalidaAsync()
        {

            var salidas = await _salidaRepository.ObtenerTodosAsync();

            var dtos = salidas.Select(s => new ListarSalidaDTO
            {
                Id = s.Id,
                Fecha = s.Fecha,
                ClienteNombre = s.Cliente?.Nombre ?? "Desconocido",
                UsuarioEmail = s.Usuario?.Email ?? "Desconocido",
                TotalItems = s.DetallesProductos?.Sum(d => d.Cantidad) ?? 0
            }).ToList();

            return dtos;
        }

        public async Task<SalidaProducto> ObtenerPorId(int id)
        {
            return await _salidaRepository.ObtenerPorIdAsync(id);
        }

        public async Task RegistrarSalidaAsync(CrearSalidaDTO dto, int usuarioId)
        {
            if (dto.Detalles == null || !dto.Detalles.Any())
            {
                throw new Exception("La salida debe contener al menos un detalle de producto.");
            }

            var salida = new SalidaProducto
            {
                ClienteId = dto.ClienteId,
                UsuarioId = usuarioId,
                Fecha = DateTime.UtcNow,
                DetallesProductos = dto.Detalles.Select(d => new SalidaDetalle
                {
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad
                }).ToList()
            };

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
            }

            await _salidaRepository.AgregarAsync(salida);
        }

    }
}
