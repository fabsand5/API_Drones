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

        [HttpPost(nameof(editarCourier))]
        [EnableCors("MyPolicy")]
        public ResponseEditarDronTO editarCourier([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestEditarDronTO data)
        {
            ResponseEditarDronTO response = new ResponseEditarDronTO();
            try
            {
                if (data.idCourier != 0)
                    response.courierId = dao.editarCourier(data, "editar");
                else
                    response.courierId = dao.editarCourier(data, "crear");

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
