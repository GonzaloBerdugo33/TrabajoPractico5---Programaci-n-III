namespace MiApiTP.DTOs
{
    public class CrearSalidaDTO
    {
        public int ClienteId { get; set; }
        public string MetodoPago { get; set; } = "Efectivo";
        public List<CrearSalidaDetalleDTO> Detalles { get; set; } = new();
    }
}