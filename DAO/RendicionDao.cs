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


        public long aplicarCambiosSolucion(RequestAplicarCambiosSolucionTO resp)
        {
            int result;

            String sp = "prc_ope_solucion_create";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_base_id", resp.baseId); // de obtenerUsuario
            dbParam.Add("@i_scdt_id", resp.reglaId); //solucion marcada en el select
            dbParam.Add("@i_dcto_id", resp.documentoId); // viene del obtenerDatosObjeccion
            dbParam.Add("@i_sadt_usr",resp.usuario); // de obtenerUsuario
            dbParam.Add("@i_sadt_obsv", resp.observacion); //texto que se escriba al cambiar la solucion
            dbParam.Add("@i_sadt_tpob_dsc", resp.tipoObjeccionDesc); //tipoObjeccionDescripcion sacado de obtenerDatosObjeccion
            dbParam.Add("@i_sadt_sldt_dsc", resp.solucionDesc); //descripcion de la solucion marcada
            dbParam.Add("@o_sadt_id", null, DbType.Int32, direction: ParameterDirection.Output);
            dbParam.Add("@o_error_cdg", null, DbType.Int32, direction: ParameterDirection.Output);
            dbParam.Add("@o_error_dsc", null, DbType.String, direction: ParameterDirection.Output, size: 500);

            _dapper.GetDbconnection().Execute(sp, dbParam, commandType: CommandType.StoredProcedure);

            result = dbParam.Get<Int32>("o_error_cdg");

            if (result == 0)
            {
                long id = dbParam.Get<Int32>("o_sadt_id");
                return id;
            }
            else
            {
                String descripcion = dbParam.Get<String>("o_error_dsc");
                throw new Exception(descripcion);
            }
        }



        public SolucionConfiguradaTO obtenerSolucionConfigurada(long? idDocumento, String codTipoObjeccion)
        {
            String sp = "[dbo].[prc_ope_solucion_configurada_get]";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_scdt_id", idDocumento);
            dbParam.Add("@i_tpob_cdg", codTipoObjeccion);
            
            SolucionConfiguradaTO solucion = _dapper.Get<SolucionConfiguradaTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return solucion;
        }


        public int obtenerCantSalidasTerreno(long idDocumento, String evento)
        {
            String sp = "[dbo].[fnc_ope_cant_eventos_docto](@doct_id, @dtes_cdg)";
            var dbParam = new DynamicParameters();
            dbParam.Add("@doct_id", idDocumento);
            dbParam.Add("@dtes_cdg", evento);

            int solucion = _dapper.Get<int>(sp, dbParam, commandType: CommandType.Text);
            return solucion;

        }
    }
}
