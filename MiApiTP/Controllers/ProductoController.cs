using MiApiTP.DTOs;
using MiApiTP.Modelos;
using MiApiTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _service;
        private readonly AuditoriaService _auditoria;

        public ProductoController(ProductoService service, AuditoriaService auditoria)
        {
            _service = service;
            _auditoria = auditoria;
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
        [Authorize(Roles = "Administrador,Cajero")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _service.ObtenerPorId(id);
            if (producto == null) return NotFound();

            var productoDto = new ProductoDTO
            {
                Id = producto.Id,
                CategoriaId = producto.CategoriaId,
                ProveedorId = producto.ProveedorId,
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
        [Authorize(Roles = "Administrador")]
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

            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            int? usuarioId = int.TryParse(idClaim, out var n) ? n : null;

            await _auditoria.Registrar("AltaProducto", "Producto", producto.Id,
                $"Alta de producto '{producto.Nombre}', precio {producto.Precio}, stock {producto.Stock}",
                usuarioId, email);

            var productoDto = new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                ImagenUrl = producto.ImagenUrl,
                CategoriaNombre = "...",
                ProveedorNombre = "..."
            };

            return Ok(productoDto);
        }

        [HttpPost("{id}/imagen")]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            int? usuarioId = int.TryParse(idClaim, out var n) ? n : null;

            await _auditoria.Registrar("EdicionProducto", "Producto", id,
                $"Edición de producto '{producto.Nombre}', precio {producto.Precio}, stock {producto.Stock}",
                usuarioId, email);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarProducto(id);

                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
                int? usuarioId = int.TryParse(idClaim, out var n) ? n : null;

                await _auditoria.Registrar("BajaProducto", "Producto", id,
                    $"Baja de producto con id {id}",
                    usuarioId, email);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
