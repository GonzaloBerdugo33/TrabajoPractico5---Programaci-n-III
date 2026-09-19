namespace MiApiTP.Modelos
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        // Usamos listas para representar la relación de uno a muchos
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public List<IngresoProducto> Ingresos { get; set; } = new List<IngresoProducto>();
    }
}
