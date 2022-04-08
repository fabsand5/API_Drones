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
        public ResponseSolucionTO aplicarRegla([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestAplicarCambiosSolucionTO data)
        {
            ResponseAplicarCambiosSolucionTO responseSolucionAplicada = new ResponseAplicarCambiosSolucionTO();
            ResponseSolucionTO responseAplicarRegla = new ResponseSolucionTO();
            int cantSalidasTerreno;

            const int REGLA_A_REVISION = 1;
            const int REGLA_RETENER_Y_DEVOLVER = 3;

            String observacion;

            if (sessionDao.validarUsuario(idUsuario, token))
            {
                try
                {
                    //PASO 1
                    responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(null, data.codTipoObjeccion);

                    observacion = "REGLA APLICADA POR SISTEMA";

                    if (responseSolucionAplicada != null)
                    {
                        //PASO 2
                        cantSalidasTerreno = dao.obtenerCantSalidasTerreno(data.documentoId, "RTA");

                        if (cantSalidasTerreno >= responseSolucionAplicada.solucionConfigurada.cantMaxAplicar)
                        //sldt solucion documento **** responseSolucionAplicada.solucionConfigurada.solucionId
                        //scdt solucion configurada documento
                        //sadt solucion aplicada al documento
                        {
                            observacion = "REGLA APLICADA POR DEFECTO POR CUMPLIR CANTIDAD MAXIMA (" + responseSolucionAplicada.solucionConfigurada.cantMaxAplicar  + ") DE SALIDAS A RUTA";
                            responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(REGLA_RETENER_Y_DEVOLVER, null);
                        }
                    }
                    else
                    {
                        observacion = "REGLA APLICADA POR DEFECTO YA QUE NO SE ENCONTRO REGLA CONFIGURADA";
                        responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(REGLA_A_REVISION, null);
                    }

                }
                catch (Exception ex)
                {
                    String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                    _logger.LogError("Error en aplicarRegla " + requestString + ex.Message, ex);
                    responseAplicarRegla.error.codigo = 16;
                    responseAplicarRegla.error.descripcion = ex.Message;

                    observacion = "REGLA APLICADA POR DEFECTO YA QUE OCURRIO UN ERROR: " + ex.Message;
                    responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(REGLA_A_REVISION, null);

                }

                //PASO 3
                responseAplicarRegla = dao.aplicarCambiosSolucion(responseSolucionAplicada.solucionConfigurada, observacion, data.documentoId, data.usuario, data.baseId);

            }
            else
            {
                responseAplicarRegla.error = this.errorSessionInvalida();
            }
            return responseAplicarRegla;
        }



        [HttpPost(nameof(aplicarCambiosSolucion))]
        [EnableCors("MyPolicy")]
        public ResponseSolucionTO aplicarCambiosSolucion([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestAplicarCambiosSolucionTO data)
        {
            ResponseAplicarCambiosSolucionTO responseSolucionAplicada = new ResponseAplicarCambiosSolucionTO();
            ResponseSolucionTO responseAplicarRegla = new ResponseSolucionTO();

            if (sessionDao.validarUsuario(idUsuario, token))
            {
                try
                {
                    responseSolucionAplicada.solucionConfigurada = dao.obtenerSolucionConfigurada(data.solucionId, null);

                    //responseSolucionAplicada.solucionConfigurada.solucionConfiguradaId = data.solucionId;

                    responseAplicarRegla = dao.aplicarCambiosSolucion(responseSolucionAplicada.solucionConfigurada, data.observacion, data.documentoId, data.usuario, data.baseId);
                }
                catch (Exception ex)
                {
                    String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                    _logger.LogError("Error en aplicarCambiosSolucion " + requestString + ex.Message, ex);
                    responseAplicarRegla.error.codigo = 15;
                    responseAplicarRegla.error.descripcion = ex.Message;
                }
            }
            else
            {
                responseAplicarRegla.error = this.errorSessionInvalida();
            }
            return responseAplicarRegla;
        }


        [HttpPost(nameof(obtenerHistorialSoluciones))]
        [EnableCors("MyPolicy")]
        public ResponseObtenerHistorialSolucionesTO obtenerHistorialSoluciones([FromHeader] long idUsuario, [FromHeader] String token, [FromBody] RequestObtenerHistorialSolucionesTO data)
        {
            ResponseObtenerHistorialSolucionesTO response = new ResponseObtenerHistorialSolucionesTO();
            if (sessionDao.validarUsuario(idUsuario, token))
            {
                try
                {
                    response.listaHistorialSoluciones = dao.obtenerHistorialSoluciones(data.documentoId);
                }
                catch (Exception ex)
                {
                    String requestString = JsonConvert.SerializeObject(data, Formatting.Indented);
                    _logger.LogError("Error en obtenerHistorialSoluciones " + requestString + ex.Message, ex);
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
