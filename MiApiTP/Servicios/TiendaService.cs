using Microsoft.EntityFrameworkCore;
using MiApiTP.Data;
using MiApiTP.DTOs;
using MiApiTP.Modelos;

namespace MiApiTP.Servicios
{
    // Lógica de la tienda online (app móvil). Reutiliza UsuarioService y SalidaService.
    public class TiendaService
    {
        private readonly MiApiTPContext _context;
        private readonly UsuarioService _usuarioService;
        private readonly SalidaService _salidaService;

        public TiendaService(MiApiTPContext context, UsuarioService usuarioService, SalidaService salidaService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _salidaService = salidaService;
        }

        // ---------- Registro de cliente desde la app ----------
        public async Task<UsuarioDTO> RegistrarClienteAsync(RegistroClienteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new InvalidOperationException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new InvalidOperationException("El email es obligatorio.");
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new InvalidOperationException("La contraseña debe tener al menos 6 caracteres.");
            if (string.IsNullOrWhiteSpace(dto.Telefono))
                throw new InvalidOperationException("El teléfono es obligatorio.");
            if (string.IsNullOrWhiteSpace(dto.Direccion))
                throw new InvalidOperationException("La dirección es obligatoria.");

            // 1) Se crea el usuario con rol "Cliente" fijo (la app no puede elegir otro).
            //    RegistrarAsync ya controla que el email no esté repetido.
            var usuario = await _usuarioService.RegistrarAsync(new CrearUsuarioDTO
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password,
                Rol = "Cliente"
            });

            // 2) Se crea el cliente y se lo une al usuario con UsuarioId
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                UsuarioId = usuario.Id
            };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return usuario;
        }

        // ---------- Compra del cliente logueado ----------
        public async Task<int> ComprarAsync(CrearCompraDTO dto, int usuarioId)
        {
            var cliente = await ObtenerClienteDeUsuarioAsync(usuarioId);

            var salida = new CrearSalidaDTO
            {
                ClienteId = cliente.Id,   // sale del token, no de la app
                MetodoPago = dto.MetodoPago,
                Detalles = dto.Detalles
            };

            // Misma lógica que la venta de la Web: valida stock, congela precio y descuenta stock
            return await _salidaService.RegistrarSalidaAsync(salida, usuarioId);
        }

        // ---------- Mis compras ----------
        public async Task<List<SalidaDTO>> MisComprasAsync(int usuarioId)
        {
            var cliente = await ObtenerClienteDeUsuarioAsync(usuarioId);

            return await _context.Salidas
                .Where(s => s.ClienteId == cliente.Id)
                .OrderByDescending(s => s.Fecha)
                .Select(s => new SalidaDTO
                {
                    Id = s.Id,
                    ClienteId = s.ClienteId,
                    UsuarioId = s.UsuarioId,
                    ClienteNombre = cliente.Nombre,
                    Fecha = s.Fecha,
                    MetodoPago = s.MetodoPago,
                    DetallesProductos = s.DetallesProductos.Select(d => new SalidaDetalleDTO
                    {
                        ProductoId = d.ProductoId,
                        NombreProducto = d.Producto != null ? d.Producto.Nombre : "Desconocido",
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList()
                })
                .ToListAsync();
        }

        // Busca el cliente que pertenece al usuario logueado
        private async Task<Cliente> ObtenerClienteDeUsuarioAsync(int usuarioId)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            if (cliente == null)
                throw new InvalidOperationException("Tu usuario no tiene un cliente asociado.");
            return cliente;
        }
    }
}