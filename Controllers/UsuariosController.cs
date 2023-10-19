
using Biblioteca123.Models;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca123.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Sair()
        {
           HttpContext.Session.Clear();
           return RedirectToAction("Index", "Home");
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
          

            return View( new UsuarioService().Listar());
        }

        public IActionResult Registrar()
        {
            Autenticacao.CheckLogin(this);
           
            return View();
        }

        [HttpPost]

          public IActionResult Registrar(Usuario novoUser)
        {
            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);

            new UsuarioService().incluirUsuario(novoUser);

            return RedirectToAction("Listagem" , "Usuarios");
        }
        
          public IActionResult Editar(int Id)
        {
            Autenticacao.CheckLogin(this);
          

           return View(new UsuarioService().Listar(Id));
        }


       [HttpPost]

       public IActionResult Editar(Usuario user)
        {
            
            new UsuarioService().editarUsuario(user);
            return RedirectToAction("Listagem", "Usuarios");
        }
        
        public IActionResult excluirUsuario(int Id)
        {
             new UsuarioService().excluirUsuario(Id);

             return RedirectToAction("Listagem" , "Usuarios");
        }

    }
}