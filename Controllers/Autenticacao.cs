using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static bool CheckLoginSenha(string login, string senha, Controller controller){
            using (BibliotecaContext bc = new BibliotecaContext()){
                CheckIfUserAdminExist(bc);
                senha = Encrypt.TextEncrypted(senha);

                IQueryable<Usuario> usuarioEncontrado = bc.Usuarios.Where(u => u.Login == login && u.Senha == senha);
                List<Usuario> ListaUsuariosEncontrados = usuarioEncontrado.ToList();

                if(ListaUsuariosEncontrados.Count == 0){
                    return false;
                }else{
                    //registrando na sessão os dados do usuário encontrado
                    controller.HttpContext.Session.SetString("Login", ListaUsuariosEncontrados[0].Login);
                    controller.HttpContext.Session.SetString("Nome", ListaUsuariosEncontrados[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", ListaUsuariosEncontrados[0].Tipo);
                    return true;
                }
            }
        }

        public static void CheckIfUserAdminExist(BibliotecaContext bc){
            IQueryable<Usuario> usuarioEncontrado = bc.Usuarios.Where(u => u.Login == "admin");

            //Se não existir será criado o usuário admin 
            if(usuarioEncontrado.ToList().Count==0){
                Usuario admin = new Usuario();
                admin.Login = "admin";
                admin.Senha = Encrypt.TextEncrypted("123");
                admin.Tipo = Usuario.ADMIN;
                admin.Nome = "Administrador";

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }
        }

        public static void CheckIfUserIsAdmin(Controller controller){
            if(!(controller.HttpContext.Session.GetInt32("Tipo") == Usuario.ADMIN)){
                controller.Request.HttpContext.Response.Redirect("/Usuario/AdminRequired");   
            }
        }
    }
}