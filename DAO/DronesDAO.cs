using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Drones.Models;
using System.Data;
using Dapper;

namespace API_Drones.DAO
{
    public class DronesDAO
    {
        public long registrarDron(DronTO dron)
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
