namespace WebApplication2.Models
{
    // Punto 2: Existen 3 tipos de usuarios en el sistema
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Roles posibles: "Administrador", "Bibliotecario", "Usuario"
        public string Rol { get; set; } = "Usuario";
    }
}
