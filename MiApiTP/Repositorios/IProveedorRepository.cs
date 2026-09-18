using MiApiTP.Data;
using MiApiTP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiTP.Repositorios
{
    public interface IProveedorRepository
    {
        Task<List<Proveedor>> ObtenerTodosAsync();
        Task<Proveedor> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Proveedor proveedor);
        Task ActualizarAsync(Proveedor proveedor);
        Task EliminarAsync(int id);

    }
}
