

namespace MiApiTP.DTOs
{
    public class CrearProductoDTO
    {
        public string Nombre {  get; set; }
        public decimal Precio { get; set; }
        public int Stock {  get; set; }
        public string ImagenUrl { get; set; }
        public int CategoriaId { get; set; }
        public int ProveedorId { get; set;  }

    }
}
