using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Drones.Models
{
    public class API_DronesException: Exception
    {
        public ErrorTO error { get; set; }

        public API_DronesException() { }

        public API_DronesException(int codigo, String descripcion)
        {
            this.error = new ErrorTO();
            this.error.codigo = codigo;
            this.error.descripcion = descripcion;
        }
    }
}
