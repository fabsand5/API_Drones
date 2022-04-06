using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class ResponseSolucionTO
    {
        public ResponseSolucionTO()
        {
            this.error = new ErrorTO();
        }
        public ErrorTO error { get; set; }

        public long solucionId { get; set; }
        public long solucionConfiguradaId { get; set; }
        public String solucionDescripcion { get; set; }


    }
}
