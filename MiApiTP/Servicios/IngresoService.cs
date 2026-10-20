using MiApiTP.Modelos;
using MiApiTP.Repositorios;

namespace MiApiTP.Servicios
{
    public class IngresoService 
    {
        private readonly IIngresoProductoRepository _ingresoRepository;
        private readonly IProductoRepository _productoRepository;

        public IngresoService(IIngresoProductoRepository ingresoRepository, IProductoRepository productoRespository ) 
        {
            _ingresoRepository = ingresoRepository;
            _productoRepository = productoRespository;
        }

        public async Task<List<IngresoProducto>> ListarIngresos()
        {
            return await _ingresoRepository.ObtenerTodosAsync();
        }

        public async Task<IngresoProducto> ObtenerPorId(int id)
        {
            return await _ingresoRepository.ObtenerPorIdAsync(id);
        }
        public async Task RegistrarIngreso(IngresoProducto ingreso) 
        {
            if (ingreso.DetallesIngresos == null || !ingreso.DetallesIngresos.Any())
            {
                throw new Exception("El ingreso debe contener al menos un detalle de producto.");
            }

            foreach (var detalles in ingreso.DetallesIngresos)
            {
                var producto = await _productoRepository.ObtenerPorIdAsync(detalles.ProductoId);

                if(producto == null) 
                {
                    throw new Exception($"El producto con ID {detalles.ProductoId} no existe en el sistema.");
                }
                producto.Stock += detalles.Cantidad;
                await _productoRepository.ActualizarAsync(producto);
            }
            await _ingresoRepository.AgregarAsync(ingreso);
        }
    }
}