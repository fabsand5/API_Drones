using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class ValeCargaMonitorTO
    {
        public long valeCargaId { get; set; }
        public String estadoCodigo { get; set; }
        public String estadoDescripcion { get; set; }
        public long courierId { get; set; }
        public String courierNombre { get; set; }
        public long zonaId { get; set; }
        public String zonaCodigo { get; set; }
        public String zonaDescripcion { get; set; }
        public String fechaCreacion { get; set; }
        public String fechaCierre { get; set; }
        public String fechaAsignacion { get; set; }
        public int cantidadDocumentos { get; set; }
    }
}
