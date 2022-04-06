﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_rendicion.Models
{
    public class RequestAplicarCambiosSolucionTO
    {
        public long baseId { get; set; }
        public long documentoId { get; set; }
        public String usuario { get; set; }
        public String observacion { get; set; }
        public String tipoObjeccionDesc { get; set; }
        public String codTipoObjeccion { get; set; }
        public int reglaId { get; set; }

        public long solucionId { get; set; }

        //public long idDocumento { get; set; }

    }
}
