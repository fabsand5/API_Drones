using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class ResponseObtenerHistorialSolucionesTO
    {
        public ResponseObtenerHistorialSolucionesTO()
        {
            this.error = new ErrorTO();
        }
        public ErrorTO error { get; set; }

        public List<HistorialSolucionesTO> listaHistorialSoluciones { get; set; }
    }
}
