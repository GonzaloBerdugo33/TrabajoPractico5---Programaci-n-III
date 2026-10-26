namespace MiApiTP.DTOs
{
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
}