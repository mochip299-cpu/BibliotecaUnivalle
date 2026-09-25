using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dashboard
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            ViewBag.MisPrestamos = _context.Prestamos.Count(p => p.UsuarioId == userId && p.Estado == "Activo");
            return View();
        }

        // Catálogo de libros
        public async Task<IActionResult> Catalogo()
        {
            return View(await _context.Libros.Where(l => l.Stock > 0).ToListAsync());
        }

        // Mis préstamos
        public async Task<IActionResult> MisPrestamos()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var prestamos = await _context.Prestamos
                .Include(p => p.Libro)
                .Where(p => p.UsuarioId == userId)
                .ToListAsync();
            return View(prestamos);
        }

        // Solicitar préstamo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Solicitar(int libroId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var libro  = await _context.Libros.FindAsync(libroId);
            if (libro != null && libro.Stock > 0)
            {
                _context.Prestamos.Add(new Prestamo
                {
                    UsuarioId     = userId,
                    LibroId       = libroId,
                    FechaPrestamo = DateTime.Now,
                    Estado        = "Activo"
                });
                libro.Stock--;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Catalogo));
        }
    }
}
