namespace MiApiTP.Modelos
{
    public class SalidaProducto
    {
        public int Id { get; set; }
        // FK
        public int ClienteId { get; set; }
        // FK
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        // Propiedades de navegación muchos a uno
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public List<SalidaDetalle> DetallesProductos { get; set; }
    }
}
