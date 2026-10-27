using MiApiTP.Repositorios;
using MiApiTP.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly ProductoService _productoService;
        private readonly IIngresoProductoRepository _ingresoRepository;
        private readonly ISalidaProductoRepository _salidaRepository;
        private readonly IReporteService _pdf;

        public ReporteController(ProductoService productoService,
            IIngresoProductoRepository ingresoRepository,
            ISalidaProductoRepository salidaRepository,
            IReporteService pdf)
        {
            _productoService = productoService;
            _ingresoRepository = ingresoRepository;
            _salidaRepository = salidaRepository;
            _pdf = pdf;
        }

        [HttpGet("Stock")]
        public async Task<IActionResult> Stock()
        {
            var productos = await _productoService.ObtenerTodos();
            var archivo = _pdf.GenerarListadoStock(productos.OrderBy(p => p.Nombre).ToList());
            return File(archivo, "application/pdf", $"stock-{DateTime.UtcNow.AddHours(-3):yyyyMMdd}.pdf");
        }

        [HttpGet("Entradas")]
        public async Task<IActionResult> Entradas()
        {
            var ingresos = await _ingresoRepository.ObtenerTodosAsync();
            var archivo = _pdf.GenerarListadoEntradas(ingresos.OrderByDescending(i => i.Fecha).ToList());
            return File(archivo, "application/pdf", $"entradas-{DateTime.UtcNow.AddHours(-3):yyyyMMdd}.pdf");
        }

        [HttpGet("Salidas")]
        public async Task<IActionResult> Salidas()
        {
            var salidas = await _salidaRepository.ObtenerTodosAsync();
            var archivo = _pdf.GenerarListadoSalidas(salidas.OrderByDescending(s => s.Fecha).ToList());
            return File(archivo, "application/pdf", $"salidas-{DateTime.UtcNow.AddHours(-3):yyyyMMdd}.pdf");
        }
    }
}