using Microsoft.AspNetCore.Components.Forms;
using MiApiTP.Web.DTOs;

namespace MiApiTP.Web.Servicios.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>?> ObtenerPaginadoAsync(int pagina = 1, int tamanoPagina = 10);
        Task<ProductoDTO?> ObtenerPorIdAsync(int id);
        Task<bool> CrearProductoAsync(CrearProductoDTO dto);
        Task<bool> SubirImagenAsync(int productoId, IBrowserFile archivo);
        Task<bool> ActualizarProductoAsync(int Id, CrearProductoDTO dto);
        Task<bool> EliminarProductoAsync(int id);

    }
}
