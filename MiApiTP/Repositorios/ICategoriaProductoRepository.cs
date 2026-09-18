using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public interface ICategoriaProductoRepository
    {
        Task<List<CategoriaProducto>> ObtenerTodosAsync();
        Task<CategoriaProducto> ObtenerPorIdAsync(int id);
        Task AgregarAsync(CategoriaProducto categoria);
        Task ActualizarAsync(CategoriaProducto categoria);
        Task EliminarAsync(int id);

    }
}
