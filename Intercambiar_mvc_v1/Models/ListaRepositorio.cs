using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class ListaRepositorio
    {

        private NoticiaRepositorio NoticiaRepo = new NoticiaRepositorio();
        private VideoRepositorio VideoRepo = new VideoRepositorio();

        public IEnumerable<Lista> ListarNoticia(int ordem) 
        {

            int ID = (5 * ordem - 4);

            IEnumerable<Lista> _Lista = NoticiaRepo.ListarNoticas_Top(ID, 5);

            return _Lista;
        }

        public Lista MontaLinkLista(Lista _Lista)
        {

            string letra = "Empty";
            int Posicao = 50;

            if (_Lista.Texto.Length > Posicao)
            {
                while (letra != " ")
                {
                    letra = _Lista.Texto.Substring(Posicao, 1);
                    Posicao += 1;
                }
            }

            _Lista.Texto = _Lista.Texto.Substring(0, Posicao).Replace("<p>", "").Replace("</p>", "") + "...";
            return _Lista;
        }

        public IEnumerable<Lista> ListarVideo(int ordem)
        {

            int ID = (5 * ordem - 4);

            IEnumerable<Lista> _Lista = VideoRepo.ListarVideo_Top(ID, 5);

            return _Lista;
        }

        public IEnumerable<Lista> ListarBusca(int ordem, string Where)
        {

            int ID = (5 * ordem - 4);

            IEnumerable<Lista> _ListaVideo = VideoRepo.ListarVideo_Top(ID, 5, Where);

            IEnumerable<Lista> _ListaNoticia = NoticiaRepo.ListarNoticas_Top(ID, 5, Where);

            return ((from t in _ListaNoticia
                     select new Lista { ID = t.ID, Titulo = t.Titulo, Texto = t.Texto, Imagem = t.Imagem, URL = t.URL })
                    .Union(from t in _ListaVideo
                     select new Lista { ID = t.ID, Titulo = t.Titulo, Texto = t.Texto, Imagem = t.Imagem, URL = t.URL })).ToList(); ;
        }


    }
}