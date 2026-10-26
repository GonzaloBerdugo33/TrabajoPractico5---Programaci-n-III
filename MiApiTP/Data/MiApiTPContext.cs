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

            // Relación SalidaProducto -> SalidaDetalles
            modelBuilder.Entity<SalidaProducto>()
                .HasMany(s => s.DetallesProductos)
                .WithOne(d => d.SalidaProducto)
                .HasForeignKey(d => d.SalidaProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalidaProducto>()
                .Property(s => s.MetodoPago)
                .HasMaxLength(20)
                .HasDefaultValue("Efectivo");

            modelBuilder.Entity<SalidaDetalle>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación IngresoProducto -> IngresoDetalles
            modelBuilder.Entity<IngresoProducto>()
                .HasMany(i => i.DetallesIngresos)
                .WithOne(d => d.IngresoProducto)
                .HasForeignKey(d => d.IngresoProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngresoDetalle>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
