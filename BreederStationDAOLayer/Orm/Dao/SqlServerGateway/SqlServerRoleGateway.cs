using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Database;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerRoleGateway : RoleGateway
    {
        public static string TABLE_NAME = "Role";

        private static string SQL_INSERT = "INSERT INTO ROLE (ID, TYPE, DESCRIPTION) VALUES (@p_id,@p_type, @p_description)";

        private static string SQL_SELECT = "SELECT ID, TYPE, DESCRIPTION FROM ROLE";

        private static string SQL_UPDATE = "UPDATE ROLE SET description=@p_description WHERE TYPE=@p_type";

        private static string SQL_DELETE_TYPE = "DELETE FROM ROLE WHERE id=@p_id";

        public SqlServerRoleGateway(IDatabaseService db ) : base(db)
        {

        }

        protected override void PrepareInsertUpdateCommand(DbCommand command, Role role)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = role.Type;
            sqlCommand.Parameters.Add("@p_type", SqlDbType.VarChar).Value = role.Type.ToString();
            sqlCommand.Parameters.Add("@p_description", SqlDbType.VarChar).Value = role.Description;
        }

        protected override void PrepareIdCommand(DbCommand command, int roleId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = roleId;
        }

        protected override IList<Role> Read(DbDataReader reader)
        {
            IList<Role> roles = new List<Role>();
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                Role role = new Role();
                role.Id = sqlReader.GetInt32(++i);
                role.Type = RoleEnumUtils.getRoleType(sqlReader.GetString(++i));
                role.Description = sqlReader.GetString(++i);
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
