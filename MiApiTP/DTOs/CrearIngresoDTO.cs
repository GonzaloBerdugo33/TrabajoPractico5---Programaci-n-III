namespace MiApiTP.DTOs
{
    public class CrearIngresoDTO
    {
        public int ProveedorId { get; set; }
        public List<CrearIngresoDetalleDTO> Detalles { get; set; } = new();
    }
}