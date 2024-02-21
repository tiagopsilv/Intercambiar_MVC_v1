using Intercambiar_mvc_v1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intercambiar_mvc_v1.Controllers
{
    public class SocialController : Controller
    {
        //
        // GET: /Social/

        public ActionResult Destinos()
        {
            return View();
        }

        public ActionResult ListaPais(string ipa)
        {
            return View();
        }


        public ActionResult Pais(string ipai)
        {
            return View();
        }

        public ActionResult Cidade(string icdi)
        {
            return View();
        }

        public ActionResult eufui(string idex)
        {
            return View();
        }

        public ActionResult CadastroCelular()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            Autenticar a = new Autenticar();
            HttpCookie cookie = Request.Cookies[a.get_chaveCriptografia()];

            if (cookie != null)
            {
               
                a.UsuarioLogado(cookie.Value, "Perfil");

                Perfil P = a.GetPerfil(a.GetUsuario(a.GetCookie(cookie.Value).email));

                //ViewBag.VPerfil = P;

               return View("Perfil", P);
            }
            else
            {
                return RedirectToAction("Index", "Noticias");
            }
        }

        [HttpPost]
        public ActionResult FileUp(HttpPostedFileBase file)
        {
            Autenticar a = new Autenticar();
            HttpCookie cookie = Request.Cookies[a.get_chaveCriptografia()];

            if (cookie != null)
            {
                    
                if (a.UsuarioLogado(cookie.Value, "FileUp"))
                {
                    if (file != null && file.ContentLength > 0)
                        try
                        {
                            string path = Path.Combine(Server.MapPath("~/Images"),
                                                       Path.GetFileName(file.FileName));
                            file.SaveAs(path);
                            ViewBag.Message = "File uploaded successfully";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        }
                    else
                    {
                        ViewBag.Message = "You have not specified a file.";
                    }
                }
            }
            return View();
        }

        public ActionResult Administrador()
        {
            Autenticar a = new Autenticar();
            HttpCookie cookie = Request.Cookies[a.get_chaveCriptografia()];
            bool Valida = false;

            if (cookie != null)
            {

                if (a.UsuarioLogado(cookie.Value, "Administrador"))
                {
                    Usuario _Usuario = a.GetUsuario(a.GetCookie(cookie.Value).email);
                    AutorRepositorio _AutorRepositorio = new AutorRepositorio();

                    if (_AutorRepositorio.ContAutor(_Usuario.Id) > 0)
                    {
                        IList<Noticia_Relat> LstNoticia_Relat = a.Sel_Noticias_Relat(_Usuario.Id);

                        ViewBag.Noticia_Relat = LstNoticia_Relat;

                        Valida = true;
                    }
                }
                
            }

            if (Valida)
                return View();
            else
                return RedirectToAction("Index", "Noticias");

        }

        [HttpPost]
        public JsonResult Logar(Login Log)
        {
            Autenticar a = new Autenticar();

            Usuario _Usuario;
            AutorRepositorio _AutorRepositorio = new AutorRepositorio();
            bool Autor = false;


            string Nome = "";
            string Foto = "";
            string Mensagem = a.AutenticarUsuario(Log.email, Log.Senha);
            bool Valido = (Mensagem!="")?false:true;

            if (Valido == true)
            {
                _Usuario = a.GetUsuario(Log.email);
                Nome = _Usuario.Nome;
                Foto = _Usuario.Foto;
                if (_AutorRepositorio.ContAutor(_Usuario.Id) > 0)
                    Autor = true;    
            }

            return Json(new { resultado = Valido, resultadoMensagem = Mensagem, resultadoNome = Nome, resultadoFoto = Foto, resultadoAutor = Autor });
        }

        [HttpPost]
        public JsonResult Logoff()
        {
            Autenticar a = new Autenticar();
            string result = "0";

            if (a.LimparCookie(a.get_chaveCriptografia().ToString()) == true)
            {
                result = "1";
            }
            else
            {
                result = "0";
            }
            return Json(new { resultado = result });
        }

        public JsonResult chaveCookie()
        {
            Autenticar a = new Autenticar();
            string Nome = "";
            string Foto = "";
            string Chave = "";

            HttpCookie cookie = Request.Cookies[a.get_chaveCriptografia()];

            if (cookie != null)
            {
                a.AutenticarUsuario(cookie.Value, "chaveCookie");
                cookie = Request.Cookies[a.get_chaveCriptografia()];
            }

            if (cookie != null)
            {
                
                Usuario _Usuario = a.GetUsuario(a.GetCookie(cookie.Value).email);
                Nome = _Usuario.Nome;
                Foto = _Usuario.Foto;
                Chave = a.get_chaveCriptografia().ToString();
            }

            return Json(new { chave = Chave, resultadoNome = Nome, resultadoFoto = Foto });
        }

        //public JsonResult FacebookAuthenticated()
        //{

        //}

        public JsonResult GetFacebookLoginUrl(string Dis, string url)
        {
            Autenticar a = new Autenticar();

            var urlResult = a.GetFacebookLoginUrl(Dis, url);

            return Json(new { url = urlResult.ToString() });
        }

        public JsonResult VerificarEmail(string _Email)
        {
            bool result = false;
            LoginRepositorio _Log = new LoginRepositorio();
            Login L = _Log.SelLoginEmail(_Email);

            if (L != null)
                result = true;


            return Json(new { existe = result });
        }

        public ActionResult RetornoFb()
        {
            Autenticar a = new Autenticar();
            a.RetornoFb(Request.Url);

            string URL = "http://www.intercambiar.com.br/";
            HttpCookie cookie = Request.Cookies["IntercambiarUrlVoltaFB"];

            if (cookie != null)
                URL = cookie.Value;

            a.LimparCookie("IntercambiarUrlVoltaFB");

            return Redirect(URL);
        }


        public JsonResult GetEstados()
        {
            EstadoRepositorio repoEstado = new EstadoRepositorio();
            var Estado = repoEstado.ListarEstados();

            return Json(Estado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCidades(int IdEstado)
        {
            CidadeRepositorio repoCidade = new CidadeRepositorio();
            var Cidade = repoCidade.ListarCidades(IdEstado);

            return Json(Cidade, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddUser(string Email,
                                    string Nome,
                                    string locate,
                                    string sexo,
                                    string Endereco,
                                    string CEP,
                                    string Cidade,
                                    string Estado,
                                    string Fone,
                                    string Celular,
                                    string Escolaridade,
                                    string Foto,
                                    string Facebooku,
                                    string Primeiro_Nome,
                                    string Sobrenome,
                                    DateTime DtNasc,
                                    string Senha)
        {

            bool result = false;
            Usuario U = new Usuario();
            Autenticar a = new Autenticar();
            LoginRepositorio _Log = new LoginRepositorio();
            Login L = _Log.SelLoginEmail(Email);

            if (L != null)
                result = true;
            else 
                result = a.ValidaEmail(Email);

            if (Primeiro_Nome.Trim().Length <= 0)
            {
                result = false;
            }

            if (Sobrenome.Trim().Length <= 0)
            {
                result = false;
            }

            if (Fone.Trim().Length <= 0)
            {
                result = false;
            }

            if (DtNasc.ToString().Length  <= 0)
            {
                result = false;
            }

            if (CEP.Trim().Length <= 0)
            {
                result = false;
            }

            if (Endereco.Trim().Length <= 0)
            {
                result = false;
            }

            if (Cidade.Trim().Length <= 0)
            {
                result = false;
            }

            if (Estado.Trim().Length <= 0)
            {
                result = false;
            }

            if (Senha.Trim().Length <= 0)
            {
                result = false;
            }

            if (result)
            {

				U.Email = Email; 
				U.Nome = Nome; 
				U.locale = locate; 
				U.sexo = sexo;
				U.Endereco = Endereco;
				U.CEP = CEP;
				U.Cidade = Cidade;
				U.Estado = Estado;
				U.Fone = Fone;
				U.Celular = Celular;
				U.Escolaridade = Escolaridade;
				U.Foto = Fone;
				U.Primeiro_Nome = Primeiro_Nome;
				U.Sobrenome = Sobrenome;
                U.DtNasc = DtNasc;

                a.AdicionarUsuario(U, Senha); 
            }

            return Json(new { resultado = result });
        }


        [HttpPost]
        public bool VerificaAutor()
        {
            Autenticar Autent = new Autenticar();
            HttpCookie cookie = Request.Cookies[Autent.get_chaveCriptografia()];
            bool Result = false;

            if (cookie != null)
                if (Autent.UsuarioLogado(cookie.Value, "VerificaAutor") == true)
                {
                    Usuario _Usuario = Autent.GetUsuario(Autent.GetCookie(cookie.Value).email);

                    AutorRepositorio _AutorRepositorio = new AutorRepositorio();

                    if (_AutorRepositorio.ContAutor(_Usuario.Id) > 0)
                    {
                        IList<Noticia_Relat> LstNoticia_Relat = Autent.Sel_Noticias_Relat(_Usuario.Id);

                        ViewBag.Noticia_Relat = LstNoticia_Relat;

                        Result = true;
                    }
                }

            return Result;
        }

    }
}
