using MiApiTP.Modelos;
using MiApiTP.Servicios;
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
        public async Task<IActionResult> Crear(SalidaProducto salida)
        {
            try
            {
                await _service.RegistrarSalida(salida);
                return Ok(salida);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
