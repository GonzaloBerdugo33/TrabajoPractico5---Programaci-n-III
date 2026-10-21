namespace MiApiTP.DTOs
{
    public class SalidaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public List<SalidaDetalleDTO> DetallesProductos { get; set; } = new();
    }
}