using System.Collections.Generic;

namespace rs_rendicion.Models
{
    public class ResponseObtenerListaValeCargaTO
    {
        public ResponseObtenerListaValeCargaTO()
        {
            this.error = new ErrorTO();
        }
        public ErrorTO error { get; set; }
        public List<ValeCargaMonitorTO> listaValesCarga { get; set; }
    }
}
