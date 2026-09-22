using MiApiTP.Modelos;
using MiApiTP.Servicios;
using MiApiTP.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Authorize]
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
            var salida = new SalidaProducto
            {
                ProductoId = dto.ProductoId,
                ClienteId = dto.ClienteId,
                UsuarioId = dto.UsuarioId,
                Cantidad = dto.Cantidad,
                Fecha = DateTime.SpecifyKind(dto.Fecha, DateTimeKind.Utc)
            };

            try
            {
                await _service.RegistrarSalida(salida);
                var resultado = new SalidaDTO
                {
                    Id = salida.Id,
                    ProductoId = salida.ProductoId,
                    ClienteId = salida.ClienteId,
                    UsuarioId = salida.UsuarioId,
                    Cantidad = salida.Cantidad,
                    Fecha = salida.Fecha
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
