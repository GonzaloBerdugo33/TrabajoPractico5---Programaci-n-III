namespace MiApiTP.Modelos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; }
        // FK
        public int CategoriaId { get; set; }
        // Propiedad de Navegacion
        public CategoriaProducto Categoria { get; set; }
        // FK
        public int ProveedorId { get; set; }
        // Propiedad de Navegacion
        public Proveedor Proveedor { get; set; }
        // Usamos listas para representar relaciones de uno a muchos
        public List<IngresoProducto> Ingresos { get; set; } = new List<IngresoProducto>();
        public List<SalidaProducto> Salidas { get; set; } = new List<SalidaProducto>();

    }
}
