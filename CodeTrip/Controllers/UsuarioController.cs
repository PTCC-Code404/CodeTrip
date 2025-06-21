using CodeTrip.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CodeTrip.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string Email_Usuario, string Senha_Usuario)
        {

            var usuario = _usuarioRepositorio.ObterUsuario(Email_Usuario);
            if (usuario != null && usuario.Senha_Usuario == Senha_Usuario)
            {
                
                return RedirectToAction("MenuSistema", "Home");
            }
            
            ModelState.AddModelError("", "Email ou senha inválidos.");
            return View();
        }
    }
}