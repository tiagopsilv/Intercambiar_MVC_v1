
using Intercambiar_mvc_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intercambiar_mvc_v1.Controllers
{
    public class ListaController : Controller
    {
        //
        // GET: /Lista/

        private ListaRepositorio repo = new ListaRepositorio();

        public ActionResult Listar(string tp, string or, string wl = "")
        {

            int result;

            ViewBag.ordem = or;
            ViewBag.Tipo = tp;

            if ((or != null && int.TryParse(or, out result)) && (tp == "n" || tp == "v" || tp == "b"))
            {
                int quantidadeNoticia = (int)Convert.ChangeType(or, typeof(int));

                IEnumerable<Lista> List;

                if (tp == "v")
                    List = repo.ListarVideo(quantidadeNoticia);
                else
                {
                    if (tp == "b")
                        List = repo.ListarBusca(quantidadeNoticia, wl);
                    else
                        List = repo.ListarNoticia(quantidadeNoticia);
                }

                return View("Listar", List);
            }
            else
                return RedirectToAction("Index", "Noticias");
        }

        public JsonResult GetDados(string quantidade)
        {
            IEnumerable<Lista> resultado = repo.ListarNoticia(0);
            return Json(resultado);
        }

    }
}
