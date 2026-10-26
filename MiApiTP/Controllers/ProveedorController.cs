using MiApiTP.DTOs;
using MiApiTP.Modelos;
using MiApiTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorService _service;

        public ProveedorController(ProveedorService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPaginado(int pagina = 1, int tamanoPagina = 10) 
        {
            var proveedores = await _service.ObtenerPaginado(pagina, tamanoPagina);
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id) 
        {
            var proveedor = await _service.ObtenerPorId(id);
            if (proveedor == null) return NotFound();
            return Ok(proveedor);
        }

        [HttpGet("Todos")]
        public async Task<IActionResult> ObtenerTodosParaSelect()
        {
            var proveedor = await _service.ObtenerTodos();

            var dto = proveedor.Select(p => new ProveedorSelectDTO
            {
                Id = p.Id,
                RazonSocial = p.RazonSocial
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("ListarTodos")]
        public async Task<IActionResult> ObtenerTodosParaGrid()
        {
            var proveedores = await _service.ObtenerTodos();
            return Ok(proveedores);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Proveedor proveedor) 
        {
            await _service.CrearProveedor(proveedor);
            return Ok(proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Proveedor proveedor) 
        {
            if (id != proveedor.Id) return BadRequest();
            await _service.ActualizarProveedor(proveedor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarProveedor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
