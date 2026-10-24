namespace MiApiTP.DTOs
{
    public class ListarSalidaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;
        public int TotalItems { get; set; }
    }
}