using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dashboard principal
        public IActionResult Index()
        {
            ViewBag.TotalLibros    = _context.Libros.Count();
            ViewBag.TotalUsuarios  = _context.Usuarios.Count();
            ViewBag.TotalPrestamos = _context.Prestamos.Count();
            ViewBag.PrestamosActivos = _context.Prestamos.Count(p => p.Estado == "Activo");
            return View();
        }

        // ---- LIBROS ----
        public async Task<IActionResult> Libros()
        {
            return View(await _context.Libros.ToListAsync());
        }

        public IActionResult CrearLibro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearLibro(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libros.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Libros));
            }
            return View(libro);
        }

        public async Task<IActionResult> EditarLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarLibro(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libros.Update(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Libros));
            }
            return View(libro);
        }

        public async Task<IActionResult> EliminarLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Libros));
        }

        // ---- USUARIOS ----
        public async Task<IActionResult> Usuarios()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        public IActionResult CrearUsuario() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Usuarios));
            }
            return View(usuario);
        }

        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Usuarios));
        }

        // ---- PRÉSTAMOS ----
        public async Task<IActionResult> Prestamos()
        {
            var prestamos = await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .ToListAsync();
            return View(prestamos);
        }
    }
}
