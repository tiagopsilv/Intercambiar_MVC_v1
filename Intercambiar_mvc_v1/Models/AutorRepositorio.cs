using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class AutorRepositorio
    {
        private AutorContexto db = new AutorContexto();

        public Autor SelAutor(int id_Usuario)
        {

            Autor _Autor = db.Autor.Where(T => T.id_Usuario == id_Usuario).Take(1).ToList().First();

            return _Autor;
        }

        public int ContAutor(int id_Usuario)
        {
            return db.Autor.Where(T => T.id_Usuario == id_Usuario).Count();
        }

    }
}