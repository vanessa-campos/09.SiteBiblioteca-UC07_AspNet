using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
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
            if(login == null){
                ViewData["Erro"] = "Login inválido";
                return View();
            } else if(senha == null){
                ViewData["Erro"] = "Senha inválida";
                return View();
            } else{
                HttpContext.Session.SetString("user", login);
                HttpContext.Session.SetString("password", senha);
                return RedirectToAction("Index");
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
