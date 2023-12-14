using BancoPromericaCaso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BancoPromericaCaso.Controllers
{
    public class LoginController : Controller
    {
        private readonly BancoContext _context;

        public LoginController(BancoContext context)
        {
            _context = context;
        }
         public IActionResult Register()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _context.usuario.SingleOrDefaultAsync(x => x.Correo == login.Correo && x.Contrasena == login.Contrasena);

                    if (usuario != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                return View("Index", login);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error en el inicio de sesión");
                return View("Index", login);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("IdUsuario,Nombres,Apellidos,Correo,Contrasena")] usuario usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(usuarios);
        }

    }
}
