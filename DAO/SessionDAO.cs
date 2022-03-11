using Dapper;
using ws_rendicion.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace lib_session.DAO
{
    public class SessionDAO
    {
        private readonly IDapper _dapper;
        public SessionDAO(IDapper dapper)
        {
            _dapper = dapper;
        }

        public bool validarUsuario(long usuarioId, String token)
        {
            if (usuarioId > 0 && token != "")
            {
                int result;
                String sp = "[dbo].[prc_org_usuario_session_existe]";
                var dbParam = new DynamicParameters();
                dbParam.Add("@I_USUA_ID", usuarioId);
                dbParam.Add("@I_TOKEN", token);
                dbParam.Add("@O_EXISTE", DbType.Int32, direction: ParameterDirection.Output);
                result = _dapper.GetDbconnection().Execute(sp, dbParam, commandType: CommandType.StoredProcedure);
                int existe = dbParam.Get<Int32>("O_EXISTE");
                return existe == 0 ? false : true;
            }
            else 
            {
                return false;
            }
        }
    }
}
