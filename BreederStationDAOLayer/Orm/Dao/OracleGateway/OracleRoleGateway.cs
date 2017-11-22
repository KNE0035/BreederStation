using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Database;
using System.Data.Common;
using System;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleRoleGateway : RoleGateway
    {
        public static string TABLE_NAME = "Role";

        private static string SQL_INSERT = "INSERT INTO ROLE (ID, TYPE, DESCRIPTION) VALUES (:p_id,:p_type, :p_description)";

        private static string SQL_SELECT = "SELECT ID, TYPE, DESCRIPTION FROM ROLE";

        private static string SQL_UPDATE = "UPDATE ROLE SET description=:p_description WHERE TYPE=:p_type";

        private static string SQL_DELETE_TYPE = "DELETE FROM ROLE WHERE id=:p_id";

        public OracleRoleGateway(IDatabaseService db ) : base(db)
        {

        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, Role role)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = role.Type;
            oracleCommand.Parameters.Add("p_type", OracleDbType.Varchar2).Value = role.Type.ToString();
            oracleCommand.Parameters.Add("p_description", OracleDbType.Varchar2).Value = role.Description;
        }

        protected override void PrepareIdCommand(DbCommand command, int roleId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = roleId;
        }

        protected override IList<Role> Read(DbDataReader reader)
        {
            IList<Role> roles = new List<Role>();
            OracleDataReader oracleReader = (OracleDataReader)reader;

            while (oracleReader.Read())
            {
                int i = -1;
                Role role = new Role();
                role.Id = oracleReader.GetInt32(++i);
                role.Type = RoleEnumUtils.getRoleType(oracleReader.GetString(++i));
                role.Description = oracleReader.GetString(++i);
                roles.Add(role);
            }
            return roles;
        }

        protected override string GetInsertSql()
        {
            return SQL_INSERT;
        }

        protected override string GetDeleteSql()
        {
            return SQL_DELETE_TYPE;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }
    }
}
