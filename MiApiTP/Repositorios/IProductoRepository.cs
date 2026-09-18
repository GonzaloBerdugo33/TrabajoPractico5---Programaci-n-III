using Microsoft.EntityFrameworkCore;
using MiApiTP.Modelos;

namespace MiApiTP.Repositorios
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Producto producto);
        Task ActualizarAsync(Producto producto);
        Task EliminarAsync(int id);
    }
}
