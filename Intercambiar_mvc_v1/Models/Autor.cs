using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Autor")]
    public class Autor
    {
        [Key]
        public int id { get; set; }
        public int id_Usuario { get; set; }
        public string Descricao { get; set; }
    }
}