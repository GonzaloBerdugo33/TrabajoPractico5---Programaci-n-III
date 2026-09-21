using MiApiTP.DTOs;
using MiApiTP.Modelos;
using MiApiTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize]
        public async Task<IActionResult> Crear(CrearProductoDTOs dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                ImagenUrl = dto.ImagenUrl,
                CategoriaId = dto.CategoriaId,
                ProveedorId = dto.ProveedorId,
            };
            await _service.CrearProducto(producto);
            return Ok(producto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Actualizar(int id, CrearProductoDTOs dto)
        {
            var producto = new Producto
            {
                Id = id,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                ImagenUrl = dto.ImagenUrl,
                CategoriaId = dto.CategoriaId,
                ProveedorId = dto.ProveedorId
            };
            
            await _service.ActualizarProducto(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
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
