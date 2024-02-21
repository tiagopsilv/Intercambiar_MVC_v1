using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    [Table("fb_school")]
    public class fb_school
    {
        [Key]
        public int ID { get; set; }
        public string IdName { get; set; }
        public int IdLogin { get; set; }
        public string type { get; set; }
        public string year { get; set; }
    }
}