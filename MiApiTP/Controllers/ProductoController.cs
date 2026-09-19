using Microsoft.AspNetCore.Mvc;
using MiApiTP.Modelos;
using MiApiTP.Servicios;
using System.Linq.Expressions;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _service;

        public ProductoController(ProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPaginado(int pagina = 1, int tamanoPagina = 10)
        {
            var productos = await _service.ObtenerPaginado(pagina, tamanoPagina);
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _service.ObtenerPorId(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Producto producto)
        {
            await _service.CrearProducto(producto);
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            await _service.ActualizarProducto(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarProducto(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
