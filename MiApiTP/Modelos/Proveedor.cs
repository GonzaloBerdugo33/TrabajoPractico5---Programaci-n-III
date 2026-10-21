namespace MiApiTP.Modelos
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Usamos listas para representar la relación de uno a muchos
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public List<IngresoProducto> Ingresos { get; set; } = new List<IngresoProducto>();
    }
}
