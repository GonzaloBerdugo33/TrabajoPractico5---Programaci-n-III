using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiApiTP.Modelos;
using MiApiTP.Servicios;
using MiApiTP.DTOs;

namespace MiApiTP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly JwtService _jwtService;

        public UsuarioController(UsuarioService service, JwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarios = await _service.ListarUsuarios();
            var dtos = usuarios.Select(u => new UsuarioDTOs
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Rol = u.Rol
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuario = await _service.ObtenerPorId(id);
            if (usuario == null) return NotFound();
            var dto = new UsuarioDTOs
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol
            };
            return Ok(dto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTOs dto) 
        {
            var usuario = await _service.ValidarCredenciales(dto.Email, dto.Password);
            if(usuario == null) 
            {
                return Unauthorized("Credenciales Invalidas");
            }

            var token = _jwtService.GenerarToken(usuario);
            return Ok(new { token = token });
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearUsuarioDTOs dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = dto.Rol
            };
            await _service.CrearUsuario(usuario);

            var resultado = new UsuarioDTOs
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol
            };
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Actualizar(int id, CrearUsuarioDTOs dto)
        {
            var usuario = new Usuario
            {
                Id = id,
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = dto.Rol
            };
            await _service.ActualizarUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarUsuario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}