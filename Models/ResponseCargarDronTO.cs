using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Drones.Models
{
    public class ResponseCargarDronTO
    {
        public ErrorTO error { get; set; }

        public String codigo { get; set; }
        public ResponseCargarDronTO()
        {
            this.error = new ErrorTO();
        }
    }
}
