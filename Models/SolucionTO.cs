using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class SolucionTO
    {
        public int solucionId { get; set; }
        public int bodegaId { get; set; }
        public String descripcion { get; set; }
        public String estado { get; set; }
    }
}
