using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class Perfil
    {
        public int IdUsuario { get; set; }
        public int IdLogin { get; set; }
        public long Pontuacao { get; set; }
        public int Estrelas { get; set; }
        public long Textos { get; set; }
        public long Viagens { get; set; }
        public long Comentarios { get; set; }
        public long Gols { get; set; }
    }
}