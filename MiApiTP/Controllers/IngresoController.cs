using MiApiTP.DTOs;
using MiApiTP.Modelos;
using MiApiTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador")]
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
            var dto = ingresos.Select(i => new ListarIngresoDTO
            {
                Id = i.Id,
                Fecha = DateTime.UtcNow,
                ProveedorRazonSocial = i.Proveedor?.RazonSocial?? "Desconocido",
                UsuarioEmail = i.Usuario?.Email?? "Desconocido",
                TotalItems = i.DetallesIngresos?.Sum(d => d.Cantidad) ?? 0
            }).ToList();
            
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id) 
        {
            var ingreso = await _service.ObtenerPorId(id);
            if (ingreso == null) return NotFound();
            return Ok(ingreso);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearIngresoDTO dto)
        {
            try 
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? User.FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(usuarioIdClaim))
                {
                    return Unauthorized(new { mensaje = "No se pudo identificar al usuario autenticado desde el token." });
                }

                int usuarioIdLogueado = int.Parse(usuarioIdClaim);
                var ingreso = new IngresoProducto
                {
                    ProveedorId = dto.ProveedorId,
                    UsuarioId = usuarioIdLogueado,
                    Fecha = DateTime.UtcNow,


                    DetallesIngresos = dto.Detalles.Select(detalleDto => new IngresoDetalle
                    {
                        ProductoId = detalleDto.ProductoId,
                        Cantidad = detalleDto.Cantidad

                    }).ToList()
                };


                await _service.RegistrarIngreso(ingreso);

                return Ok(new { mensaje = "Ingreso registrado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
