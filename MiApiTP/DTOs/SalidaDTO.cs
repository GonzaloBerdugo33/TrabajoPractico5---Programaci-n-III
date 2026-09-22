namespace MiApiTP.DTOs
{
    public class SalidaDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}