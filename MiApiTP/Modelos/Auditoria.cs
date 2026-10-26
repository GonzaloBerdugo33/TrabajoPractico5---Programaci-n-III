using System.ComponentModel.DataAnnotations;

namespace MiApiTP.Modelos
{
    public class Auditoria
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public int? UsuarioId { get; set; }

        [MaxLength(200)]
        public string UsuarioEmail { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Accion { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Entidad { get; set; } = string.Empty;
        public int? EntidadId { get; set; }

        [MaxLength(500)]
        public string Detalle { get; set; } = string.Empty;

        [MaxLength(45)]
        public string? Ip { get; set; }
    }
}
