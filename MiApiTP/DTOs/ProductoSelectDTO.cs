namespace MiApiTP.DTOs
{
    public class ProductoSelectDTO
    {
        public int Id { get; set;  }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock {  get; set; }
    }
}
