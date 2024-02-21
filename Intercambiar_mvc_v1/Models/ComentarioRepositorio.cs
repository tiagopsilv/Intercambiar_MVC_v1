using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class ComentarioRepositorio
    {
        private ComentarioContexto db = new ComentarioContexto();

        public bool AddComentario(Comentario Co)
        {
            try
            {

                db.Comentario.Add(Co);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public IEnumerable<Comentario> ListarComentarios(int idNoticia)
        {

            IEnumerable<Comentario> _Comentarios = db.Comentario.Where(T => T.id_Noticias == idNoticia).OrderByDescending(T => T.DataHora);

            return _Comentarios;
        }
    }
}