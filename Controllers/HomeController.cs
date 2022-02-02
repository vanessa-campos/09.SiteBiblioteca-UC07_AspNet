using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            if(Autenticacao.CheckLoginSenha(login, senha, this)){
                return RedirectToAction("Index");

            } else{
                ViewData["Erro"] = "Login ou senha inválidos";
                return View();
            }
        }

        public IActionResult Logout(){
			HttpContext.Session.Clear();            
            return RedirectToAction("Login");
	    }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
