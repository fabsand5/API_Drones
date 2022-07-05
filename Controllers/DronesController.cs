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
using API_Drones.Services;
using ws_API_Drones.Services;

namespace API_Drones.Controllers
{
    [Produces("application/json")]
    [Route("api/drones")]
    [EnableCors("MyPolicy")]

    public class DronesController : ControllerBase
    {
        private readonly IDapper _dapper;
        private DronesDAO dao;
        private readonly ILoggerManager _logger;

        //Métodos
        [HttpPost(nameof(registrarDron))]
        [EnableCors("MyPolicy")]
        public ResponseRegistrarDronTO registrarDron([FromBody] RequestRegistrarDronTO data)
        {
            ResponseRegistrarDronTO response = new ResponseRegistrarDronTO();
            try
            {
                    response.numeroSerie = dao.registrarDron(data.dron);
            }
            catch (API_DronesException ex)
            {
                _logger.LogError("Error en registrarDron " + ex.error.descripcion, ex);
                response.error.codigo = ex.error.codigo;
                response.error.descripcion = ex.error.descripcion;
            }
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                _logger.LogError("Error en editarCourier " + requestString + ex.Message, ex);
                response.error.codigo = 1;
                response.error.descripcion = ex.Message;
            }

            return response;
        }


        [HttpPost(nameof(cargarDron))]
        [EnableCors("MyPolicy")]
        public ResponseCargarDronTO cargarDron([FromBody] RequestCargarDronTO data)
        {
            ResponseCargarDronTO response = new ResponseCargarDronTO();
            try
            {
                response.codigo = dao.cargarDron(data.medicamento);
            }
            catch (API_DronesException ex)
            {
                _logger.LogError("Error en registrarDron " + ex.error.descripcion, ex);
                response.error.codigo = ex.error.codigo;
                response.error.descripcion = ex.error.descripcion;
            }
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                _logger.LogError("Error en cargarDron " + requestString + ex.Message, ex);
                response.error.codigo = 2;
                response.error.descripcion = ex.Message;
            }

            return response;
        }

        [HttpPost(nameof(obtenerDrones))]
        [EnableCors("MyPolicy")]
        public ResponseObtenerDronesTO obtenerDrones()
        {
            ResponseObtenerDronesTO response = new ResponseObtenerDronesTO();
            try
            {
                response.listaDronesDisponibles = dao.obtenerDrones();
            }
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject("", Formatting.Indented);
                _logger.LogError("Error en obtenerHistorialSoluciones " + requestString + ex.Message, ex);
                response.error.codigo = 15;
                response.error.descripcion = ex.Message;
            }


            return response;
        }


        [HttpPost(nameof(comprobarEstadoBateria))]
        [EnableCors("MyPolicy")]
        public ResponseCargarDronTO comprobarEstadoBateria([FromBody] RequestCargarDronTO data)
        {
            ResponseCargarDronTO response = new ResponseCargarDronTO();
            try
            {
                response.codigo = dao.cargarDron(data.medicamento);
            }
            catch (API_DronesException ex)
            {
                _logger.LogError("Error en registrarDron " + ex.error.descripcion, ex);
                response.error.codigo = ex.error.codigo;
                response.error.descripcion = ex.error.descripcion;
            }
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                _logger.LogError("Error en cargarDron " + requestString + ex.Message, ex);
                response.error.codigo = 2;
                response.error.descripcion = ex.Message;
            }

            return response;
        }

    }
}
