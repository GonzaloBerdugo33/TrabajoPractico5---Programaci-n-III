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
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<IngresoProducto> Ingresos { get; set; }
        public DbSet<IngresoDetalle> IngresoDetalles { get; set; }
        public DbSet<SalidaProducto> Salidas { get; set; }
        public DbSet<SalidaDetalle> SalidaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalidaProducto>()
                .Ignore("ProductoId")
                .Ignore("Producto");

            modelBuilder.Entity<SalidaProducto>()
                .HasMany(s => s.DetallesProductos)
                .WithOne(d => d.salidaProducto)
                .HasForeignKey(d => d.SalidaProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalidaDetalle>()
                .HasOne(d => d.producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId);

            modelBuilder.Entity<IngresoProducto>()
                .Ignore("ProductoId")
                .Ignore("Producto");

            modelBuilder.Entity<IngresoProducto>()
                .HasMany(i => i.DetallesIngresos)
                .WithOne(d => d.ingresoProducto)
                .HasForeignKey(d => d.IngresoProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngresoDetalle>()
                .HasOne(d => d.producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId);
        }
    }
}
