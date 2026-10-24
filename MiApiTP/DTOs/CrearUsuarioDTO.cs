namespace MiApiTP.DTOs
{
    public class CrearUsuarioDTO
    {
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Rol { get; set; } = string.Empty;
    }
}