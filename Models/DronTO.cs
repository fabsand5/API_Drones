using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Drones.Models
{
    public class DronTO
    {
        public long numeroSerie { get; set; }
        public String modelo { get; set; }
        public int pesoLimite { get; set; }
        public int capacidadBateria { get; set; }
        public String estado { get; set; }
    }
}
