using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class LoginRepositorio
    {
        private LoginContexto db = new LoginContexto();

        public Login SelLoginEmail(string Email)
        {
            List<Login> Lista;

            Lista = db.Login.Where(T => T.email == Email).ToList();
            if (Lista.Count > 0)
            {
                return Lista.First();
            }
            else
            {
                return null;
            }
        }

        public string ValidaLogin(string Email, string Senha)
        {
             List<Login> ListaEmail;

            ListaEmail = db.Login.Where(T => T.email == Email).ToList();
            if (ListaEmail.Count > 0)
            {
                List<Login> ListaSenha;

                ListaSenha = db.Login.Where(T => T.email == Email && T.Senha == Senha).ToList();
                if (ListaSenha.Count <= 0)
                {
                    return "Senha invalido";
                }
                else
                    return "";
            }
            else
            {
                return "E-mail invalido";
            }
        }

        public bool UpdLogin(Login _Login)
        {
            try
            {
                List<Login> _Cont = db.Login.Where(T => T.ID == _Login.ID).ToList();

                _Cont.ForEach(x =>
                {
                    x.email = _Login.email;
                    x.Facebook = _Login.Facebook;
                    x.Recuperar = _Login.Recuperar;
                    x.Senha = _Login.Senha;
                });

                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool AddLogin(Login _Login)
        {
            try
            {

                db.Login.Add(_Login);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

    }
}