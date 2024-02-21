using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Noticia")]
    public class Noticia
    {
        [Key]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string  Autor { get; set; }
        public DateTime Data { get; set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }
        public bool Destaque { get; set; }
        public int id_Autor { get; set; }

    }
}