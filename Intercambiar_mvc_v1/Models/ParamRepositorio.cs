using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intercambiar_mvc_v1.Models
{
    public class ParamRepositorio
    {
        private ParamContexto db = new ParamContexto();
        public string ListaParam(string Chave_Param)
        {

            Param P = db.Param.Where(T => T.Chave_Param == Chave_Param).Take(1).ToList().First();
            return P.Ds_Param;
        }
    }
}