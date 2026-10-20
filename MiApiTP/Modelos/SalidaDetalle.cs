using System.Text.Json.Serialization;

namespace MiApiTP.Modelos
{
    public class SalidaDetalle
    {
        public int Id {  get; set; }
        // FK
        public int ProductoId { get; set; }
        public Producto producto { get; set; }
        //FK
        public int SalidaProductoId {  get; set; }
        
        [JsonIgnore]
        public SalidaProducto salidaProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
