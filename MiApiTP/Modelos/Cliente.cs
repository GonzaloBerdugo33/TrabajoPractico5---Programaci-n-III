using System.Text.Json.Serialization;

namespace MiApiTP.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Usamos listas para representar la relación de uno a muchos
        [JsonIgnore]
        public List<SalidaProducto> Salidas { get; set; } = new List<SalidaProducto>();
    }
}
