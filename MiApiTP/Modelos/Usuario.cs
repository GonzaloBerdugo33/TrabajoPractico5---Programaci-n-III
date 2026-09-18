namespace MiApiTP.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Rol { get; set; }
        // Usamos listas para representar la relación de uno a muchos
        public List<IngresoProducto> Ingresos { get; set; }
        public List<SalidaProducto> Productos { get; set; }

    }
}
