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

        public async Task<SalidaDTO?> ObtenerDetalleAsync(int id)
        {
            var salidas = await _salidaRepository.ObtenerPorIdAsync(id);
            if (salidas == null) 
            {
                return null;
            }

            return new SalidaDTO
            {
                Id = salidas.Id,
                ClienteId = salidas.ClienteId,
                UsuarioId = salidas.UsuarioId,
                Fecha = salidas.Fecha,
                ClienteNombre = salidas.Cliente?.Nombre ?? "Desconocido",
                UsuarioEmail = salidas.Usuario?.Email ?? "Desconocido",
                MetodoPago = salidas.MetodoPago,
                DetallesProductos = salidas.DetallesProductos.Select(d => new SalidaDetalleDTO
                {
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre ?? "Desconocido",
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()
            };
        }

        public async Task<int> RegistrarSalidaAsync(CrearSalidaDTO dto, int usuarioId)
        {

            if (dto.ClienteId <= 0)
            {
                throw new Exception("Debe indicar un cliente válido.");
            }

            if (dto.Detalles == null || !dto.Detalles.Any())
            {
                throw new Exception("La salida debe contener al menos un detalle de producto.");
            }

            if (dto.Detalles.Any(d => d.Cantidad <= 0))
            {
                throw new ArgumentException("Las cantidades de los productos deben ser mayores a cero.");
            }

            var metodosValidos = new[] { "Efectivo", "Tarjeta", "Transferencia" };
            if (!metodosValidos.Contains(dto.MetodoPago)) 
            {
                throw new Exception("Método de pago inválido.");
            }

            var salida = new SalidaProducto
            {
                ClienteId = dto.ClienteId,
                UsuarioId = usuarioId,
                Fecha = DateTime.UtcNow,
                MetodoPago = dto.MetodoPago,
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

                if (producto.Stock < detalles.Cantidad)
                {
                    throw new Exception($"No se realizo la salida del producto {producto.Nombre} por falta de stock");
                }

                detalles.PrecioUnitario = producto.Precio;
                producto.Stock -= detalles.Cantidad;

            }

            await _salidaRepository.AgregarAsync(salida);

            return salida.Id;
        }

    }
}
