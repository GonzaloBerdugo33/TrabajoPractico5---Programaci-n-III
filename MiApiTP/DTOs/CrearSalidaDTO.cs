namespace MiApiTP.DTOs
{
    public class CrearSalidaDTO
    {
        public int ClienteId { get; set; }
        public List<CrearSalidaDetalleDTO> Detalles { get; set; } = new();
    }
}