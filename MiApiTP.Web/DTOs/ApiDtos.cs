namespace MiApiTP.Web.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResultDTO
    {
        public string Token { get; set; } = string.Empty;
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
    }

    public class CrearUsuarioDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

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

    public class CrearProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public int ProveedorId { get; set; }
    }

    public class ProductoSelectDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }

    public class ProveedorSelectDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
    }

    public class CrearProveedorDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class ListarProveedorDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class ClienteSelectDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class CrearClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class ListarClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

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

    public class ListarSalidaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;
        public int TotalItems { get; set; }
    }

    public class CrearSalidaDTO
    {
        public int ClienteId { get; set; }

        public string MetodoPago { get; set; } = "Efectivo";
        public List<CrearSalidaDetalleDTO> Detalles { get; set; } = new();
    }

    public class CrearSalidaDetalleDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }

    public class IngresoDTO
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public List<IngresoDetalleDTO> DetallesProductos { get; set; } = new();
    }

    public class IngresoDetalleDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
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

    public class CategoriaProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int? UsuarioId { get; set; }
        public string UsuarioEmail { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public int? EntidadId { get; set; }
        public string Detalle { get; set; } = string.Empty;
        public string? Ip { get; set; }
    }

    public class AuditoriaPaginadaDTO
    {
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public List<AuditoriaDTO> Items { get; set; } = new();
    }

}