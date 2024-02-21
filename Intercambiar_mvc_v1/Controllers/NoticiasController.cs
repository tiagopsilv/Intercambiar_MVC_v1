using Intercambiar_mvc_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intercambiar_mvc_v1.Controllers
{
    public class NoticiasController : Controller
    {
        //
        // GET: /Noticias/

        private NoticiaRepositorio repo = new NoticiaRepositorio();
        private VideoRepositorio repoVideo = new VideoRepositorio();
        private Autenticar Autent = new Autenticar();
        private EstadoRepositorio repoEstado = new EstadoRepositorio();

        public ActionResult Index()
        {

            var dados = repo.ListarPrimeiraNoticia();
            VideoRepositorio repoVideo = new VideoRepositorio();
            
            ViewBag.PrimeiraNoticia = dados;

            ViewBag.ColunaMeio = repo.ListarColunaMeio();

            ViewBag.Destaque = repo.ListarNoticiaDestaque();

            ViewBag.ListarNoticasAntigasIndex = repo.ListarNoticasAntigasIndex();

            ViewBag.Video = repoVideo.ListarPrimeiraVideo();

            HttpCookie cookie = Request.Cookies[Autent.get_chaveCriptografia()];

            if (cookie != null)
                Autent.UsuarioLogado(cookie.Value, "Index");
            else
                Autent.UsuarioLogado("Index");

            return View();
        }

        public ActionResult LerNoticia(string ID)
        {

            int result;

            if (ID != null && int.TryParse(ID, out result))
            {
                int IDNoticia = (int)Convert.ChangeType(ID, typeof(int));
                if (repo.NoticiaExiste(IDNoticia))
                {
                    var _Noticia = repo.BuscarNoticia(IDNoticia);

                    NoticiaRepositorio repoNoticiaRecentes = new NoticiaRepositorio();

                    ViewBag.NoticiaRecentes = repoNoticiaRecentes.ListarNoticiaRecentes(IDNoticia);

                    ViewBag.Destaque = repoNoticiaRecentes.ListarNoticiaDestaque();
                    ViewBag.LinkTwitter = repoNoticiaRecentes.RemoveAccents(_Noticia.Titulo);

                    HttpCookie cookie = Request.Cookies[Autent.get_chaveCriptografia()];

                    if (cookie != null)
                        Autent.UsuarioLogado(cookie.Value, "LerNoticia/" + ID.ToString());
                    else
                        Autent.UsuarioLogado("LerNoticia/" + ID.ToString());

                    return View("LerNoticia", _Noticia);
                }
                else
                    return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult Video(string ID)
        {
            int result;

            if (ID != null && int.TryParse(ID, out result))
            {
                Video _Video = repoVideo.ListaVideo((int)Convert.ChangeType(ID, typeof(int)));

                HttpCookie cookie = Request.Cookies[Autent.get_chaveCriptografia()];

                if (cookie != null)
                    Autent.UsuarioLogado(cookie.Value, "Video");
                else
                    Autent.UsuarioLogado("Video");

                return View("Video", _Video);
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult Sobre()
        {
            HttpCookie cookie = Request.Cookies[Autent.get_chaveCriptografia()];

            if (cookie != null)
                Autent.UsuarioLogado(cookie.Value, "Sobre");
            else
                Autent.UsuarioLogado("Sobre");

            return View();
        }

        public ActionResult Contato()
        {
            HttpCookie cookie = Request.Cookies[Autent.get_chaveCriptografia()];

            if (cookie != null)
                Autent.UsuarioLogado(cookie.Value, "Contato");
            else
                Autent.UsuarioLogado("Contato");

            return View();
        }

        public JsonResult GetComentario(int IdComentario)
        {
            ComentarioRepositorio repoComentari = new ComentarioRepositorio();
            UsuarioRepositorio UsuarioRep = new UsuarioRepositorio();
            Usuario U;
            IList<string> Lista = new List<string>();

            IEnumerable<Comentario> Comentari = repoComentari.ListarComentarios(IdComentario);

            foreach (Comentario C in Comentari)
            {
                U = UsuarioRep.SelUsuario(C.id_Usuario);
                Lista.Add("<li><img src='" + U.Foto + "' class='Texto_Comentario_Caixa_Foto' /><h2>" + U.Nome + "</h2> <h1>" + C.Texto + " </h1></li>");
            }

            return Json(Lista, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Comentar(int IdComentario, string Texto)
        {
            HttpCookie cookie = Request.Cookies[Autent.get_chaveCriptografia()];

            string bCerto = "nao";

            if (Texto != null)
            {
                if (cookie != null && Texto.Length > 0 && Texto.Replace("(*0*)", "").Replace("(*1*)", "") != "")
                    if (Autent.UsuarioLogado(cookie.Value, "Comentar", "LerNoticia/" + IdComentario.ToString()) == true)
                    {
                        ComentarioRepositorio repoComentari = new ComentarioRepositorio();
                        Usuario _Usuario = Autent.GetUsuario(Autent.GetCookie(cookie.Value).email);

                        Comentario C = new Comentario();
                        C.id_Noticias = IdComentario;
                        C.id_Usuario = _Usuario.Id;
                        C.Texto = Texto.Replace("(*0*)", "<p>").Replace("(*1*)", "</p>");
                        C.DataHora = DateTime.Now;

                        repoComentari.AddComentario(C);

                        bCerto = "sim";

                    }
            }

            return Json(new { result = bCerto});
        }

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
