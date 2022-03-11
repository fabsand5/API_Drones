using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class RequestObtenerListaValeCargaTO
    {
        public long valeCargaId { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string codigoEstado { get; set; }

    }
}
