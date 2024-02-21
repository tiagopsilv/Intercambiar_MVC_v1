using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Intercambiar_mvc_v1.Models
{
    [Table("Comentario")]
    public class Comentario
    {
        [Key]
        public int ID { get; set; }
        public int id_Noticias { get; set; }
        public int id_Usuario { get; set; }
        public string Texto { get; set; }
        public DateTime DataHora { get; set; }
    }
}