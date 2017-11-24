using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDAOLayer.Database
{
    public class DatabaseResourceCreator
    {
        public static DbCommand createCommand(DbConnection connection, String strCommand)
        {
            if(connection.GetType() == typeof(OracleConnection))
            {
                return new OracleCommand(strCommand, (OracleConnection)connection);
            }
            else if (connection.GetType() == typeof(SqlConnection))
            {
                return new SqlCommand(strCommand, (SqlConnection)connection);
            }
            return null;
        }

        public static string createConnectionString(DbConnection connection)
        {
            string connString = "";
            if (connection.GetType() == typeof(OracleConnection))
            {
                connString = ApplicationProperties.OracleConnString;
                return connString;
            } else if (connection.GetType() == typeof(SqlConnection))
            {
                connString = ApplicationProperties.SqlServerConnString;
                return connString;
            }
            return null;
        }
    }


}
