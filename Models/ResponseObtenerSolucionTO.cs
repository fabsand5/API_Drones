using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class ResponseObtenerSolucionTO
    {
        public ResponseObtenerSolucionTO()
        {
            this.error = new ErrorTO();
        }
        public ErrorTO error { get; set; }

        public SolucionTO solucion { get; set; }
       
    }
}
