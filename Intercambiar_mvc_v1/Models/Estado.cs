using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Estado")]
    public class Estado
    {
        [Key]
        public int ID { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }
        public int pais { get; set; }
    }
}