using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using API_Drones.DAO;
using API_Drones.Models;

namespace API_Drones.Controllers
{
    [Produces("application/json")]
    [Route("api/drones")]
    [EnableCors("MyPolicy")]

    public class DronesController : ControllerBase
    {
        private DronesDAO dao;

        [HttpPost(nameof(registrarDron))]
        [EnableCors("MyPolicy")]
        public ResponseRegistrarDronTO registrarDron([FromBody] RequestRegistrarDronTO data)
        {
            ResponseRegistrarDronTO response = new ResponseRegistrarDronTO();
            try
            {
                    response.numeroSerie = dao.editarCourier(data, "editar");
            }
            catch (CourierExceptionTO ex)
            {
                _logger.LogError("Error en editarCourier " + ex.error.descripcion, ex);
                response.error.codigo = ex.error.codigo;
                response.error.descripcion = ex.error.descripcion;
            }
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                _logger.LogError("Error en editarCourier " + requestString + ex.Message, ex);
                response.error.codigo = 22;
                response.error.descripcion = ex.Message;
            }

            return response;
        }

    }
}
