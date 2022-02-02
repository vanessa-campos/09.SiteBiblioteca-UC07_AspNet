using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro(){
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario u){    

            UsuarioService usuarioService = new UsuarioService();
            if(u.Id == 0) {
                u.Senha = Encrypt.TextEncrypted(u.Senha);
                usuarioService.Inserir(u);
            }
            else{
                u.Senha = Encrypt.TextEncrypted(u.Senha);
                usuarioService.Atualizar(u);
            }
            return RedirectToAction("Listagem");
        }
        
        public IActionResult Listagem(int p=1){
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckIfUserIsAdmin(this);

            int qtPg = 10;            
            UsuarioService usuarioService = new UsuarioService();  
            ICollection<Usuario> list = usuarioService.MaxUsuariosPg(p, qtPg);
            int qtReg = usuarioService.CountItems();
            ViewData["Pages"] = (int)Math.Ceiling((double)qtReg/qtPg);
            return View(list);
        }

        [HttpPost]
        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckIfUserIsAdmin(this);

            FiltrosUsuarios objFiltro = null;
            if(!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosUsuarios();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;                
            }

            UsuarioService usuarioService = new UsuarioService();
            return View(usuarioService.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckIfUserIsAdmin(this);        

            UsuarioService us = new UsuarioService();
            Usuario u = us.ObterPorId(id);
            return View(u);
        }

        public IActionResult Excluir(int id)
        {
            Autenticacao.CheckLogin(this);            
            Autenticacao.CheckIfUserIsAdmin(this);        

            UsuarioService us = new UsuarioService();
            Usuario u = us.ObterPorId(id);
            return View(u);
        }

        [HttpPost]
        public IActionResult Excluir(int id, string escolha)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckIfUserIsAdmin(this);        

            if(escolha == "s"){
                UsuarioService us = new UsuarioService();
                us.DeletarUsuario(id);
            }
            return RedirectToAction("Listagem");
        }


        public IActionResult AdminRequired(){
            Autenticacao.CheckLogin(this);
            return View();
        }
        
    }
}