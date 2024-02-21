using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class Noticia_Relat
    {
        public int id { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public int Acesso { get; set; }
        public int Comentario { get; set; }
        public int Novo { get; set; }
    }
}