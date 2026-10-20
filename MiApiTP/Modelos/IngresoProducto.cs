namespace MiApiTP.Modelos
{
    public class IngresoProducto
    {
        public int Id { get; set; }
        // FK
        public int ProveedorId { get; set; }
        // FK
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        // Propiedades de navegación muchos a uno
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public List<IngresoDetalle> DetallesIngresos { get; set; }

    }
}
