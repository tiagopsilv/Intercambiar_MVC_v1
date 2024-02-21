using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class NoticiaRepositorio
    {

        public string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        } 

        private NoticiaContexto db = new NoticiaContexto();

        public Noticia ListarPrimeiraNoticia()
        {
            //Lista mais recente.
            var _Noticia = db.Noticia.OrderByDescending(T => T.Data).Take(1).ToList();
            return MontaLinkNoticia(_Noticia.First());
        }

        private Noticia MontaLinkNoticia(Noticia _Noticia)
        {

            string letra = "Empty";
            int Posicao = 150;


            if (_Noticia.Texto.Length > Posicao)
            {
                while (letra != " " && _Noticia.Texto.Length != Posicao)
                {
                    letra = _Noticia.Texto.Substring(Posicao, 1);
                    Posicao += 1;
                }
            }
            else
            {
                Posicao = _Noticia.Titulo.Length;
            }

            _Noticia.Texto = _Noticia.Texto.Substring(0, Posicao).Replace("<p>", "").Replace("</p>", "") + "...";
            return _Noticia;
        }

        public IEnumerable<Noticia> ListarColunaMeio()
        {
            int IdPrimeiro = ListarPrimeiraNoticia().ID;

            IEnumerable<Noticia> _Noticias = db.Noticia.OrderByDescending(T => T.Data).Where(T => T.Destaque == false && T.ID != IdPrimeiro).Take(4);

            for (int i = 0; i <= (_Noticias.ToList().Count - 1); i++)
            {
                _Noticias.ToList()[i] = MontaLinkNoticia(_Noticias.ToList()[i]);
            }

            return _Noticias;
        }

        public Noticia ListarNoticiaDestaque()
        {
            int IdPrimeiro = ListarPrimeiraNoticia().ID;
            //Lista mais recente.
            var _Noticia = db.Noticia.OrderByDescending(T => T.Data).Where(T => T.Destaque == true && T.ID != IdPrimeiro).Take(1).ToList();
            return MontaLinkNoticia(_Noticia.First());
        }

        public Noticia BuscarNoticia(int IdNoticia)
        {
            return db.Noticia.Where(T => T.ID == IdNoticia).Take(1).ToList().First();
        }

        public bool NoticiaExiste(int IdNoticia)
        {
            return db.Noticia.Any(T => T.ID == IdNoticia);
        }

        public IEnumerable<Noticia> ListarNoticiaRecentes(int IdNoticia)
        {
            int IdPrimeiro = ListarPrimeiraNoticia().ID;

            IEnumerable<Noticia> _Noticias = db.Noticia.OrderByDescending(T => T.Data).Where(T => T.ID != IdPrimeiro).Take(4);

            for (int i = 0; i <= (_Noticias.ToList().Count - 1); i++)
            {
                _Noticias.ToList()[i] = MontaLinkNoticia(_Noticias.ToList()[i]);
            }

            return _Noticias;
        }

        public IEnumerable<Noticia> ListarNoticasAntigasIndex()
        {
    
            var IdPrimeiro = db.Noticia.OrderByDescending(T => T.Data).Take(5).Select(T => T.ID);
            
            IEnumerable<Noticia> _Noticias = db.Noticia.OrderByDescending(T => T.Data).Where(T => !IdPrimeiro.ToList().Contains(T.ID)).Take(4);

            for (int i = 0; i <= (_Noticias.ToList().Count - 1); i++)
            {
                _Noticias.ToList()[i] = MontaLinkNoticia(_Noticias.ToList()[i]);
            }

            return _Noticias;
        }

        public IEnumerable<Lista> ListarNoticas_Top(int ID, int quant, string Where = "")
        {
            string strLink = @"/Noticias/LerNoticia/";

            IEnumerable<Lista> _Lista;

            if (Where == "")
            {
                _Lista = (from t in db.Noticia
                          where t.ID > ID
                          select new Lista { ID = t.ID, Titulo = t.Titulo, Texto = t.Texto, Imagem = t.Imagem, URL = strLink + t.ID.ToString() }).Take(quant);
            }
            else
            {
                _Lista = (from t in db.Noticia
                          where t.ID > ID && (t.Titulo.Contains(Where) || t.Texto.Contains(Where))
                          select new Lista { ID = t.ID, Titulo = t.Titulo, Texto = t.Texto, Imagem = t.Imagem, URL = strLink + t.ID.ToString() }).Take(quant);
            }

            return _Lista;
        
        }

    }
}