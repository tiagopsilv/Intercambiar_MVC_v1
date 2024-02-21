using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Video")]
    public class Video
    {
        [Key]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public string URL { get; set; }
    }
}