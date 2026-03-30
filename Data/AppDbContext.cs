using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }


        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<MovimientoInventario> Movimientos {  get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasOne(p => p.Categoria).WithMany(c => c.Productos).HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<MovimientoInventario>().HasOne(m => m.Usuario).WithMany(u => u.Movimientos).HasForeignKey(m => m.UsuarioId);

            modelBuilder.Entity<MovimientoInventario>().HasOne(m => m.Producto).WithMany(p => p.Movimientos).HasForeignKey( m => m.ProductoId);
        }
    }
}
