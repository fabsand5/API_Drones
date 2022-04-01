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



        //[HttpPost(nameof(obtenerSolucion))]
        //[EnableCors("MyPolicy")]
        //public ResponseObtenerSolucionTO obtenerSolucion([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestObtenerSolucionTO data)
        //{
        //    ResponseObtenerSolucionTO response = new ResponseObtenerSolucionTO();
        //    if (sessionDao.validarUsuario(idUsuario, token))
        //    {
        //        try
        //        {
        //            response.solucion = dao.obtenerSolucion(data.extra1);
        //        }
        //        catch (Exception ex)
        //        {
        //            String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
        //            _logger.LogError("Error en obtenerSolucion " + requestString + ex.Message, ex);
        //            response.error.codigo = 15;
        //            response.error.descripcion = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        response.error = this.errorSessionInvalida();
        //    }
        //    return response;
        //}


        [HttpPost(nameof(obtenerListaSoluciones))]
        [EnableCors("MyPolicy")]
        public ResponseObtenerListaSolucionesTO obtenerListaSoluciones([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestObtenerListaSolucionesTO data)
        {
            ResponseObtenerListaSolucionesTO response = new ResponseObtenerListaSolucionesTO();
            if (sessionDao.validarUsuario(idUsuario, token))
            {
                try
                {
                    response.listaSolucion = dao.obtenerListaSoluciones( data.estado);
                }
                catch (Exception ex)
                {
                    String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                    _logger.LogError("Error en obtenerListaSoluciones " + requestString + ex.Message, ex);
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



        [HttpPost(nameof(aplicarRegla))]
        [EnableCors("MyPolicy")]
        public ResponseAplicarCambiosSolucionTO aplicarRegla([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestAplicarCambiosSolucionTO data)
        {
            ResponseAplicarCambiosSolucionTO responseSolucionAplicada = new ResponseAplicarCambiosSolucionTO();
            ResponseAplicarCambiosSolucionTO responseAplicarRegla = new ResponseAplicarCambiosSolucionTO();
            int cantSalidasTerreno;

            const int REGLA_A_REVISION = 1;
            const int REGLA_RETENER_Y_DEVOLVER = 3;


            if (sessionDao.validarUsuario(idUsuario, token))
            {
                try
                {
                    //PASO 1
                    responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(null, data.codTipoObjeccion);


                    if (responseSolucionAplicada != null)
                    {
                        //PASO 2
                        cantSalidasTerreno = dao.obtenerCantSalidasTerreno(data.idDocumento, "RTA");

                        if (cantSalidasTerreno >= responseSolucionAplicada.solucionConfigurada.cantMaxAplicar)
                        {
                            responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(REGLA_RETENER_Y_DEVOLVER, null);
                        }
                    }
                    else
                    {
                        responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(REGLA_A_REVISION, null);
                    }

                    RequestAplicarCambiosSolucionTO aplicarCambiosSolucion = data;
                    //PASO 3
                    responseAplicarRegla.solucionId = dao.aplicarCambiosSolucion(aplicarCambiosSolucion);
                }
                catch (Exception ex)
                {
                    String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                    _logger.LogError("Error en aplicarCambiosSolucion " + requestString + ex.Message, ex);
                    responseAplicarRegla.error.codigo = 16;
                    responseAplicarRegla.error.descripcion = ex.Message;
                }
            }
            else
            {
                responseAplicarRegla.error = this.errorSessionInvalida();
            }
            return responseAplicarRegla;
        }





        [HttpPost(nameof(aplicarCambiosSolucion))]
        [EnableCors("MyPolicy")]
        public ResponseAplicarCambiosSolucionTO aplicarCambiosSolucion([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestAplicarCambiosSolucionTO data)
        {
            ResponseAplicarCambiosSolucionTO response = new ResponseAplicarCambiosSolucionTO();
            if (sessionDao.validarUsuario(idUsuario, token))
            {
                RequestAplicarCambiosSolucionTO aplicarCambiosSolucion = data;
                try
                {
                    response.solucionConfigurada = dao.obtenerSolucionConfigurada(data.idDocumento, data.codTipoObjeccion);
                    response.solucionId = dao.aplicarCambiosSolucion(aplicarCambiosSolucion);
                    //response.solucionId = dao.aplicarCambiosSolucion(data.baseId, data.reglaId, data.documentoId, data.usuario, data.observacion, data.tipoObjeccionDesc, data.solucionDesc);
                }
                catch (Exception ex)
                {
                    String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                    _logger.LogError("Error en obtenerListaSoluciones " + requestString + ex.Message, ex);
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
