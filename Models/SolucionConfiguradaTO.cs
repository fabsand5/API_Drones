using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class SolucionConfiguradaTO
    {
        public long solucionId { get; set; }
        public String solucionDesc { get; set; }
        public int cantMaxAplicar { get; set; }
        public long baseId { get; set; }
        public long solucionConfiguradaId { get; set; }
        public String solucionConfiguradaDesc { get; set; }
        public long familiaObjeccionId { get; set; }
        public String tipoObjeccionCodigo { get; set; }
        public String tipoObjeccionDescripcion { get; set; }
    }
}
