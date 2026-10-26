namespace MiApiTP.Modelos
{
    public class SalidaProducto
    {
        public int Id { get; set; }
        // FK
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        // FK
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; } = "Efectivo";
        public List<SalidaDetalle> DetallesProductos { get; set; } = new();
    }
}
