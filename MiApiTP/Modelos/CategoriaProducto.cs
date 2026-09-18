namespace MiApiTP.Modelos
{
    public class CategoriaProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Propiedad de Navegacion
        public List<Producto> Productos { get; set; }
    }
}
