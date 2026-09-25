using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Bibliotecario")]
    public class BibliotecarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BibliotecarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dashboard
        public IActionResult Index()
        {
            ViewBag.TotalLibros      = _context.Libros.Count();
            ViewBag.PrestamosActivos = _context.Prestamos.Count(p => p.Estado == "Activo");
            return View();
        }

        // Libros
        public async Task<IActionResult> Libros()
        {
            return View(await _context.Libros.ToListAsync());
        }

        // Préstamos
        public async Task<IActionResult> Prestamos()
        {
            var prestamos = await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .ToListAsync();
            return View(prestamos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                prestamo.Estado          = "Devuelto";
                prestamo.FechaDevolucion = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Prestamos));
        }
    }
}
