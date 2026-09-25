using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed: usuarios por defecto
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Admin",         Email = "admin@univalle.edu.bo",  Password = "admin123",  Rol = "Administrador" },
                new Usuario { Id = 2, Nombre = "Bibliotecario", Email = "biblio@univalle.edu.bo", Password = "biblio123", Rol = "Bibliotecario" },
                new Usuario { Id = 3, Nombre = "Juan Perez",    Email = "juan@univalle.edu.bo",   Password = "juan123",   Rol = "Usuario" }
            );

            modelBuilder.Entity<Libro>().HasData(
                new Libro { Id = 1, Titulo = "Introduccion a C#",    Autor = "John Sharp",   ISBN = "978-0-7356-6759-7", Stock = 5 },
                new Libro { Id = 2, Titulo = "ASP.NET Core en Accion", Autor = "Andrew Lock", ISBN = "978-1-61729-466-6", Stock = 3 }
            );
        }
    }
}
