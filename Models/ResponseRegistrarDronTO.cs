using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Drones.Models
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
