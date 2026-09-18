using Microsoft.EntityFrameworkCore;
using MiApiTP.Modelos;

namespace MiApiTP.Data
{
    public class MiApiTPContext : DbContext
    {
        public MiApiTPContext(DbContextOptions<MiApiTPContext> options) : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CategoriaProducto> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<IngresoProducto> Ingresos { get; set; }
        public DbSet<SalidaProducto> Salidas { get; set; }
    }
}
