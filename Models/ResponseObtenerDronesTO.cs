﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Drones.Models
{
    public class ResponseObtenerDronesTO
    {
        public ErrorTO error { get; set; }
        public List<DronTO> listaDronesDisponibles { get; set; }

        public ResponseObtenerDronesTO()
        {
            this.error = new ErrorTO();
        }
    }
}
