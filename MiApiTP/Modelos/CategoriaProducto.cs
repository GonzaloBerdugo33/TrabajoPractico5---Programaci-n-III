using System.Text.Json.Serialization;

namespace MiApiTP.Modelos
{
    public class CategoriaProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Propiedad de Navegacion
        [JsonIgnore]
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
