namespace PuntoDeVenta.Movil.DTOs
{
    // ---------- Login y usuario ----------
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResultDTO
    {
        public string Token { get; set; } = string.Empty;
    }

    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    // ---------- Tienda ----------
    // El mismo ProductoDTO que usa la Web
    public class ProductoDTO
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public int ProveedorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? ImagenUrl { get; set; } = string.Empty;
        public string? CategoriaNombre { get; set; }
        public string? ProveedorNombre { get; set; }
    }

    public class RegistroClienteDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }

    public class CrearCompraDTO
    {
        public string MetodoPago { get; set; } = "Tarjeta";
        public List<CrearSalidaDetalleDTO> Detalles { get; set; } = new();
    }

    public class CrearSalidaDetalleDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }

    // ---------- Compras (mis compras, ventas y ingresos) ----------
    public class SalidaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; } = "Efectivo";
        public List<SalidaDetalleDTO> DetallesProductos { get; set; } = new();
    }

    public class SalidaDetalleDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }

    public class ProveedorSelectDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
    }

    public class ProductoSelectDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }

    public class ListarIngresoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string ProveedorRazonSocial { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;
        public int TotalItems { get; set; }
    }

    public class CrearIngresoDTO
    {
        public int ProveedorId { get; set; }
        public List<CrearIngresoDetalleDTO> Detalles { get; set; } = new();
    }

    public class CrearIngresoDetalleDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
