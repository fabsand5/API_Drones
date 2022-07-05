using Dapper;
using API_Drones.Models;
using API_Drones.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ws_API_Drones.Services;

namespace API_Drones.DAO
{
    public class DronesDAO
    {
        private readonly IDapper _dapper;

        List<DronTO> lstDron = new List<DronTO>();
    public DronesDAO(IDapper dapper)
        {
            _dapper = dapper;
        }

        public long registrarDron(DronTO dron)
        {
            String sp = "[dbo].[prc_]";
            var dbParam = new DynamicParameters();
            //dbParam.Add("@i_id", id);

            long numeroSerie = 0; //= _dapper.GetAll<DronTO>(sp, dbParam, commandType: CommandType.StoredProcedure);

            return numeroSerie;
        }


        public String cargarDron(MedicamentoTO medicamento)
        {
            String sp = "[dbo].[prc_]";
            var dbParam = new DynamicParameters();
            //dbParam.Add("@i_id", id);

            String codigo = ""; //= _dapper.GetAll<MedicamentoTO>(sp, dbParam, commandType: CommandType.StoredProcedure);

            return codigo;
        }


        public List<DronTO> obtenerDrones()
        {
            String sp = "[dbo].[prc_]";
            var dbParam = new DynamicParameters();
            //dbParam.Add("@i_id", id);
            lstDron.Add(new DronTO() { numeroSerie = "1234567890", modelo = "peso ligero", pesoLimite = 400, capacidadBateria = 40, estado = "INACTIVO" });
            lstDron.Add(new DronTO() { numeroSerie = "12345678901234567890", modelo = "peso medio", pesoLimite = 200, capacidadBateria = 100, estado = "CARGANDO" });
            lstDron.Add(new DronTO() { numeroSerie = "123456789012345678901234567890", modelo = "peso crucero", pesoLimite = 100, capacidadBateria = 30, estado = "CARGADO" });
            lstDron.Add(new DronTO() { numeroSerie = "1234567890123456789012345678901234567890", modelo = "peso pesado)", pesoLimite = 500, capacidadBateria = 25, estado = "ENTREGANDO CARGA" });
            lstDron.Add(new DronTO() { numeroSerie = "12345678901234567890123456789012345678901234567890", modelo = "peso ligero", pesoLimite = 400, capacidadBateria = 90, estado = "CARGA ENTREGADA" });
            lstDron.Add(new DronTO() { numeroSerie = "123456789012345678901234567890123456789012345678901234567890", modelo = "peso ligero", pesoLimite = 300, capacidadBateria = 77, estado = "REGRESANDO" });

            List<DronTO> listaDronesDisponibles = new List<DronTO>();
            foreach (var dron in lstDron)
            {
                if (dron.estado == "INACTIVO")
                    listaDronesDisponibles.Add(dron);
            }
            //List<DronTO> listaDrones = lstDron; //= _dapper.GetAll<MedicamentoTO>(sp, dbParam, commandType: CommandType.StoredProcedure);

            return listaDronesDisponibles;
        }
    }
}
