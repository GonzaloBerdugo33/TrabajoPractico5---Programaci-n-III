namespace MiApiTP.DTOs
{
    public class ListarIngresoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string ProveedorRazonSocial { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;
        public int TotalItems { get; set; }
    }
}
