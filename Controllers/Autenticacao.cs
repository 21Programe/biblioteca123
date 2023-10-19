using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Biblioteca123.Models;


namespace Biblioteca123.Controllers
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
        
        public static bool verificaLoginSenha(string Login, string senha, Controller controller)
        {
             using (BibliotecaContext bc = new BibliotecaContext())
            {
                verificaAdmin(bc);

               senha = Criptografo.TextoCriptografado(senha);
               IQueryable<Usuario> c = bc.Usuarios.Where(u => u.Login == Login && u.Senha == senha);

               List<Usuario> lista = c.ToList();

               if(lista.Count == 0)
               {
                 return false;
               }
               else{
                 controller.HttpContext.Session.SetString("Login", lista[0].Login);
                 controller.HttpContext.Session.SetString("Nome", lista[0].Nome);
                 controller.HttpContext.Session.SetInt32("Tipo", lista[0].Tipo);
                 return true;
               }
            }
            
        }

       public static void verificaAdmin(BibliotecaContext bc)
       {
         IQueryable<Usuario> a = bc.Usuarios.Where(u => u.Login == "admin");

         List<Usuario> list = a.ToList();

         if(list.Count == 0)
         {
            Usuario Admin = new Usuario();
            Admin.Login = "admin";
            Admin.Senha = Criptografo.TextoCriptografado("123");
            Admin.Nome = "Administrador";
            Admin.Tipo = Usuario.ADMIN;

            bc.Add(Admin);
            bc.SaveChanges();
         }
       }

       public static void UsuarioAdmin(Controller controller)
       {
           if(controller.HttpContext.Session.GetInt32("Tipo") != Usuario.ADMIN)
           {
            controller.Request.HttpContext.Response.Redirect("/Usuarios/Admin");
           }
       }

    }
}