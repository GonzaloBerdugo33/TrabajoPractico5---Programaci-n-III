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
            var dtos = await _service.ListarIngresosAsync();
            return Ok(dtos);
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
                    return Unauthorized(new { mensaje = "No se pudo identificar al usuario autenticado." });
                }

                int usuarioIdLogueado = int.Parse(usuarioIdClaim);

                await _service.RegistrarIngresoAsync(dto, usuarioIdLogueado);

                return Ok(new { mensaje = "Ingreso registrado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
