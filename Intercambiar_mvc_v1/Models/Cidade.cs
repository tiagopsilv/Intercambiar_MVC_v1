using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        public int ID { get; set; }
        public string nome { get; set; }
        public int Estado { get; set; }
    }
}