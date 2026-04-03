using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<InventoryMovement> InventoryMovements {  get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<InventoryMovement>().HasOne(m => m.User).WithMany(u => u.Movements).HasForeignKey(m => m.UserId);

            modelBuilder.Entity<InventoryMovement>().HasOne(m => m.Product).WithMany(p => p.Movements).HasForeignKey( m => m.ProductId);
        }
    }
}
