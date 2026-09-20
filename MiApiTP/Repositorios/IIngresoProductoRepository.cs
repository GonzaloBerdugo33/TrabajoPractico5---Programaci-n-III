using MiApiTP.Modelos;

namespace MiApiTP.Repositorios
{
    public interface IIngresoProductoRepository
    {
        Task<List<IngresoProducto>> ObtenerTodosAsync();
        Task<IngresoProducto> ObtenerPorIdAsync(int id);
        Task AgregarAsync(IngresoProducto ingreso);
    }
}