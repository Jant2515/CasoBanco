using BancoPromericaCaso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BancoPromericaCaso.Controllers
{
    public class citasController : Controller
    {
        private readonly BancoContext _context;

        public citasController(BancoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.citas
                .Include(x => x.usuario)
                .Include(y => y.clientes)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.citas == null)
            {
                return NotFound();
            }

            var usuario = await _context.citas
                .Include(x => x.usuario)
                .Include(y => y.clientes)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        public IActionResult Create()
        {
            ViewData["Dropusuario"] = new SelectList(_context.usuario, "IdUsuario", "Nombres");
            ViewData["Dropclientes"] = new SelectList(_context.clientes, "IdCliente", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,FechaHora,Descripcion,IdUsuario,IdCliente")] citas cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cita);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.citas == null)
            {
                return NotFound();
            }

            var cita = await _context.citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            return View(cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,FechaHora,Descripcion,IdUsuario,IdCliente")] citas cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cita);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.citas == null)
            {
                return NotFound();
            }

            var cita = await _context.citas
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.citas == null)
            {
                return Problem("Entity set 'BancoContext.citas'  is null.");
            }
            var cita = await _context.citas.FindAsync(id);
            if (cita != null)
            {
                _context.citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return (_context.citas?.Any(e => e.IdCita == id)).GetValueOrDefault();
        }
    }
}
