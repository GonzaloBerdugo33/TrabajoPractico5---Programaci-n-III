using MiApiTP.Modelos;
using MiApiTP.Servicios;
using MiApiTP.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class SalidaController : ControllerBase
    {
        private readonly SalidaService _service;

        public SalidaController(SalidaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var salidas = await _service.ListarSalidas();
            return Ok(salidas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var salida = await _service.ObtenerPorId(id);
            if (salida == null) return NotFound();
            return Ok(salida);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearSalidaDTO dto)
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(usuarioIdClaim))
            {
                return Unauthorized(new { mensaje = "No se pudo identificar al usuario autenticado desde el token." });
            }

            int usuarioIdLogueado = int.Parse(usuarioIdClaim);

            try
            {
                var salida = new SalidaProducto
                {
                    ClienteId = dto.ClienteId,
                    UsuarioId = usuarioIdLogueado,
                    Fecha = DateTime.UtcNow,
                    DetallesProductos = dto.Detalles.Select(detalleDto => new SalidaDetalle
                    {
                        ProductoId = detalleDto.ProductoId,
                        Cantidad = detalleDto.Cantidad
                    }).ToList()
                };

                await _service.RegistrarSalida(salida);

                return Ok(new { mensaje = "Salida registrada con éxito" });
            }
            catch (Exception ex)
            {
                // 🔍 Esto nos va a mostrar el error exacto y en qué línea ocurrió
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "Sin inner exception";
                return BadRequest(new
                {
                    error = ex.Message,
                    inner = innerMessage,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}
