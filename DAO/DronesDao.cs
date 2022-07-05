using Dapper;
using rs_drones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ws_drones.Services;
//using rs_drones.Services;
//using ws_drones.Models;
//using rs_drones.Models;


namespace rs_drones.DAO
{
    public class dronesDao
    {
        private readonly IDapper _dapper;

        List<DronTO> lstDron = new List<DronTO>();
        List<MedicamentoTO> lstMedicamento = new List<MedicamentoTO>();

        public dronesDao(IDapper dapper)
        {
            _dapper = dapper;
        }


        //Agregar métodos aquí

        public long registrarDron(DronTO dron)
        {
            String sp = "[dbo].[prc_]";
            var dbParam = new DynamicParameters();
            //dbParam.Add("@i_id", id);

            long numeroSerie = 0; //= _dapper.GetAll<DronTO>(sp, dbParam, commandType: CommandType.StoredProcedure);

            return numeroSerie;
        }


        public String cargarDron(String medicamento, String dron)
        {
            String respuesta = "";
            String sp = "[dbo].[prc_]";
            var dbParam = new DynamicParameters();
            //dbParam.Add("@i_id", id);
            lstDron.Add(new DronTO() { numeroSerie = "1234567890", modelo = "peso ligero", pesoLimite = 400, capacidadBateria = 40, estado = "INACTIVO" });
            lstDron.Add(new DronTO() { numeroSerie = "12345678901234567890", modelo = "peso medio", pesoLimite = 200, capacidadBateria = 100, estado = "CARGANDO" });
            lstDron.Add(new DronTO() { numeroSerie = "123456789012345678901234567890", modelo = "peso crucero", pesoLimite = 100, capacidadBateria = 30, estado = "CARGADO" });
            lstDron.Add(new DronTO() { numeroSerie = "1234567890123456789012345678901234567890", modelo = "peso pesado)", pesoLimite = 500, capacidadBateria = 25, estado = "ENTREGANDO CARGA" });
            lstDron.Add(new DronTO() { numeroSerie = "12345678901234567890123456789012345678901234567890", modelo = "peso ligero", pesoLimite = 400, capacidadBateria = 90, estado = "CARGA ENTREGADA" });
            lstDron.Add(new DronTO() { numeroSerie = "123456789012345678901234567890123456789012345678901234567890", modelo = "peso ligero", pesoLimite = 300, capacidadBateria = 77, estado = "REGRESANDO" });

            lstMedicamento.Add(new MedicamentoTO() { nombre = "med_1", peso = 25, codigo = "MED_001", imagen = "" });
            lstMedicamento.Add(new MedicamentoTO() { nombre = "med_2", peso = 501, codigo = "MED_002", imagen = "" });
            lstMedicamento.Add(new MedicamentoTO() { nombre = "med_3", peso = 200, codigo = "MED_003", imagen = "" });
            lstMedicamento.Add(new MedicamentoTO() { nombre = "med_4", peso = 70, codigo = "MED_004", imagen = "" });

            MedicamentoTO medicamentoEncontrado = new MedicamentoTO();
            DronTO dronEncontrado = new DronTO();

            foreach(var med in lstMedicamento)
            {
                if (medicamento == med.codigo)
                { 
                    medicamentoEncontrado = med; 
                }
                else
                { 
                    respuesta = "Medicamento no encontrado."; 
                }
            }

            foreach (var drn in lstDron)
            {
                if (dron == drn.numeroSerie)
                {
                    dronEncontrado = drn;
                }
                else
                {
                    respuesta = "Dron no encontrado.";
                }
            }

            if (dronEncontrado.pesoLimite >= medicamentoEncontrado.peso)
            {
                respuesta = "Medicamento " + medicamento + " agregado a dron " + dron + " exitosamente.";
            }
            else
            {
                respuesta = "Medicamento supera peso límite de dron.";
            }

            //String codigo = ""; //= _dapper.GetAll<MedicamentoTO>(sp, dbParam, commandType: CommandType.StoredProcedure);

            return respuesta;
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


        public String comprobarNivelBateria(String numeroSerie)
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

            DronTO dronEncontrado = new DronTO();

            String respuesta = "";

            foreach (var dron in lstDron)
            {
                if (dron.numeroSerie == numeroSerie)
                    dronEncontrado = dron;
            }

            if(dronEncontrado != null)
                respuesta = "El nivel de batería del dron " + numeroSerie + " es: " + dronEncontrado.capacidadBateria + "%.";
            else
                respuesta = "Dron no encontrado";

            //String codigo = ""; //= _dapper.GetAll<MedicamentoTO>(sp, dbParam, commandType: CommandType.StoredProcedure);

            return respuesta;
        }


        public String comprobarPesoMedicamentosDron(String numeroSerie)
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

            DronTO dronEncontrado = new DronTO();

            String respuesta = "";

            foreach (var dron in lstDron)
            {
                if (dron.numeroSerie == numeroSerie)
                    dronEncontrado = dron;
            }

            if (dronEncontrado != null)
                respuesta = "El peso total de medicamentos cargados en el dron " + dronEncontrado.numeroSerie + " es: " + dronEncontrado.pesoLimite + " gr.";
            else
                respuesta = "Dron no encontrado";

            //String codigo = ""; //= _dapper.GetAll<MedicamentoTO>(sp, dbParam, commandType: CommandType.StoredProcedure);

            return respuesta;
        }
    }
}
