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
            var producto = await _productoRepository.ObtenerPorIdAsync(ingreso.ProductoId);
            producto.Stock += ingreso.Cantidad;
            await _productoRepository.ActualizarAsync(producto);
            await _ingresoRepository.AgregarAsync(ingreso);
            
        }
    }
}