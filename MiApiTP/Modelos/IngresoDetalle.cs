using System.Text.Json.Serialization;

namespace MiApiTP.Modelos
{
    public class IngresoDetalle
    {
        public int Id { get; set; }
        // FK
        public int ProductoId { get; set; }
        public Producto producto { get; set; }
        //FK
        public int IngresoProductoId { get; set; }
        [JsonIgnore]
        public IngresoProducto? ingresoProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

    }
}
