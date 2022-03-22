using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class ResponseObtenerListaSolucionesTO
    {
        public ResponseObtenerListaSolucionesTO()
        {
            this.error = new ErrorTO();
        }
        public ErrorTO error { get; set; }

        public List<SolucionTO> listaSolucion { get; set; }
    }
}
