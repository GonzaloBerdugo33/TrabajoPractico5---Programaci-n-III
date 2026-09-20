namespace MiApiTP.DTOs
{
    public class CrearIngresoDTO
    {
        public int ProductoId { get; set; }
        public int ProveedorId { get; set; }
        public int UsuarioId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}