using Dapper;
using rs_rendicion.Models;
using System;
using System.Collections.Generic;
using System.Data;
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


        public SolucionTO obtenerSolucion(String extra1)
        {
            String sp = ""; //"[dbo].[prc_ope_vale_carga_get]";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_extra1", extra1);

            SolucionTO solucion = _dapper.Get<SolucionTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return solucion;
        }

        public List<SolucionTO> obtenerListaSoluciones(long baseId, int estado)
        {
            String sp = "[dbo].[prc_org_solucion_get]";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_base_id", baseId);
            dbParam.Add("@i_estado", estado);

            List<SolucionTO> solucion = _dapper.GetAll<SolucionTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return solucion;
        }


        public SolucionTO aplicarCambiosSolucion(
            long baseId, long reglaId, long documentoId, String usuario, String observacion, String tipoObjeccionDesc,
            String solucionDesc)
        {
            String sp = "prc_ope_solucion_create";
            var dbParam = new DynamicParameters();
            dbParam.Add("@i_base_id", baseId);
            dbParam.Add("@i_scdt_id", reglaId);
            dbParam.Add("@i_dcto_id", documentoId);
            dbParam.Add("@i_sadt_usr", usuario);
            dbParam.Add("@i_sadt_obsv", observacion);
            dbParam.Add("@i_sadt_tpob_dsc", tipoObjeccionDesc);
            dbParam.Add("@i_sadt_sldt_dsc", solucionDesc);

            SolucionTO solucion = _dapper.Get<SolucionTO>(sp, dbParam, commandType: CommandType.StoredProcedure);
            return solucion;
        }
    }
}
