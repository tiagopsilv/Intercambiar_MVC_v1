using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class EstadoRepositorio
    {
        private EstadoContexto db = new EstadoContexto();

        public IEnumerable<Estado> ListarEstados()
        {

            IEnumerable<Estado> _Estados = db.Estado.OrderBy(T => T.nome);

            return _Estados;
        }
    }
}