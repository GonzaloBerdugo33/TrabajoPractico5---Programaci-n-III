using MiApiTP.DTOs;
using MiApiTP.Modelos;
using MiApiTP.Repositorios;

namespace MiApiTP.Servicios
{
    public class IngresoService
    {
        private readonly IIngresoProductoRepository _ingresoRepository;
        private readonly IProductoRepository _productoRepository;

        public IngresoService(IIngresoProductoRepository ingresoRepository, IProductoRepository productoRespository)
        {
            _ingresoRepository = ingresoRepository;
            _productoRepository = productoRespository;
        }

        public async Task<List<ListarIngresoDTO>> ListarIngresosAsync()
        {

            var ingresos = await _ingresoRepository.ObtenerTodosAsync();

            var dtos = ingresos.Select(i => new ListarIngresoDTO
            {
                Id = i.Id,
                Fecha = i.Fecha,
                ProveedorRazonSocial = i.Proveedor?.RazonSocial ?? "Desconocido",
                UsuarioEmail = i.Usuario?.Email ?? "Desconocido",
                TotalItems = i.DetallesIngresos?.Sum(d => d.Cantidad) ?? 0
            }).ToList();

            return dtos;
        }

        public async Task<IngresoProducto> ObtenerPorId(int id)
        {
            return await _ingresoRepository.ObtenerPorIdAsync(id);
        }

        public async Task<int> RegistrarIngresoAsync(CrearIngresoDTO dto, int usuarioId)
        {
            if (dto.ProveedorId <= 0)
            {
                throw new Exception("Debe indicar un proveedor válido.");
            }

            if (dto.Detalles == null || !dto.Detalles.Any())
            {
                throw new Exception("El ingreso debe contener al menos un detalle de producto.");
            }

            if (dto.Detalles.Any(d => d.Cantidad <= 0))
            {
                throw new ArgumentException("Las cantidades de los productos deben ser mayores a cero.");
            }

            var ingreso = new IngresoProducto
            {
                ProveedorId = dto.ProveedorId,
                UsuarioId = usuarioId,
                Fecha = DateTime.UtcNow,
                DetallesIngresos = dto.Detalles.Select(d => new IngresoDetalle
                {
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad
                }).ToList()
            };

            foreach (var detalles in ingreso.DetallesIngresos)
            {
                var producto = await _productoRepository.ObtenerPorIdAsync(detalles.ProductoId);

                if (producto == null)
                {
                    throw new Exception($"El producto con ID {detalles.ProductoId} no existe.");
                }

                producto.Stock += detalles.Cantidad;

            }

            await _ingresoRepository.AgregarAsync(ingreso);

            return ingreso.Id;
        }
    }
}