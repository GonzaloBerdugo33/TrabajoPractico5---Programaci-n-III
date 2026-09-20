using MiApiTP.Modelos;

namespace MiApiTP.Repositorios
{
    public interface ISalidaProductoRepository
    {
        Task<List<SalidaProducto>> ObtenerTodosAsync();
        Task<SalidaProducto> ObtenerPorIdAsync(int id);
        Task AgregarAsync(SalidaProducto salida);
    }
}