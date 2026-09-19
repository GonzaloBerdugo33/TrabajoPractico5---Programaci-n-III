namespace MiApiTP.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        // Usamos listas para representar la relación de uno a muchos
        public List<SalidaProducto> Salidas { get; set; } = new List<SalidaProducto>();
    }
}
