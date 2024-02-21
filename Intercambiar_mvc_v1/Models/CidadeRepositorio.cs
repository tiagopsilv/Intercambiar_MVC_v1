using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class CidadeRepositorio
    {
        private CidadeContexto db = new CidadeContexto();

        public IEnumerable<Cidade> ListarCidades(int IdEstado)
        {

            IEnumerable<Cidade> _Cidades = db.Cidade.OrderBy(T => T.nome).Where(T => T.Estado == IdEstado);

            return _Cidades;
        }
    }
}