using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca123.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {   
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
            
        }
        
        public Usuario Listar(int id)
        {   
           using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
            
        }

        public void incluirUsuario(Usuario u)
        {
             using (BibliotecaContext bc = new BibliotecaContext())
            {
               bc.Usuarios.Add(u);
               bc.SaveChanges();
            }
        }
        
        public void editarUsuario(Usuario u)
        {
              using (BibliotecaContext bc = new BibliotecaContext())
            {
               Usuario BD = bc.Usuarios.Find(u.Id);
               BD.Login = u.Login;
               BD.Nome = u.Nome;

               if(BD.Senha != u.Senha)
               {
                 BD.Senha = Criptografo.TextoCriptografado(u.Senha);
               }
               else{
                 BD.Senha = u.Senha;
               }
             
               BD.Tipo = u.Tipo;

               bc.SaveChanges();
            }
        }
        
        public void excluirUsuario(int Id)
        {
               using (BibliotecaContext bc = new BibliotecaContext())
            {
               bc.Usuarios.Remove(bc.Usuarios.Find(Id));
               bc.SaveChanges();
            }
            
        }
    }
}