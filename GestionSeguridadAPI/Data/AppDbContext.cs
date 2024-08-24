using Microsoft.EntityFrameworkCore;

namespace GestionSeguridadAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Documento> Documentos { get; set; } // Asegúrate de que este nombre sea el correcto

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Categoria>().HasKey(c => c.Id);
            modelBuilder.Entity<Documento>().HasKey(d => d.Id);

            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.CategoriaPadre)
                .WithMany(c => c.Subcategorias)
                .HasForeignKey(c => c.CategoriaPadreId);

            modelBuilder.Entity<Documento>()
                .HasOne(d => d.Categoria)
                .WithMany()
                .HasForeignKey(d => d.CategoriaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
