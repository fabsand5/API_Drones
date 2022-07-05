using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rs_drones.Models
{
    public class ResponseRegistrarDronTO
    {
        public ErrorTO error { get; set; }
        public long numeroSerie { get; set; }
        public ResponseRegistrarDronTO()
        {
            this.error = new ErrorTO();
        }
    }
}
