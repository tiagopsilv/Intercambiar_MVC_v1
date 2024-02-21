using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("LogLogin")]
    public class LogLogin
    {
        [Key]
        public int ID { get; set; }
        public int Id_Login { get; set; }
        [Column("LogLogin")]
        public DateTime Log { get; set; }
        public bool LogFace { get; set; }
        public string Token { get; set; }
        public bool getInfo { get; set; }

    }
}