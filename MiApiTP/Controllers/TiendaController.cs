using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApiTP.DTOs;
using MiApiTP.Servicios;
using System.Security.Claims;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiendaController : ControllerBase
    {
        private readonly TiendaService _service;
        private readonly AuditoriaService _auditoria;

        public TiendaController(TiendaService service, AuditoriaService auditoria)
        {
            _service = service;
            _auditoria = auditoria;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(RegistroClienteDTO dto)
        {
            try
            {
                var usuario = await _service.RegistrarClienteAsync(dto);

                await _auditoria.Registrar("RegistroCliente", "Usuario", usuario.Id,
                    $"Registro de cliente desde la app: {usuario.Email}",
                    usuario.Id, usuario.Email);

                return Ok(usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("compra")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Comprar(CrearCompraDTO dto)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            if (!int.TryParse(idClaim, out var usuarioId)) return Unauthorized();

            try
            {
                var id = await _service.ComprarAsync(dto, usuarioId);

                await _auditoria.Registrar("CompraOnline", "Salida", id,
                    $"Compra online #{id}", usuarioId, email);

                return Ok(new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("mis-compras")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> MisCompras()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var usuarioId)) return Unauthorized();

            try
            {
                var compras = await _service.MisComprasAsync(usuarioId);
                return Ok(compras);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}