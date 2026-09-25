namespace WebApplication2.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string Estado { get; set; } = "Activo"; // Activo, Devuelto

        public Usuario? Usuario { get; set; }
        public Libro? Libro { get; set; }
    }
}
