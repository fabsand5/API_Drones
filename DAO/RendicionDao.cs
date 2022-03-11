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
    }
}
