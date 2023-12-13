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
                    var usuario = await _context.usuario.SingleOrDefaultAsync(x => x.Correo == login.Correo && x.Contrasena == login.Contraseña);

                    if (usuario != null)
                    {
                        HttpContext.Session.SetString("Correo", usuario.Correo);
                        HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));

                        if (usuario.EsAdministrador)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
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
    }
}
