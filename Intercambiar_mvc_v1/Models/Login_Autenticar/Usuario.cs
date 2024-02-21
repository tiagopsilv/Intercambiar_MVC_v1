using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Intercambiar_mvc_v1.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
	    public int IdLogin { get; set; }
	    public string Email { get; set; }
	    public string Nome { get; set; }
	    public string Endereco { get; set; }
	    public string CEP { get; set; }
	    public string Cidade { get; set; }
	    public string Estado { get; set; }
	    public string Fone { get; set; }
	    public string Celular { get; set; }
	    public string Escolaridade { get; set; }
	    public string Foto { get; set; }
	    public string Facebooku { get; set; }
	    public string sexo { get; set; }
	    public string locale { get; set; }
        public DateTime dtCadastro { get; set; }
        public DateTime dtAtualizacao { get; set; }
        public string Primeiro_Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DtNasc { get; set; }
    }
}