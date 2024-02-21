using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Param")]
    public class Param
    {
        [Key]
        public int ID { get; set; }
        public string Chave_Param { get; set; }
        public string Ds_Param { get; set; }
    }
}