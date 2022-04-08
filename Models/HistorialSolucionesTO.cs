using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class HistorialSolucionesTO
    {
        public String fecha {get; set;}
        public String observacionReglaAplicada { get; set; }
        public String usuario { get; set; }
        public String tipoObjeccion { get; set; }
        public String solucionAplicadaDescripcion { get; set; }
    }
}
