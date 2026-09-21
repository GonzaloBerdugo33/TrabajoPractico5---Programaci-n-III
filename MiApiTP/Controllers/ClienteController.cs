using MiApiTP.Modelos;
using MiApiTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPaginado(int pagina = 1, int tamanoPagina = 10)
        {
            var clientes = await _service.ObtenerPaginado(pagina, tamanoPagina);
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _service.ObtenerPorId(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            await _service.CrearCliente(cliente);
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Actualizar(int id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();
            await _service.ActualizarCliente(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarCliente(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
