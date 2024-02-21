using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class VideoRepositorio
    {
        private VideoContexto db = new VideoContexto();

        public Video ListarPrimeiraVideo()
        {
            //Lista mais recente.
            return db.Video.OrderByDescending(T => T.Data).Take(1).ToList().First();
        }

        public Video ListaVideo(int IDVideo)
        {

            return db.Video.Where(T => T.ID == IDVideo).Take(1).ToList().First();
        }

        public IEnumerable<Lista> ListarVideo_Top(int ID, int quant, string Where = "")
        {
            string strLink = @"/Noticias/Video/";

            IEnumerable<Lista> _Lista;

            if (Where == "")
            {
                _Lista = (from t in db.Video
                    where t.ID >= ID
                    select new Lista { ID = t.ID, Titulo = t.Titulo, Texto = "Veja o video.", Imagem = t.Titulo, URL = strLink + t.ID.ToString() }).Take(quant);
            }
            else
            {
                _Lista = (from t in db.Video
                          where t.ID >= ID && t.Titulo.Contains(Where)
                    select new Lista { ID = t.ID, Titulo = t.Titulo, Texto = "Veja o video.", Imagem = t.Titulo, URL = strLink + t.ID.ToString() }).Take(quant);
            }

            return _Lista;

        }

    }
}