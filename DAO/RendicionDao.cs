using Dapper;
using rs_rendicion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ws_rendicion.Services;
//using rs_rendicion.Services;
//using ws_rendicion.Models;
//using rs_rendicion.Models;


namespace rs_rendicion.DAO
{
    public class RendicionDao
    {
        private readonly IDapper _dapper;

        public RendicionDao(IDapper dapper)
        {
            _dapper = dapper;
        }


        //Agregar métodos aquí

        public List<ValeCargaMonitorTO> obtenerListaValeCarga(long valeCargaId, string fechaInicio, string fechaFin, string codigoEstado)
        {

            String sp = "[dbo].[prc_ope_vale_carga_get]";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_vlcr_id", valeCargaId);
            dbParam.Add("@i_fecha_inicio", fechaInicio == null ? "" : fechaInicio);
            dbParam.Add("@i_fecha_fin", fechaFin == null ? "" : fechaFin);
            dbParam.Add("@i_vces_cdg", codigoEstado == null ? "" : codigoEstado);


            List<ValeCargaMonitorTO> listaVales = _dapper.GetAll<ValeCargaMonitorTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return listaVales;
        }

        public List<SolucionTO> obtenerListaSoluciones(int estado)
        {
            String sp = "[dbo].[prc_ope_solucion_get]";
            var dbParam = new DynamicParameters();
            //dbParam.Add("@i_base_id", baseId);
            dbParam.Add("@i_estado", estado);

            List<SolucionTO> solucion = _dapper.GetAll<SolucionTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return solucion;
        }


        public ResponseSolucionTO aplicarCambiosSolucion(SolucionConfiguradaTO solucionConfig, String observacion, long documentoId, String usuario, long baseId)
        {
            int result; 

            ResponseSolucionTO response = new ResponseSolucionTO();

            String sp = "prc_ope_solucion_create";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_base_id", baseId); // de obtenerUsuario
            dbParam.Add("@i_scdt_id", solucionConfig.solucionConfiguradaId); //solucion marcada en el select
            dbParam.Add("@i_dcto_id", documentoId); // de obtenerDatosObjeccion
            dbParam.Add("@i_sadt_usr", usuario); // de obtenerUsuario
            dbParam.Add("@i_sadt_obsv", observacion); //texto que se escriba al cambiar la solucion
            dbParam.Add("@i_sadt_tpob_dsc", solucionConfig.tipoObjeccionDescripcion); //de obtenerSolucionConfigurada
            dbParam.Add("@i_sadt_sldt_dsc", solucionConfig.solucionDesc); //descripcion de la solucion marcada
            dbParam.Add("@o_sadt_id", null, DbType.Int32, direction: ParameterDirection.Output);
            dbParam.Add("@o_error_cdg", null, DbType.Int32, direction: ParameterDirection.Output);
            dbParam.Add("@o_error_dsc", null, DbType.String, direction: ParameterDirection.Output, size: 500);

            _dapper.GetDbconnection().Execute(sp, dbParam, commandType: CommandType.StoredProcedure);

            result = dbParam.Get<Int32>("o_error_cdg");

            if (result == 0)
            {
                long id = dbParam.Get<Int32>("o_sadt_id");
                response.solucionId = id; //bitacora
                response.solucionConfiguradaId = solucionConfig.solucionConfiguradaId;
                response.solucionDescripcion = solucionConfig.solucionDesc;
                response.error.codigo = 0;
                return response;
            }
            else
            {
                String descripcion = dbParam.Get<String>("o_error_dsc");
                throw new Exception(descripcion);
            }
        }


        public SolucionConfiguradaTO obtenerSolucionConfigurada(long? regla, String codTipoObjeccion)
        {
            String sp = "[dbo].[prc_ope_solucion_configurada_get]";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_scdt_id", regla);
            dbParam.Add("@i_tpob_cdg", codTipoObjeccion);
            
            SolucionConfiguradaTO solucion = _dapper.Get<SolucionConfiguradaTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return solucion;
        }


        public int obtenerCantSalidasTerreno(long idDocumento, String evento)
        {
            string sql = "select COUNT(t.DOCT_ID)" +
                         " from [DataCore].[dbo].[SHP_DOCUMENTO_TRAZABILIDAD] t" +
                         " where t.DOCT_ID = " + idDocumento + " and DTES_CDG = '" + evento + "'";

            int count = _dapper.Get<int>(sql, null, commandType: CommandType.Text); //CommandType.StoredProcedure

            return count;
        }


        public List<HistorialSolucionesTO> obtenerHistorialSoluciones(long documentoId)
        {
            String sp = "[dbo].[prc_ope_solucion_aplicada_get]";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_docto_id", documentoId);

            List<HistorialSolucionesTO> historialSoluciones = _dapper.GetAll<HistorialSolucionesTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return historialSoluciones;
        }
    }
}
