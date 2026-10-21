namespace MiApiTP.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? ImagenUrl { get; set; } = string.Empty;
        public string? CategoriaNombre { get; set; }
        public string? ProveedorNombre { get; set; }
    }
}
