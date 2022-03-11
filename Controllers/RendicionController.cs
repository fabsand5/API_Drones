using lib_session.DAO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rs_rendicion.DAO;
using rs_rendicion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ws_rendicion.Services;

namespace rs_rendicion.Controllers
{
    [Produces("application/json")]
    [Route("api/rendicion")]
    [EnableCors("MyPolicy")]
    public class RendicionController : ControllerBase
    {
        private readonly IDapper _dapper;

        private RendicionDao dao;
        private SessionDAO sessionDao;

        private readonly ILoggerManager _logger;

        public RendicionController(IDapper dapper, ILoggerManager logger)
        {
            _dapper = dapper;
            dao = new RendicionDao(dapper);
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


        //Agregar métodos aquí

        [HttpPost(nameof(obtenerListaValeCarga))]
        [EnableCors("MyPolicy")]
        public ResponseObtenerListaValeCargaTO obtenerListaValeCarga([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestObtenerListaValeCargaTO data)
        {
            ResponseObtenerListaValeCargaTO response = new ResponseObtenerListaValeCargaTO();
            if (sessionDao.validarUsuario(idUsuario, token))
            {
                try
                {
                    response.listaValesCarga = dao.obtenerListaValeCarga(data.valeCargaId, data.fechaInicio, data.fechaFin, data.codigoEstado);
                }
                catch (Exception ex)
                {
                    String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                    _logger.LogError("Error en obtenerListaValeCarga " + requestString + ex.Message, ex);
                    response.error.codigo = 15;
                    response.error.descripcion = ex.Message;
                }
            }
            else
            {
                response.error = this.errorSessionInvalida();
            }
            return response;
        }
    }
}
