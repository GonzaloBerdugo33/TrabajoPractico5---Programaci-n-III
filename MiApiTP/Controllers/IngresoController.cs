using MiApiTP.DTOs;
using MiApiTP.Modelos;
using MiApiTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class IngresoController : ControllerBase
    {
        private readonly IngresoService _service;

        public IngresoController(IngresoService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() 
        {
            var ingresos = await _service.ListarIngresos();
            return Ok(ingresos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id) 
        {
            var ingreso = await _service.ObtenerPorId(id);
            if (ingreso == null) return NotFound();
            return Ok(ingreso);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearIngresoDTOs dto)
        {
            var ingreso = new IngresoProducto
            {
                ProductoId = dto.ProductoId,
                ProveedorId = dto.ProveedorId,
                UsuarioId = dto.UsuarioId,
                Cantidad = dto.Cantidad,
                Fecha = DateTime.SpecifyKind(dto.Fecha, DateTimeKind.Utc)
            };

            try
            {
                await _service.RegistrarIngreso(ingreso);
                var resultado = new IngresoDTOs
                {
                    Id = ingreso.Id,
                    ProductoId = ingreso.ProductoId,
                    ProveedorId = ingreso.ProveedorId,
                    UsuarioId = ingreso.UsuarioId,
                    Cantidad = ingreso.Cantidad,
                    Fecha = ingreso.Fecha
                };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
