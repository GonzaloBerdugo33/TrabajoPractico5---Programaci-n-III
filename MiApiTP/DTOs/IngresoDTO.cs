namespace MiApiTP.DTOs
{
    public class IngresoDTO
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public List<IngresoDetalleDTO> DetallesProductos { get; set; }
    }
}