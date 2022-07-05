using lib_session.DAO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rs_drones.DAO;
using rs_drones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ws_drones.Services;

namespace rs_drones.Controllers
{
    [Produces("application/json")]
    [Route("api/drones")]
    [EnableCors("MyPolicy")]
    public class dronesController : ControllerBase
    {
        private readonly IDapper _dapper;

        private dronesDao dao;
        private SessionDAO sessionDao;

        private readonly ILoggerManager _logger;

        public dronesController(IDapper dapper, ILoggerManager logger)
        {
            _dapper = dapper;
            dao = new dronesDao(dapper);
            sessionDao = new SessionDAO(dapper);
            this._logger = logger;
        }


        private ErrorTO errorSessionInvalida()
        {
            ErrorTO error = new ErrorTO();
            error.codigo = 21;
            error.descripcion = "Usuario/Token Invalidos";
            return error;
        }


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
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                _logger.LogError("Error en registrarDron " + requestString + ex.Message, ex);
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
                response.respuesta = dao.cargarDron(data.codMedicamento, data.numSerieDron);
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
                _logger.LogError("Error en obtenerDrones " + requestString + ex.Message, ex);
                response.error.codigo = 3;
                response.error.descripcion = ex.Message;
            }

            return response;
        }


        [HttpPost(nameof(comprobarNivelBateria))]
        [EnableCors("MyPolicy")]
        public ResponseCargarDronTO comprobarNivelBateria([FromBody] RequestComprobarDronTO data)
        {
            ResponseCargarDronTO response = new ResponseCargarDronTO();
            try
            {
                response.respuesta = dao.comprobarNivelBateria(data.numeroSerie);
            }
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                _logger.LogError("Error en comprobarEstadoBateria " + requestString + ex.Message, ex);
                response.error.codigo = 4;
                response.error.descripcion = ex.Message;
            }

            return response;
        }


        [HttpPost(nameof(comprobarPesoMedicamentosDron))]
        [EnableCors("MyPolicy")]
        public ResponseCargarDronTO comprobarPesoMedicamentosDron([FromBody] RequestComprobarDronTO data)
        {
            ResponseCargarDronTO response = new ResponseCargarDronTO();
            try
            {
                response.respuesta = dao.comprobarPesoMedicamentosDron(data.numeroSerie);
            }
            catch (Exception ex)
            {
                String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                _logger.LogError("Error en comprobarPesoMedicamentosDron " + requestString + ex.Message, ex);
                response.error.codigo = 5;
                response.error.descripcion = ex.Message;
            }

            return response;
        }


    }
}
