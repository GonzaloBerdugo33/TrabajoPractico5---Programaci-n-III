namespace MiApiTP.DTOs
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int? UsuarioId { get; set; }
        public string UsuarioEmail { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public int? EntidadId { get; set; }
        public string Detalle { get; set; } = string.Empty;
        public string? Ip { get; set; }
    }

    public class AuditoriaPaginadaDTO
    {
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public List<AuditoriaDTO> Items { get; set; } = new();
    }
}