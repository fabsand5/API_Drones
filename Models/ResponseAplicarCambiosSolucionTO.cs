using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class ResponseAplicarCambiosSolucionTO
    {
        public ResponseAplicarCambiosSolucionTO()
        {
            this.error = new ErrorTO();
        }
        public ErrorTO error { get; set; }

        public long solucionId { get; set; }
    }
}
