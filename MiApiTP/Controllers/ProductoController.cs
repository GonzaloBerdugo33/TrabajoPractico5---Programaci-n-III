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
            var productosDto = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock,
                ImagenUrl = p.ImagenUrl,
                CategoriaNombre = p.Categoria?.Nombre ?? "Sin Categoría",
                ProveedorNombre = p.Proveedor?.RazonSocial ?? "Sin Proveedor"
            }).ToList();

            return Ok(productosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _service.ObtenerPorId(id);
            if (producto == null) return NotFound();

            var productoDto = new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                ImagenUrl = producto.ImagenUrl,
                CategoriaNombre = producto.Categoria?.Nombre ?? "Sin Categoría",
                ProveedorNombre = producto.Proveedor?.RazonSocial ?? "Sin Proveedor"
            };

            return Ok(productoDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Crear(CrearProductoDTO dto)
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

        [HttpPost("{id}/imagen")]
        [Authorize]
        public async Task<IActionResult> SubirImagen(int id, IFormFile archivo)
        {
            try
            {
                var rutaImagen = await _service.SubirImagen(id, archivo);
                return Ok(new { imagenUrl = rutaImagen });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Actualizar(int id, CrearProductoDTO dto)
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
