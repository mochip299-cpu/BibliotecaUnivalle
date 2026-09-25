using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed: usuarios por defecto para probar el login
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Admin",        Email = "admin@univalle.edu.bo",  Password = "admin123",  Rol = "Administrador" },
                new Usuario { Id = 2, Nombre = "Bibliotecario",Email = "biblio@univalle.edu.bo", Password = "biblio123", Rol = "Bibliotecario" },
                new Usuario { Id = 3, Nombre = "Juan Perez",   Email = "juan@univalle.edu.bo",   Password = "juan123",   Rol = "Usuario" }
            );
        }
    }
}
