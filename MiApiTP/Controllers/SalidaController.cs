using MiApiTP.Modelos;
using MiApiTP.Servicios;
using MiApiTP.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalidaController : ControllerBase
    {
        private readonly SalidaService _service;
        private readonly AuditoriaService _auditoria;

        public SalidaController(SalidaService service, AuditoriaService auditoria)
        {
            _service = service;
            _auditoria = auditoria;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Cajero") ]
        public async Task<IActionResult> ObtenerTodos()
        {
            var salidas = await _service.ListarSalidaAsync();
            return Ok(salidas);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Cajero")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var dto = await _service.ObtenerDetalleAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Cajero")]
        public async Task<IActionResult> Crear(CrearSalidaDTO dto)
        {
            try
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? User.FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(usuarioIdClaim))
                {
                    return Unauthorized(new { mensaje = "No se pudo identificar al usuario autenticado." });
                }

                int usuarioIdLogueado = int.Parse(usuarioIdClaim);

                var id = await _service.RegistrarSalidaAsync(dto, usuarioIdLogueado);

                var email = User.FindFirst(ClaimTypes.Email)?.Value
                         ?? User.FindFirst("email")?.Value
                         ?? "";
                var items = dto.Detalles.Sum(d => d.Cantidad);

                await _auditoria.Registrar("Venta", "Salida", id,
                    $"Venta a cliente {dto.ClienteId}, pago {dto.MetodoPago}, {items} unidades",
                    usuarioIdLogueado, email);

                return Ok(new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
