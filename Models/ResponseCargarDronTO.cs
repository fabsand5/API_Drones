using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_drones.Models
{
    public class ResponseCargarDronTO
    {
        public ErrorTO error { get; set; }

        public String respuesta { get; set; }
        public ResponseCargarDronTO()
        {
            this.error = new ErrorTO();
        }
    }
}
