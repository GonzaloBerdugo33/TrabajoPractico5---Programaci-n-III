namespace MiApiTP.Modelos
{
    public class SalidaProducto
    {
        public int Id { get; set; }
        // FK
        public int ProductoId { get; set; }
        // FK
        public int ClienteId { get; set; }
        // FK
        public int UsuarioId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        // Propiedades de navegación muchos a uno
        public Producto Producto { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
    }
}
