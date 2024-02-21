using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Login")]
    public class Login
    {
        [Key]
        public int ID { get; set; }
        public string email { get; set; }
        public string Senha { get; set; }
        public bool Recuperar { get; set; }
        public bool Facebook { get; set; }
    }
}